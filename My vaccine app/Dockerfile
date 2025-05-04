# Usa una imagen base de .NET SDK para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copia el archivo csproj y restaura las dependencias
COPY *.csproj ./
RUN dotnet restore

# Copia todo el resto del código
COPY . ./

# Construye la aplicación
RUN dotnet publish -c Release -o out

# Usa una imagen base de .NET Runtime para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .

# Expone el puerto en el que la aplicación escuchará
EXPOSE 5000

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "My vaccine app.dll"]
