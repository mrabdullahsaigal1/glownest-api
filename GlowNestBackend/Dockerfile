# Use the official .NET 8.0 runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the official .NET 8.0 SDK as the build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["GlowNestBackend.csproj", "./"]
RUN dotnet restore "./GlowNestBackend.csproj"

# Copy the remaining source code and build the application
COPY . . 
WORKDIR "/src"
RUN dotnet publish "./GlowNestBackend.csproj" -c Release -o /app/publish

# Final stage to build the runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GlowNestBackend.dll"]
