# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
RUN apk add --update nodejs npm
WORKDIR /src
COPY ["clientside.backend/clientside.backend/clientside.backend.csproj", "clientside.backend/clientside.backend/"]
COPY ["frontend/frontend.esproj", "frontend/"]
COPY ["RolDbContext/RolDbContext.csproj", "RolDbContext/"]
COPY ["viewmodels/viewmodels.csproj", "viewmodels/"]
RUN dotnet restore "./clientside.backend/clientside.backend/clientside.backend.csproj"
COPY . .
WORKDIR "/src/clientside.backend/clientside.backend"
RUN dotnet build "./clientside.backend.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./clientside.backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=publish /src/frontend/dist/frontend ./wwwroot
ENTRYPOINT ["dotnet", "clientside.backend.dll"]