# BusinessManagement - Aplicación ASP.NET Core Contenedorizada

![Docker](https://img.shields.io/badge/docker-ready-blue?logo=docker)
![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet?logo=dotnet)
![License](https://img.shields.io/badge/license-MIT-green)

Aplicación de gestión empresarial (CRUD de clientes, productos, ventas, inventarios) basada en Clean Architecture, contenedores Docker y SQL Server. Incluye autenticación, seeding de datos y despliegue sencillo.

---

## Tabla de Contenidos
1. [Descripción General](#descripción-general)
2. [Arquitectura y Diagrama](#arquitectura-y-diagrama)
3. [Requisitos Previos](#requisitos-previos)
4. [Instalación y Ejecución](#instalación-y-ejecución)
5. [Uso de la API](#uso-de-la-api)
6. [Pruebas](#pruebas)
7. [Mejoras Futuras](#mejoras-futuras)
8. [Créditos y Referencias](#créditos-y-referencias)

---

## Descripción General

- **Framework:** ASP.NET Core 8, Clean Architecture, SOLID
- **Persistencia:** EF Core, SQL Server 2019 (contenedor Docker)
- **Orquestación:** Docker Compose
- **Autenticación:** Básica (usuarios demo)
- **Seeding:** Datos de ejemplo para pruebas inmediatas

Imagen disponible en [Docker Hub](https://hub.docker.com/repository/docker/26jeanca/businessmanagement-webapi/general)

---

## Arquitectura y Diagrama

El proyecto sigue los principios de Clean Architecture, organizando el código en capas con responsabilidades específicas y manteniendo las dependencias hacia el centro del diagrama:

```mermaid
flowchart TB
    subgraph Presentación
        API[API Controllers]
        Swagger[Swagger UI]
        Auth[Authentication]
        API --> Swagger
        Auth --> API
    end

    subgraph Application
        Services[Application Services]
        DTOs[DTOs / ViewModels]
        Commands[Commands / Queries]
    end

    subgraph Domain
        Entities[Domain Entities]
        ValueObj[Value Objects]
        Rules[Business Rules]
    end

    subgraph Infrastructure
        Repos[Repositories]
        DB[(SQL Server)]
        EF[Entity Framework]
    end

    API --> Services
    Services --> DTOs
    Services --> Commands
    Commands --> Entities
    Services --> Entities
    Entities --> ValueObj
    Entities --> Rules
    Repos --> DB
    EF --> DB
    Services --> Repos
```

### Descripción de Capas

1. **Capa de Presentación**
   - Controllers REST API
   - Filtros y Middleware
   - Autenticación básica
   - Documentación Swagger

2. **Capa de Aplicación**
   - Servicios de aplicación
   - DTOs y ViewModels
   - Comandos (Commands)
   - Consultas (Queries)
   - Mapeos AutoMapper

3. **Capa de Dominio**
   - Entidades core (Customer, Product, Sale, etc.)
   - Objetos de valor
   - Reglas de negocio
   - Interfaces de repositorio

4. **Capa de Infraestructura**
   - Implementación de repositorios
   - Contexto de EF Core
   - Migraciones
   - Configuración de SQL Server

### Flujo de Datos
1. Las peticiones HTTP llegan a los controllers
2. Los controllers usan servicios de aplicación
3. Los servicios implementan la lógica de negocio usando entidades
4. Los repositorios persisten los datos en SQL Server
5. Las respuestas son mapeadas a DTOs y devueltas al cliente

---

## Requisitos Previos

- **Docker Desktop** (o Docker Engine + Docker Compose)
- **Git**
- **.NET SDK 8** (solo si deseas compilar fuera de Docker)

---

## Instalación y Ejecución

1. Clona el repositorio:
   ```sh
   git clone https://github.com/jeancadev/BusinessManagement.git
   cd BusinessManagement
   ```
2. Construye y levanta los contenedores:
   ```sh
   docker-compose up --build -d
   ```
3. Verifica los contenedores:
   ```sh
   docker-compose ps
   ```
4. Accede a la API: [http://localhost:8090/swagger](http://localhost:8090/swagger)
5. Detén la ejecución:
   ```sh
   docker-compose down
   ```

---

## Uso de la API

### Autenticación
- Usuario: `admin` / Contraseña: `1234`
- Usuario: `it` / Contraseña: `9999`

### Endpoints Principales

#### Productos
- `GET    /api/Products` — Listar productos
- `POST   /api/Products` — Crear producto
- `GET    /api/Products/{id}` — Obtener producto
- `PUT    /api/Products/{id}` — Actualizar producto
- `DELETE /api/Products/{id}` — Eliminar producto

#### Clientes
- `GET    /api/Customers` — Listar clientes
- `POST   /api/Customers` — Crear cliente
- `GET    /api/Customers/{id}` — Obtener cliente
- `PUT    /api/Customers/{id}` — Actualizar cliente
- `DELETE /api/Customers/{id}` — Eliminar cliente

#### Ventas
- `GET    /api/Sales` — Listar ventas
- `POST   /api/Sales` — Crear venta
- `GET    /api/Sales/{id}` — Obtener venta
- `PUT    /api/Sales/{id}` — Actualizar venta
- `DELETE /api/Sales/{id}` — Eliminar venta

#### Ejemplo de creación de producto
```json
POST /api/Products
{
  "name": "Laptop",
  "description": "Dell Inspiron 15",
  "price": 999.99,
  "stock": 10
}
```

#### Ejemplo de creación de cliente
```json
POST /api/Customers
{
  "firstName": "Pedro",
  "lastName": "Guzman",
  "email": "pedro@pedro.com"
}
```

#### Ejemplo de creación de venta
```json
POST /api/Sales
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
```

---

## Pruebas

- El proyecto incluye pruebas unitarias en `BusinessManagement.UnitTests`.
- Para ejecutarlas localmente:
  ```sh
  dotnet test BusinessManagement.UnitTests/BusinessManagement.UnitTests.csproj
  ```

---

## Mejoras Futuras

- Volumen persistente para SQL Server
- Autenticación JWT
- CI/CD con GitHub Actions
- Despliegue en Azure/Kubernetes
- Más cobertura de pruebas

---

## Créditos y Referencias

- [EF Core Docs](https://learn.microsoft.com/ef/core)
- [Docker Docs](https://docs.docker.com/)
- [SQL Server en contenedores](https://learn.microsoft.com/sql/linux/sql-server-linux-overview)
