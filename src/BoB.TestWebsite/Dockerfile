#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["BoB.TestWebsite/BoB.TestWebsite.csproj", "BoB.TestWebsite/"]
COPY ["BoB.LanguageManager/BoB.LanguageManager.csproj", "BoB.LanguageManager/"]
COPY ["BoB.CacheManager/BoB.CacheManager.csproj", "BoB.CacheManager/"]
COPY ["BoB.BaseModule/BoB.BaseModule.csproj", "BoB.BaseModule/"]
COPY ["BoB.BaseConfiguration/BoB.BoBConfiguration.csproj", "BoB.BaseConfiguration/"]
COPY ["BoB.ContainManager/BoB.ContainManager.csproj", "BoB.ContainManager/"]
COPY ["ExtendAndHelper/ExtendAndHelper.csproj", "ExtendAndHelper/"]
COPY ["BoB.UseBus/BoB.UseBus.csproj", "BoB.UseBus/"]
COPY ["BoB.BoBLogger/BoB.BoBLogger.csproj", "BoB.BoBLogger/"]
COPY ["BoB.AutoMapperManager/BoB.AutoMapperManager.csproj", "BoB.AutoMapperManager/"]
COPY ["BoB.WorkModule/BoB.Work.csproj", "BoB.WorkModule/"]
COPY ["BoB.MainDataBase/BoB.MainDataBase.csproj", "BoB.MainDataBase/"]
COPY ["BoB.EFDbContext/BoB.EFDbContext.csproj", "BoB.EFDbContext/"]
COPY ["BoB.BoBConfigManager/BoB.BoBConfigManager.csproj", "BoB.BoBConfigManager/"]
COPY ["BoB.RazorWork/BoB.RazorWork.csproj", "BoB.RazorWork/"]
COPY ["BoB.RazorDataBase/BoB.RazorDataBase.csproj", "BoB.RazorDataBase/"]
COPY ["BoB.BoBExceptions/BoB.BoBExceptions.csproj", "BoB.BoBExceptions/"]
RUN dotnet restore "BoB.TestWebsite/BoB.TestWebsite.csproj"
COPY . .
WORKDIR "/src/BoB.TestWebsite"
RUN dotnet build "BoB.TestWebsite.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BoB.TestWebsite.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BoB.TestWebsite.dll"]