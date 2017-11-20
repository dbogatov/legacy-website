FROM johnwu/dotnet-build

WORKDIR /code

# Copy the source and restore dependencies
COPY . .

RUN npm install -g gulp bower && dotnet restore && dotnet build && dotnet publish -c Release
# && cp appsettings.* bin/Release/netcoreapp1.0/publish

RUN mv bin/Release/netcoreapp1.0/publish/* /srv && rm -rf /code

# Expose the port and start the app

EXPOSE 80

ENTRYPOINT cd /srv && dotnet code.dll
