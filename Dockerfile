# ---------- Build Stage ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files first (for caching)
COPY src/SensitiveWords.Api/SensitiveWords.Api.csproj src/SensitiveWords.Api/
COPY src/SensitiveWords.Application/SensitiveWords.Application.csproj src/SensitiveWords.Application/
COPY src/SensitiveWords.Domain/SensitiveWords.Domain.csproj src/SensitiveWords.Domain/
COPY src/SensitiveWords.Infrastructure/SensitiveWords.Infrastructure.csproj src/SensitiveWords.Infrastructure/

# Restore dependencies
RUN dotnet restore src/SensitiveWords.Api/SensitiveWords.Api.csproj

# Copy the rest of the source
COPY . .

# Publish application
RUN dotnet publish src/SensitiveWords.Api/SensitiveWords.Api.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# ---------- Runtime Stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "SensitiveWords.Api.dll"]