# TODO Web App

Jednoduchá TODO webová aplikace postavená jako full-stack projekt.

## Tech stack

### Backend
- ASP.NET Core Web API
- Entity Framework Core
- MSSQL
- Repository pattern
- Service layer
- DTOs
- Dependency Injection

### Frontend
- React
- Tailwind CSS

## Aktuální stav

Momentálně je hotový backend:
- CRUD operace pro TODO položky
- Entity, DTOs, Repository, Service, Controller
- EF Core migrace
- MSSQL databáze
- Swagger pro testování API

Frontend bude doplněn později.

## API endpointy

Aktuálně backend obsahuje tyto endpointy:

- `GET /api/Todos`
- `GET /api/Todos/{id}`
- `POST /api/Todos`
- `PUT /api/Todos/{id}`
- `DELETE /api/Todos/{id}`

## Spuštění backendu

1. Otevři projekt v Visual Studiu.
2. Zkontroluj connection string v `appsettings.json`.
3. Spusť migrace databáze.
4. Spusť aplikaci.
5. Swagger bude dostupný na adrese:
   - `https://localhost:7085/swagger`

## Datový model

TODO položka obsahuje:
- `Id`
- `Title`
- `IsCompleted`

## Poznámka

Tento projekt slouží jako tréninkový full-stack projekt pro procvičení ASP.NET Core Web API a React frontendu.