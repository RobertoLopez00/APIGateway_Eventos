# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "APIGateway_Eventos/APIGateway_Eventos.csproj"
RUN dotnet publish "APIGateway_Eventos/APIGateway_Eventos.csproj" -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8081
EXPOSE 8081
COPY --from=build /app/publish .
# Asegurar que ocelot.json esté en el working dir del runtime
COPY APIGateway_Eventos/ocelot.json ./ocelot.json
ENTRYPOINT ["dotnet", "APIGateway_Eventos.dll"]