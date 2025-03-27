FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia el .sln y los .csproj de cada proyecto
COPY BusinessManagement.sln ./
COPY BusinessManagement.Domain/BusinessManagement.Domain.csproj BusinessManagement.Domain/
COPY BusinessManagement.Application/BusinessManagement.Application.csproj BusinessManagement.Application/
COPY BusinessManagement.Infrastructure/BusinessManagement.Infrastructure.csproj BusinessManagement.Infrastructure/
COPY BusinessManagement.WebApi/BusinessManagement.WebApi.csproj BusinessManagement.WebApi/

# Restaura dependencias
RUN dotnet restore BusinessManagement.sln

# Copia el resto del código
COPY . .

# Compila y publica la WebApi
RUN dotnet publish BusinessManagement.WebApi/BusinessManagement.WebApi.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

EXPOSE 80
ENTRYPOINT ["dotnet", "BusinessManagement.WebApi.dll"]
