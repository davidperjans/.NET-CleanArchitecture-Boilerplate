# Clean Architecture API Boilerplate

Ett robust API-boilerplate byggt med .NET 10 och Clean Architecture, med fokus på separation av intressen, testbarhet och underhållbarhet.

## 🎯 Syfte och Översikt

Detta projekt fungerar som en grundläggande stomme för att bygga moderna, skalbara webb-API:er. Det demonstrerar implementeringen av:

* **Clean Architecture:** För att tydligt separera domänlogik från infrastruktur och UI.
* **Generic Repository Pattern:** För att abstrahera datalagring och säkerställa en enhetlig CRUD-operation (Create, Read, Update, Delete) över olika entiteter.
* **CQRS (Command Query Responsibility Segregation) med MediatR:** För att separera läs- och skrivoperationer, vilket leder till en mer hanterbar och prestandaoptimerad applikationslogik.

Projektet är konfigurerat för snabb utveckling med förinstallerade och uppsatta nyckelbibliotek som Entity Framework Core, JWT-autentisering, FluentValidation och AutoMapper.

## ⚙️ Krav och Kom igång

### Förutsättningar

* **.NET 10 SDK:** Krävs för att bygga och köra projektet.
* **SQL Server:** En databasserver för Entity Framework Core.
* **Git:** För kloning av repot.

### Installation och Uppstart

1.  **Klona repot:**
    ```bash
    git clone [DITT REPO URL]
    cd [PROJEKTNAMN]
    ```

2.  **Uppdatera Connection String:**
    Öppna `appsettings.json` i projektet **Api** och uppdatera databasens anslutningssträng:

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=...;Database=...;User Id=...;Password=...;"
    }
    ```

3.  **Kör Migreringar:**
    Projektet är förberett med en `DbContext` och initiala migreringar. Se till att du är i root-mappen och kör:

    ```bash
    make migration name=MigrationName
    ```

    > **Obs:** För att använda en `Makefile` måste du ha **GNU Make** installerat på ditt system.

4. **Uppdatera databasen:**
   ```bash
   make migrate
   ```

5.  **Starta API:t:**
    ```bash
    make run-api
    ```
    API:t startar på `http://localhost:5118` eller `https://localhost:7053`. Du kan sedan använda den inbyggda Swagger-dokumentationen för att testa endpoints.

## 🏗️ Arkitektur

Projektet följer **Clean Architecture** och är uppdelat i följande lager (projekt):

1.  **Domain (Kärnan):**
    * Innehåller alla **entiteter**, **värdeobjekt**, **interfaces** för datalagring (`IRepository<T>`) och **domänspecifika regler**.
    * **Helt oberoende** av andra lager.

2.  **Application (Affärslogik):**
    * Innehåller **Use Cases** (implementerade som MediatR **Queries** och **Commands**).
    * Har **Validations** (med FluentValidation) och **Mappningar** (med AutoMapper).
    * Definierar interfaces som ska implementeras av Infrastruktur-lagret (t.ex. `IJwtService`).
    * **Beroende av:** *Domain*.

3.  **Infrastructure (Implementation):**
    * Innehåller **implementationer** av interfaces definierade i Domain och Application.
    * Hanterar **datalagring** (Entity Framework Core), **externa tjänster** (t.ex. JWT-generering), och **konfiguration**.
    * **Beroende av:** *Domain*, *Application*.

4.  **Api (Presentation/Startpunkt):**
    * **Webb-API:et** som exponerar endpoints (Controllers).
    * Mottar HTTP-anrop, skickar **Commands/Queries** till *Application*-lagret och returnerar resultat.
    * Hanterar **Dependency Injection** för hela applikationen.
    * **Beroende av:** *Application*.

### Generic Repository och Varför

Vi använder ett **Generic Repository Pattern** för att:

* **Abstrahera Datalagring:** Vår affärslogik (i Application) behöver inte veta om vi använder SQL Server, MongoDB eller något annat. Den interagerar bara med `IRepository<T>`.
* **Enhetliga Operationer:** Alla entiteter får grundläggande CRUD-funktionalitet (t.ex. `AddAsync`, `GetByIdAsync`, `ListAllAsync`) automatiskt genom att ärva från bastypet `Repository<T>`.
* **Minskad Kodduplicering:** Vi skriver inte om samma databasåtkomstlogik för varje entitet.

## 🔒 Grundläggande Funktionalitet

* **Autentisering:** Endpoints för att skapa konto (`Register`) och logga in (`Login`) en enkel `User`.
* **Hashing:** BCrypt används för säker lösenordshashning.
* **Validering:** `RegisterUserValidator` (med FluentValidation) demonstrerar inmatningsvalidering.
* **Pipeline Behavior:** En MediatR Pipeline Behavior är satt för centraliserad **Logging** och **Validation** *innan* en Command/Query exekveras.
