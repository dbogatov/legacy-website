#!/bin/bash

set -u
set -e

# CD to proper directory

SOURCE="${BASH_SOURCE[0]}"
while [ -h "$SOURCE" ]; do # resolve $SOURCE until the file is no longer a symlink
  DIR="$( cd -P "$( dirname "$SOURCE" )" && pwd )"
  SOURCE="$(readlink "$SOURCE")"
  [[ $SOURCE != /* ]] && SOURCE="$DIR/$SOURCE" # if $SOURCE was a relative symlink, we need to resolve it relative to the path where the symlink file was located
done
DIR="$( cd -P "$( dirname "$SOURCE" )" && pwd )"

cd $DIR

printf "Current dir "$DIR"\n"

# Now in good dir

WEBSITEPATH="/var/aspnet/mywebsite"
SUPERVISORJOB="mywebsite"

git fetch --all --quiet

#RESLOG=$(git log HEAD..origin/master --oneline)
#if [[ "${RESLOG}" != "" ]] ; then

    printf "Deploy start\n"

    # Git
    printf "Git fetch/reset/pull\n"
    git reset --quiet --hard origin/master
    git pull --quiet

    # Supervisor stops site
    printf "Stop website\n"
    supervisorctl stop mywebsite

    # Clean directory
    printf "CLeaning directory\n"
    rm -rf $WEBSITEPATH/*

    # Restore packages
    printf "Restoring NUGET packages\n"
    dnu restore

    # Publish
    printf "Publishing\n"

    # know bug: dnu tarcks elapsed time which does not work with bash
    set +e
    dnu publish --out /var/aspnet/mywebsite
    set -e

    # Fix runtime
    printf "Fixing runtime\n"
    perl -pi -e 's/"dnx"/"\/root\/.dnx\/runtimes\/dnx-coreclr-linux-x64.1.0.0-rc1-update1\/bin\/dnx"/g' /var/aspnet/mywebsite/approot/web

    # Supervisor uns website
    printf "Run website\n"
    supervisorctl start mywebsite
    
    /root/text.sh send --text="Deploy for mywebsite complete." --phones=15086677440

    printf "Deploy done.\n"

#fi

# else
    
    # If here, then there was anoter branch updated
    # printf "No changes.\n"

# fi
