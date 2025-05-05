# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only .csproj files first (for better build caching)
COPY EcommerenceBackend.WebApi/*.csproj ./EcommerenceBackend.WebApi/
COPY EcommerenceBackend.Application.Domain/*.csproj ./EcommerenceBackend.Application.Domain/
COPY EcommerenceBackend.Application.Dto/*.csproj ./EcommerenceBackend.Application.Dto/
COPY EcommerenceBackend.Infrastructure/*.csproj ./EcommerenceBackend.Infrastructure/
COPY EcommerenceBackend.Application.UseCases/*.csproj ./EcommerenceBackend.Application.UseCases/
COPY EcommerenceBackend.Application.Interfaces/*.csproj ./EcommerenceBackend.Application.Interfaces/
COPY EcommerenceBackend.Application.Interface/*.csproj ./EcommerenceBackend.Application.Interface/
COPY EcommerenceBackendUnitTestProj/*.csproj ./EcommerenceBackendUnitTestProj/

# Restore dependencies
RUN dotnet restore "./EcommerenceBackend.WebApi/EcommerenceBackend.WebApi.csproj"

# Copy everything else
COPY . .

# Build the project
RUN dotnet build "./EcommerenceBackend.WebApi/EcommerenceBackend.WebApi.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "./EcommerenceBackend.WebApi/EcommerenceBackend.WebApi.csproj" -c Release -o /app/publish

# Stage 3: Final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Production
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EcommerenceBackend.WebApi.dll"]