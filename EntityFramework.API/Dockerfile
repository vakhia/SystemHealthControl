FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EntityFramework.API/EntityFramework.API.csproj", "EntityFramework.API/"]
COPY ["EntityFramework.DAL/EntityFramework.DAL.csproj", "EntityFramework.DAL/"]
COPY ["EntityFramework.BLL/EntityFramework.BLL.csproj", "EntityFramework.BLL/"]
RUN dotnet restore "EntityFramework.API/EntityFramework.API.csproj"
COPY . .
WORKDIR "/src/EntityFramework.API"
RUN dotnet build "EntityFramework.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EntityFramework.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EntityFramework.API.dll"]
