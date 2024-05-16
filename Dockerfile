FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ENV TZ=America/Sao_Paulo
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BurgerRoyale.Kitchen/BurgerRoyale.Kitchen/BurgerRoyale.Kitchen.API.csproj", "BurgerRoyale.Kitchen/BurgerRoyale.Kitchen/"]
COPY ["BurgerRoyale.Kitchen/BurgerRoyale.Kitchen.Application/BurgerRoyale.Kitchen.Application.csproj", "BurgerRoyale.Kitchen/BurgerRoyale.Kitchen.Application/"]
COPY ["BurgerRoyale.Kitchen/BurgerRoyale.Kitchen.Domain/BurgerRoyale.Kitchen.Domain.csproj", "BurgerRoyale.Kitchen/BurgerRoyale.Kitchen.Domain/"]
COPY ["BurgerRoyale.Kitchen/BurgerRoyale.Kitchen.IOC/BurgerRoyale.Kitchen.IOC.csproj", "BurgerRoyale.Kitchen/BurgerRoyale.Kitchen.IOC/"]
COPY ["BurgerRoyale.Kitchen/BurgerRoyale.Kitchen.BackgroundService/BurgerRoyale.Kitchen.BackgroundService.csproj", "BurgerRoyale.Kitchen/BurgerRoyale.Kitchen.BackgroundService/"]
COPY ["BurgerRoyale.Kitchen/BurgerRoyale.Kitchen.Infrastructure/BurgerRoyale.Kitchen.Infrastructure.csproj", "BurgerRoyale.Kitchen/BurgerRoyale.Kitchen.Infrastructure/"]
RUN dotnet restore "./BurgerRoyale.Kitchen/BurgerRoyale.Kitchen/BurgerRoyale.Kitchen.API.csproj"

COPY . .
WORKDIR "/src/BurgerRoyale.Kitchen/BurgerRoyale.Kitchen"
RUN dotnet build "./BurgerRoyale.Kitchen.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BurgerRoyale.Kitchen.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BurgerRoyale.Kitchen.API.dll"]