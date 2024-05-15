FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ENV TZ=America/Sao_Paulo
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/BurgerRoyale.Kitchen/BurgerRoyale.Kitchen.csproj", "src/BurgerRoyale.Kitchen/"]
COPY ["src/BurgerRoyale.Kitchen.Application/BurgerRoyale.Kitchen.Application.csproj", "src/BurgerRoyale.Kitchen.Application/"]
COPY ["src/BurgerRoyale.Kitchen.Domain/BurgerRoyale.Kitchen.Domain.csproj", "src/BurgerRoyale.Kitchen.Domain/"]
COPY ["src/BurgerRoyale.Kitchen.BackgroundService/BurgerRoyale.Kitchen.BackgroundService.csproj", "src/BurgerRoyale.Kitchen.BackgroundService/"]
COPY ["src/BurgerRoyale.Kitchen.IOC/BurgerRoyale.Kitchen.IOC.csproj", "src/BurgerRoyale.Kitchen.IOC/"]
COPY ["src/BurgerRoyale.Kitchen.Infrastructure/BurgerRoyale.Kitchen.Infrastructure.csproj", "src/BurgerRoyale.Kitchen.Infrastructure/"]
RUN dotnet restore "src/BurgerRoyale.Kitchen/BurgerRoyale.Kitchen.csproj"
COPY . .
WORKDIR "/src/src/BurgerRoyale.Kitchen"
RUN dotnet build "BurgerRoyale.Kitchen.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BurgerRoyale.Kitchen.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BurgerRoyale.Kitchen.dll"]