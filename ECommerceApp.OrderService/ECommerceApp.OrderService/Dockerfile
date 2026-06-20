FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj first (better caching)
COPY ECommerceApp.OrderService/ECommerceApp.OrderService/ECommerceApp.OrderService.csproj ECommerceApp.OrderService/

COPY ECommerceApp.Shared/ECommerceApp.Shared/ECommerceApp.Shared.csproj ECommerceApp.Shared/

RUN dotnet restore ECommerceApp.OrderService/ECommerceApp.OrderService.csproj

# Copy everything else
COPY . .

WORKDIR /src/ECommerceApp.OrderService/ECommerceApp.OrderService
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ECommerceApp.OrderService.dll"]