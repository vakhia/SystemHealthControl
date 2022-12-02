FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CA.API/CA.API.csproj", "CA.API/"]
RUN dotnet restore "CA.API/CA.API.csproj"
COPY . .
WORKDIR "/src/CA.API"
RUN dotnet build "CA.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CA.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CA.API.dll"]
