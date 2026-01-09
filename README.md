# EsAppTest

API REST desarrollada como **prueba técnica** para el registro y consulta de pagos
de servicios básicos (agua, electricidad, telecomunicaciones).

La solución aplica **arquitectura en capas**, validaciones de negocio y persistencia
en base de datos relacional usando SQL Server en Docker.

---

## 🧱 Arquitectura

La solución está organizada en tres capas principales:

    src/
     ├─ EsAppTest.Api              → Capa API (FastEndpoints, Swagger)
     ├─ EsAppTest.Core             → Dominio y reglas de negocio
     └─ EsAppTest.Infrastructure   → Persistencia (EF Core, SQL Server)
    docker/
     └─ sqlserver.yml              → SQL Server 2022 en Docker


### Descripción de capas

- **Api**
  - Expone los endpoints HTTP
  - Define DTOs de Request / Response
  - Maneja mapeos con AutoMapper
  - Documentación vía Swagger

- **Core**
  - Entidades de dominio
  - Servicios de negocio
  - Validaciones y reglas (independientes de HTTP)

- **Infrastructure**
  - Entity Framework Core
  - DbContext
  - Repositorios
  - Migraciones de base de datos

---

## 🛠️ Stack Tecnológico

- .NET 8
- FastEndpoints
- Swagger / OpenAPI
- Entity Framework Core 8
- SQL Server 2022
- Docker
- AutoMapper

---

## ✅ Requisitos funcionales implementados

### Registrar un pago
**POST** `/api/payments`

- Guarda el pago en base de datos
- Estado inicial: **pendiente**
- Rechaza montos mayores a **1500 Bs**
- Rechaza moneda distinta de **BOB**

### Consultar pagos
**GET** `/api/payments?customerId=...`

- Devuelve los pagos asociados a un cliente
- Ordenados por fecha de creación descendente

---

## 🚀 Cómo ejecutar el proyecto

### 1️⃣ Levantar SQL Server con Docker

```bash
docker compose -f docker/sqlserver.yml up -d
```
### 2️⃣ Aplicar migraciones (crear base de datos y tablas)
```bash
dotnet ef database update \
  --project src/EsAppTest.Infrastructure \
  --startup-project src/EsAppTest.Api
```
### 3️⃣ Ejecutar la API
```bash
dotnet run --project src/EsAppTest.Api
```
### 📑 Swagger
```bash
http://localhost:{PUERTO}/swagger
```
## 🔌 Endpoints
### Registrar un pago
**POST** `/api/payments`
### Ejemplos:
```bash
{
  "customerId": "cfe8b150-2f84-4a1a-bdf4-923b20e34973",
  "serviceProvider": "SERVICIOS ELÉCTRICOS S.A.",
  "amount": 120.50,
  "currency": "BOB"
}
```
```bash
{
  "customerId": "cfe8b150-2f84-4a1a-bdf4-923b20e34973",
  "serviceProvider": "SERVICIOS ELÉCTRICOS S.A.",
  "amount": 1600.00,
  "currency": "BOB"
}
```
### Consultar pagos
**GET** `/api/payments?customerId=...`
### Ejemplo:
```bash
/api/payments?customerId=cfe8b150-2f84-4a1a-bdf4-923b20e34973
```
### Respuesta:
```bash
[
  {
    "paymentId": "a248ad43-1f44-4b32-b0a0-e1c725b9bb7d",
    "serviceProvider": "SERVICIOS ELÉCTRICOS S.A.",
    "amount": 120.50,
    "status": "pendiente",
    "createdAt": "2025-07-17T08:30:00Z"
  }
]
```


