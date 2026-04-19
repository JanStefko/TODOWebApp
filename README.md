# TODO Web App Backend

Backend část jednoduché TODO webové aplikace postavené jako full-stack projekt.  
API je vytvořené v ASP.NET Core Web API a slouží pro správu TODO položek pomocí základních CRUD operací.

## Funkce

- Načtení všech TODO položek
- Načtení detailu jedné TODO položky
- Vytvoření nové TODO položky
- Aktualizace TODO položky
- Smazání TODO položky
- Swagger UI pro testování endpointů

## Tech stack

- ASP.NET Core Web API
- Entity Framework Core
- Microsoft SQL Server
- Repository pattern
- Service layer
- DTOs
- Dependency Injection

## Architektura projektu

Projekt je rozdělený do několika vrstev:

- **Entity** – datový model TODO položky
- **DTOs** – request/response objekty pro API
- **Repository** – přístup k databázi
- **Service** – aplikační logika
- **Controller** – HTTP endpointy
- **DbContext** – komunikace s databází přes Entity Framework Core

## API endpointy

Aktuálně backend obsahuje tyto endpointy:

- `GET /api/Todos` – vrátí seznam všech TODO položek
- `GET /api/Todos/{id}` – vrátí detail jedné TODO položky
- `POST /api/Todos` – vytvoří novou TODO položku
- `PUT /api/Todos/{id}` – aktualizuje existující TODO položku
- `DELETE /api/Todos/{id}` – smaže TODO položku

## Datový model

TODO položka obsahuje:

- `Id`
- `Title`
- `IsCompleted`

## Spuštění projektu

1. Otevři backend projekt v Visual Studiu.
2. Zkontroluj connection string v `appsettings.json`.
3. Ujisti se, že je dostupný SQL Server.
4. Spusť EF Core migrace.
5. Spusť aplikaci.
6. Otevři Swagger UI na adrese:
   - `https://localhost:7085/swagger`

## Frontend

Frontend část aplikace je vytvořená v Reactu a komunikuje s tímto API přes HTTP requesty.  
Frontend používá endpointy uvedené výše a očekává běžící backend na adrese nastavené v jeho `.env` souboru.

## Aktuální stav

Projekt aktuálně obsahuje funkční backend API i samostatný frontend.  
Backend podporuje základní CRUD operace nad TODO položkami a slouží jako tréninkový full-stack projekt pro procvičení ASP.NET Core Web API, Entity Framework Core a Reactu.

## Poznámky

- Databáze je postavená nad MSSQL.
- API lze pohodlně testovat přes Swagger.
- Projekt je určený primárně jako studijní a portfolio ukázka.