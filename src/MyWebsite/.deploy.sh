#!/bin/bash

set -e

SOURCE="${BASH_SOURCE[0]}"
while [ -h "$SOURCE" ]; do # resolve $SOURCE until the file is no longer a symlink
  DIR="$( cd -P "$( dirname "$SOURCE" )" && pwd )"
  SOURCE="$(readlink "$SOURCE")"
  [[ $SOURCE != /* ]] && SOURCE="$DIR/$SOURCE" # if $SOURCE was a relative symlink, we need to resolve it relative to the path where the symlink file was located
done
DIR="$( cd -P "$( dirname "$SOURCE" )" && pwd )"

cd $DIR

git fetch origin
git reset --hard origin/master

dotnet restore

supervisorctl stop mywebsite

rm -rf /srv/www/dotnetcore/shevastream/*

dotnet publish -o /srv/www/dotnetcore/mywebsite

supervisorctl start mywebsite

