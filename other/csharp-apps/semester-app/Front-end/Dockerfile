FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY . ./studym8/
WORKDIR /app/studym8

# Install node
RUN curl -sL https://deb.nodesource.com/setup_14.x | bash
RUN apt-get update && apt-get install -y nodejs

# Install node build dependencies
RUN cd Front-end && npm install -y && cd ../

# Copy everything else and build
RUN dotnet publish Front-end/Front-end.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app

COPY --from=build-env /app/studym8/out .
ENTRYPOINT ["dotnet", "Front-end.dll"]
