# Specifikationer (SPECS)

Detta dokument beskriver den funktionalitet och de tekniska komponenter som systemet ska innehålla.

## 🚀 Funktionella Krav (Features)

| ID | Feature | Beskrivning |
| :--- | :--- | :--- |
| **AUTH-001** | Användarregistrering | En oregistrerad användare ska kunna skapa ett nytt konto med unikt användarnamn och säkert lösenord. |
| **AUTH-002** | Användarinloggning | En registrerad användare ska kunna logga in med sina uppgifter och erhålla en JWT-token. |

## 🌐 API Endpoints

Alla endpoints nås via bas-URL:en `/api/v1/`.

| HTTP Metod | Endpoint | Beskrivning | Use Case/Command/Query | Kräver Auth |
| :--- | :--- | :--- | :--- | :--- |
| `POST` | `/auth/register` | Skapar en ny användare i systemet. | `RegisterUserCommand` | Nej |
| `POST` | `/auth/login` | Autentiserar användare och returnerar en JWT. | `LoginUserQuery` | Nej |

## 💡 Use Cases (Application Layer)

Use Cases är implementerade som MediatR Commands (för skrivoperationer) och Queries (för läsoperationer).

| Use Case | Typ | Beskrivning |
| :--- | :--- | :--- |
| `RegisterUserCommand` | Command | Hanterar validering av indata, hashar lösenordet och sparar den nya `User`-entiteten via Generic Repository. |
| `LoginUserQuery` | Query | Validerar användarens inloggningsuppgifter, verifierar lösenordet mot hashen (BCrypt) och använder `IJwtService` för att generera en autentiseringstoken. |

## 📐 Begränsningar och Antaganden

* **Autentisering:** För närvarande implementeras en simpel `User` entitet. Inga roller (RBAC) eller avancerade behörighetshanteringar är på plats.
* **Databas:** Använder SQL Server (via EF Core) som primär datalagring. Byten till annan DB (t.ex. PostgreSQL) kräver endast ändringar i *Infrastructure* och *Api* konfigurationen.
* **API Versionering:** Använder en enkel URL-baserad versionering (`/api/v1/`).
* **Validering:** All inmatningsvalidering sker i `Application`-lagret med FluentValidation.
