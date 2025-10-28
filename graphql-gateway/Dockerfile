# ---------- Base runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5128  

# ---------- Build stage ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app

COPY graphql-gateway.csproj ./
RUN dotnet restore "./graphql-gateway.csproj"
COPY . ./
RUN dotnet build "./graphql-gateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

# ---------- Publish stage ----------
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./graphql-gateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ---------- Final stage ----------
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ORDERS_SERVICE_URL=http://order-service:5010
ENV PRODUCTS_SERVICE_URL=http://product-service:5038
ENTRYPOINT ["dotnet", "graphql-gateway.dll"]
