FROM microsoft/dotnet:1.1-runtime

# Create directory for the app source code
WORKDIR /srv

# Copy the source and restore dependencies
COPY bin/release/netcoreapp1.0/publish/ /srv

ENV ASPNETCORE_ENVIRONMENT Production
ENV HOME /srv

# Expose the port and start the app

EXPOSE 5001

ENTRYPOINT /bin/bash -c "dotnet MyWebsite.dll"
