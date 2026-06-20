FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ECommerceApp.InventoryService/ECommerceApp.InventoryService/ECommerceApp.InventoryService.csproj ECommerceApp.InventoryService/
COPY ECommerceApp.Shared/ECommerceApp.Shared/ECommerceApp.Shared.csproj ECommerceApp.Shared/

RUN dotnet restore ECommerceApp.InventoryService/ECommerceApp.InventoryService.csproj

COPY . .

WORKDIR /src/ECommerceApp.InventoryService/ECommerceApp.InventoryService
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ECommerceApp.InventoryService.dll"]