# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copiar todo y restaurar dependencias
COPY . ./
RUN dotnet restore

# Publicar la app
RUN dotnet publish -c Release -o out

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expone el puerto por defecto (ajusta si es necesario)
EXPOSE 80

# Comando para ejecutar tu app (ajusta si tu DLL tiene otro nombre)
ENTRYPOINT ["dotnet", "Chatbot WhatsApp Cobranza.dll"]
