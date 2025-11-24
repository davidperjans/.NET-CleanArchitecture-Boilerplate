# Changelog

Alla betydande förändringar i detta projekt kommer att dokumenteras i denna fil.

## 2025-11-24

### 📦 Initial Boilerplate

* Initial boilerplate skapades med .NET 8, Clean Architecture, och fyra separata projekt (Api, Application, Domain, Infrastructure).

### ✨ Nya Funktioner

* Lade till grundläggande **Autentiserings-endpoints** (`/auth/register`, `/auth/login`).

### 🏗️ Arkitektur & Teknik

* Konfigurerade **Generic Repository Pattern** för enhetlig CRUD.
* Konfigurerade **MediatR** för CQRS-implementering.
* Implementerade **MediatR Pipeline Behaviors** för centraliserad loggning och validering.
* Satte upp **FluentValidation** för inputvalidering (t.ex. `RegisterUserValidator`).
* Konfigurerade **BCrypt** för säker lösenordshashning.
* Implementerade **JWT Service** för token-generering och autentisering.
* Konfigurerade Entity Framework Core med SQL Server och initial `User`entitet.
