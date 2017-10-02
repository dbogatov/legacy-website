FROM johnwu/dotnet-build

WORKDIR /code

# Copy the source and restore dependencies
COPY . .

RUN npm install -g gulp bower && dotnet restore && dotnet build && dotnet publish


# Expose the port and start the app

EXPOSE 5001

ENTRYPOINT /bin/bash -c "dotnet bin/Debug/netcoreapp1.0/MyWebsite.dll"
