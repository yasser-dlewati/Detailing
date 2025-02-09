FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080 
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /scr
ARG BUILD_CONFIGURATION=Release
COPY ["detailing.csproj", "Detailing/"]
RUN dotnet restore "Detailing/detailing.csproj"
WORKDIR "/src/Detailing"
COPY . .
RUN dotnet build "detailing.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./detailing.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "detailing.dll"]
