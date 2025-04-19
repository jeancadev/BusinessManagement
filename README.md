# BusinessManagement - Aplicaci�n ASP.NET Core con DB Contenedorizada

Este proyecto es una aplicaci�n de gesti�n empresarial (CRUD de clientes, productos, ventas, etc.) construida con:
- ASP.NET Core (aplicando Clean Architecture y principios SOLID)
- EF Core para acceso a datos y migraciones
- SQL Server en contenedor Docker
- Docker Compose para orquestaci�n

El objetivo es demostrar un entorno completamente contenedorizado, con datos de demo (seeding) y f�cil despliegue.


## �ndice
1. [Arquitectura y Diagrama](#arquitectura-y-diagrama)
2. [Requisitos Previos](#requisitos-previos)
3. [Instrucciones de Ejecuci�n](#instrucciones-de-ejecuci�n)
4. [Uso de la Aplicaci�n](#uso-de-la-aplicaci�n)
5. [Historias y Logros T�cnicos](#historias-de-impacto-y-logros-t�cnicos)
6. [Posibles Mejoras Futuras](#posibles-mejoras-futuras)
7. [Cr�ditos / Referencias](#cr�ditos--referencias)


## Arquitectura y Diagrama

A continuaci�n se muestra la arquitectura en capas y la relaci�n entre contenedores Docker:

![Arquitectura del Proyecto]
graph TD
    subgraph "Docker Compose Environment"
        DC[Docker Compose] --> |orchestrates| WebContainer
        DC --> |orchestrates| DBContainer
        
        subgraph "WebContainer: ASP.NET Core"
            WebApp[Web API Container<br>Puerto: 8090:80] --> |contains| PL
            
            subgraph "Clean Architecture"
                PL[Presentation Layer<br>- Controllers<br>- Middleware<br>- Swagger] --> |uses| AL
                AL[Application Layer<br>- Services<br>- DTOs<br>- Interfaces] --> |uses| DL
                DL[Domain Layer<br>- Entities<br>- Value Objects<br>- Domain Services] 
                IL[Infrastructure Layer<br>- Repositories<br>- DbContext<br>- Migrations<br>- DbInitializer] --> |implements| AL
                IL --> |depends on| DL
            end
        end
        
        subgraph "DBContainer: SQL Server 2019"
            DB[(SQL Server 2019<br>Puerto: 1433:1433)] --> |contains| Tables
            Tables[Tables<br>- Products<br>- Customers<br>- Sales<br>- SaleItems]
            Vol[(Volume<br>/var/opt/mssql/data)]
            DB --- Vol
        end
        
        WebApp --> |EF Core<br>TCP 1433| DB
        WebApp --> |API Requests| Client
        Client[Client/Browser<br>- Swagger UI]
        
        subgraph "Authentication"
            Auth[Basic Auth<br>- admin/1234<br>- it/9999] --> |secures| WebApp
        end
        
        subgraph "Initial Data"
            Seed[Seeding Process<br>- Demo Products<br>- Demo Customers] --> |populates| DB
        end
    end
    
    style WebContainer fill:#d0e0ff,stroke:#0066cc
    style DBContainer fill:#ffe0d0,stroke:#cc6600
    style DC fill:#d0ffd0,stroke:#00cc66
    style Auth fill:#ffd0e0,stroke:#cc0066
    style Seed fill:#e0d0ff,stroke:#6600cc

- El contenedor `webapi` ejecuta ASP.NET Core y EF Core.
- El contenedor `db` corre SQL Server 2019.
- Docker Compose orquesta ambos contenedores y expone los puertos.

## Requisitos Previos

- **Docker Desktop** (o Docker Engine + Docker Compose) instalado.
- **Git** (para clonar el repositorio localmente).
- .NET SDK 8 (o la versi�n que este usando) si deseas compilar y correr la app fuera de Docker.

## Instrucciones de Ejecuci�n

1. Clona este repositorio:
   git clone https://github.com/jeancadev/BusinessManagement.git
   cd BusinessManagement

2. Ejecuta Docker Compose:
   docker-compose up --build -d
(Esto compilara la imagen de la WebApi y levantara el contenedor de SQL Server)

3. Verifica contenedores en ejecucion:
   docker-compose ps

4. Accede a la WebApi: http://localhost:8090/swagger
   (Swagger UI para probar los endpoints)

5. Detener la ejecucion:
   docker-compose down


## Uso de la Aplicaci�n (Endpoints y Ejemplos)

## Uso de la Aplicaci�n

La API expone varios endpoints, por ejemplo:

### Obtener todos los productos
GET /api/products

### Crear un producto
POST /api/Products 
Body (JSON): 
{ 
	"name": "Laptop",
	"description": "Dell Inspiron 15",
	"price": 999.99,
	"stock": 10
}

### Obtener un producto por ID
GET /api/Products/{id}

### Actualizar un producto
PUT /api/Products/{id}
Body (JSON):
{
	"name": "Laptop Updated",
	"description": "Dell Inspiron 15 Updated",
	"price": 1099.99,
	"stock": 7
}

### Eliminar un producto
DELETE /api/Products/{id}

### Customers
GET /api/Customers

### Obtener un cliente por ID
GET /api/Customers/{id}

### Crear un cliente
POST /api/Customers
Body (JSON):
{
  "firstName": "Pedro",
  "lastName": "Guzman",
  "email": "pedro@pedro.com"
}

### Actualizar un cliente
PUT /api/Customers/{id}
Body (JSON):
{
  "firstName": "Pedro Updated",
  "lastName": "Guzman Updated",
  "email": "pedro.updated@pedro.com"
}

### Eliminar un cliente
DELETE /api/Customers/{id}

### Sales
GET /api/Sales

### Obtener una venta por ID
GET /api/Sales/{id}

### Crear una venta
POST /api/Sales
Body (JSON):
{
  "customerId": "GUID-del-cliente",
  "saleDate": "2025-03-22T10:00:00",
  "items": [
    {
      "productId": "GUID-del-producto",
      "quantity": 2,
      "unitPrice": 500.00
    }
  ]
}

### Actualizar una venta
PUT /api/Sales/{id}
Body (JSON):
{
  "saleDate": "2025-03-22T10:00:00",
  "items": [
	{
	  "productId": "GUID-del-producto",
	  "quantity": 1,
	  "unitPrice": 800.00
	}
  ]
}

### Eliminar una venta
DELETE /api/Sales/{id}

### Autenticaci�n / Login
POST /api/Auth/Login
Body (JSON):
{
  "username": "admin",
  "password": "1234"
}
-**Nota**: La autenticaci�n es b�sica y solo valida el usuario "admin" con contrase�a "1234" igualmente se puede acceder con el usuario "it" y contrase�a "9999" (Esto es solo para demostraci�n)

### Hay un seeding de datos de demo (DbInitializer.cs) que crea algunos productos y clientes iniciales.)


## Historias y Logros T�cnicos

- **Contenedorizaci�n completa**: Logr� empaquetar la WebApi y la DB en contenedores separados, orquestados con Docker Compose, facilitando la instalaci�n y la demo ante reclutadores.
- **Resoluci�n de problemas**: Originalmente, la imagen SQL 2022 daba errores por permisos no-root. Migr� a la imagen 2019-latest, document� el proceso y resolv� el conflicto.
- **Arquitectura Limpia y SOLID**: Separ� en capas (Domain, Application, Infrastructure, WebApi) para mantener mantenibilidad y testabilidad.
- **Seeding de datos**: Implement� un DbInitializer que inyecta clientes y productos b�sicos al arrancar. Esto permite que cualquiera pruebe la API sin pasos adicionales.
- **Futuras mejoras**:
  - A�adir un volumen persistente para conservar datos entre reinicios de contenedores.
  - Implementar un pipeline de CI/CD para automatizar la construcci�n y publicaci�n de im�genes en Docker Hub.


## Posibles Mejoras Futuras

- **Volumen persistente**: Montar `/var/opt/mssql/data` en un volumen para no perder datos.
- **Autenticaci�n JWT**: Si mas adelante se requiere seguridad real, implementar un AuthController con JWT.
- **Despliegue en la nube**: Subir la imagen a Azure Container Registry y ejecutar en un Azure App Service o Kubernetes.
- **Pruebas Unitarias**: A�adir un proyecto `BusinessManagement.Tests` con xUnit para validar la l�gica de dominio.

## Cr�ditos / Referencias

- [EF Core Docs](https://learn.microsoft.com/ef/core)
- [Docker Docs](https://docs.docker.com/)
- [SQL Server en contenedores](https://learn.microsoft.com/sql/linux/sql-server-linux-overview)
