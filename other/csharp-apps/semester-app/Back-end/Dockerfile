FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY . ./studym8/
WORKDIR /app/studym8

# Copy everything else and build
RUN dotnet publish Back-end/Back-end.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app

COPY --from=build-env /app/studym8/out .
ENTRYPOINT ["dotnet", "Back-end.dll"]
