# TODO App – Backend

ASP.NET Core 8 Web API pro plnohodnotnou TODO aplikaci s JWT autentizací. Slouží jako backend pro [TODOApp.Frontend](https://github.com/JanStefko/TODOApp.Frontend).

🌐 **Live frontend:** [todo.janstefko.cz](https://todo.janstefko.cz)
🔗 **API base URL:** `https://todoapp-api-123-ghe5a3a6fbasb2cf.westeurope-01.azurewebsites.net/api`

> ⏱️ První request po nečinnosti může trvat 30-60 sekund – aplikace běží na Azure App Service Free tier (cold start) a Azure SQL Database s auto-pause.

---

## ✨ Funkce

- **Registrace a přihlášení** uživatelů (ASP.NET Identity + JWT)
- **CRUD operace** nad TODO položkami
- **Izolace dat** – každý uživatel vidí pouze své úkoly
- **Hashování hesel** přes PBKDF2 (Identity)
- **JWT tokeny** s claim-based autorizací
- **Protected endpoints** přes `[Authorize]` atribut
- **Konfigurovatelný CORS** – lokální i produkční prostředí
- **Resilient connection** k databázi přes `EnableRetryOnFailure` (Azure SQL auto-pause)

---

## 🛠️ Tech stack

- **.NET 8** (LTS)
- **ASP.NET Core Web API**
- **Entity Framework Core 8**
- **ASP.NET Core Identity** – správa uživatelů a hesel
- **JWT Bearer Authentication**
- **SQLite** – lokální vývoj
- **Azure SQL Database** – produkce
- **xUnit + Moq** – unit testy
- **Swagger / OpenAPI** – API dokumentace

---

## 🏗️ Architektura

Vrstvená architektura:

```text
TODOApp/
├── Controllers/        # HTTP endpointy (AuthController, TodosController)
├── Services/           # Aplikační logika (AuthService, JwtService)
├── Repositories/       # Přístup k databázi (TodoRepository)
├── Models/             # Doménové entity (TodoItem, ApplicationUser)
├── DTOs/               # Request/response objekty
├── Data/               # AppDbContext, DbInitializer (seed)
├── Migrations/         # EF Core migrace
└── Program.cs          # Konfigurace, DI, middleware pipeline

TODOApp.Tests/          # Unit testy (xUnit + Moq)
```

**Použité návrhové vzory:**
- Repository pattern (oddělení DB logiky)
- Service layer (business logika)
- DTO pattern (API kontrakty)
- Dependency Injection (vestavěné v ASP.NET Core)
- Result pattern (`AuthResult` pro výsledky operací)

---

## 🔌 API endpointy

### Autentizace

| Metoda | Endpoint | Popis | Auth |
|---|---|---|---|
| `POST` | `/api/Auth/register` | Registrace nového uživatele | ❌ |
| `POST` | `/api/Auth/login` | Přihlášení (vrací JWT) | ❌ |

### TODO

| Metoda | Endpoint | Popis | Auth |
|---|---|---|---|
| `GET` | `/api/Todos` | Seznam úkolů přihlášeného uživatele | ✅ |
| `GET` | `/api/Todos/{id}` | Detail jednoho úkolu | ✅ |
| `POST` | `/api/Todos` | Vytvoření nového úkolu | ✅ |
| `PUT` | `/api/Todos/{id}` | Aktualizace úkolu | ✅ |
| `DELETE` | `/api/Todos/{id}` | Smazání úkolu | ✅ |

Chráněné endpointy vyžadují hlavičku `Authorization: Bearer <token>`.

---

## 🚀 Lokální vývoj

### Předpoklady

- .NET 8 SDK
- Visual Studio 2022 nebo VS Code
- (volitelně) [TODOApp.Frontend](https://github.com/JanStefko/TODOApp.Frontend) pro plný stack

### Postup

```bash
# Klonování
git clone https://github.com/JanStefko/TODOApp.Backend.git
cd TODOApp.Backend

# Obnovení balíčků
dotnet restore

# Aplikace migrací (vytvoří lokální SQLite databázi)
dotnet ef database update --project TODOApp.csproj

# Spuštění
dotnet run --project TODOApp.csproj
```

API poběží na `https://localhost:7085`.
Swagger UI: `https://localhost:7085/swagger`.

### Demo uživatel

Po prvním spuštění je v databázi vytvořen seed účet:

```
Email:    demo@todoapp.cz
Heslo:    Demo123!
```

---

## 🧪 Testy

```bash
dotnet test
```

Aktuálně pokrytí: 3 testy pro `AuthService.RegisterAsync` (xUnit + Moq).

---

## 🌐 Nasazení

Aplikace je nasazena na **Azure App Service Linux F1** (Free tier) s **Azure SQL Database**.

**Infrastruktura:**
- **App Service:** Azure App Service Linux F1
- **Databáze:** Azure SQL Database (Free tier, serverless s auto-pause)
- **Secrets:** Application Settings (JWT klíč, connection string)
- **HTTPS:** automatický certifikát od Azure
- **CORS:** konfigurovatelný přes `appsettings.json` + Application Settings

**Konfigurace databázového provideru** v `Program.cs` automaticky vybere podle connection stringu:
- SQLite (lokálně)
- SQL Server / Azure SQL (produkce)

---

## 🔐 Bezpečnost

- **Hesla:** hashovaná přes PBKDF2 (ASP.NET Identity default)
- **JWT:** podepsaný HMAC-SHA256, expirace 60 minut
- **Secrets v produkci:** Azure Application Settings (env variables), nikdy v gitu
- **Lokální secrets:** `dotnet user-secrets`

---

## 🔗 Související

- **Frontend:** [TODOApp.Frontend](https://github.com/JanStefko/TODOApp.Frontend)
- **Live aplikace:** [todo.janstefko.cz](https://todo.janstefko.cz)