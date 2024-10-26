## Utvecklingsläge
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081
#
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Debug
#WORKDIR /app
#COPY . .
#
## Installera watch-verktyget
#RUN dotnet tool install --global dotnet-watch
#
## Se till att dotnet-watch kan användas i sessionen
#ENV PATH="$PATH:/root/.dotnet/tools"
#
#ENTRYPOINT ["dotnet", "watch", "run", "--urls", "http://+:8080;https://+:8081"]



## Produktionsläge
# Stage 1: Bygg-steg
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Kopiera csproj och återställ beroenden
COPY ["CalculationBackend.csproj", "."]
RUN dotnet restore "./CalculationBackend.csproj"

# Kopiera all kod och bygg applikationen
COPY . .
RUN dotnet build "./CalculationBackend.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 2: Publicerings-steg
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CalculationBackend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime-steg (detta är den slutliga containern som körs i produktion)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Kopiera publicerade filer från föregående steg
COPY --from=publish /app/publish .

# Exponera portar
EXPOSE 8080
EXPOSE 8081

# Kör applikationen
ENTRYPOINT ["dotnet", "CalculationBackend.dll"]



## Wait for it-lösning så backend väntar på att mongodb är redo (används om inte ovan fungerar)
## Stage 1: Bygg-steg
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#
## Kopiera csproj och återställ beroenden
#COPY ["CalculationBackend.csproj", "."]
#RUN dotnet restore "./CalculationBackend.csproj"
#
## Kopiera all kod och bygg applikationen
#COPY . .
#RUN dotnet build "./CalculationBackend.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
## Stage 2: Publicerings-steg
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./CalculationBackend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
## Stage 3: Runtime-steg (detta är den slutliga containern som körs i produktion)
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
#WORKDIR /app
#
## Kopiera publicerade filer från föregående steg
#COPY --from=publish /app/publish .
#
## Ladda ner wait-for-it-skriptet för att vänta på MongoDB
#ADD https://raw.githubusercontent.com/vishnubob/wait-for-it/master/wait-for-it.sh /wait-for-it.sh
#RUN chmod +x /wait-for-it.sh
#
## Exponera portar
#EXPOSE 8080
#EXPOSE 8081
#
## Kör applikationen men vänta först på att MongoDB ska vara redo
#ENTRYPOINT ["/wait-for-it.sh", "mongo:27017", "--", "dotnet", "CalculationBackend.dll"]