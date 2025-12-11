# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["F1DriverApp.Api.csproj", "./"]
RUN dotnet restore "F1DriverApp.Api.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "F1DriverApp.Api.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "F1DriverApp.Api.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "F1DriverApp.Api.dll"]
