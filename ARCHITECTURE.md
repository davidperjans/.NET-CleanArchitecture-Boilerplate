# Arkitektur och Dataflöde

Detta projekt är strikt byggt enligt **Clean Architecture** för att garantera låg koppling (low coupling) och hög sammanhållning (high cohesion).

## Kärnprincip

Beroenden flödar alltid inåt. De inre cirklarna (Domain) känner inte till de yttre (Infrastructure, API).

## 🗺️ Dataflödesöversikt

Ett typiskt flöde för en skrivoperation (Command) ser ut så här:

1.  **API (Presentation):**
    * Tar emot ett HTTP `POST`-anrop (t.ex. `/api/v1/auth/register`).
    * Kontrollern skickar en motsvarande **Command** (t.ex. `RegisterUserCommand`) till **MediatR**.

2.  **Application (Affärslogik och Kontrakt):**
    * **Gränssnitt/Kontrakt:** Här **definieras** alla interfaces som krävs av affärslogiken, inklusive det generiska datalagringsgränssnittet **`IRepository<T>`** och tjänstgränssnitt (t.ex. `IJwtService`).
    * **Use Cases:** Command/Query Handlers ligger här.
    * **Pipeline Behavior (Validation/Logging):** Begäran genomgår validering och loggning.
    * **Command Handler:** `RegisterUserCommandHandler` tar emot Commandet.
    * **Data Access:** Handlern kräver en instans av **`IRepository<User>`** (som är definierat i detta lager) injicerad i konstruktorn.

3.  **Domain (Kärnan):**
    * Innehåller rena **entiteter** (t.ex. `User`, `Todo`) och eventuella Enums.
    * **Har inga beroenden** till Application, Infrastructure eller API.

4.  **Infrastructure (Implementation):**
    * **Implementerar Gränssnitten:** Detta lager har ett beroende till Application och **implementerar** de interfaces som definierats där.
    * En konkret klass, **`Repository<T>`**, implementerar **`IRepository<T>`** och använder Entity Framework Core för att kommunicera med databasen.
    * **Tjänstimplementationer:** Konkreta klasser som `JwtService` implementerar `IJwtService`.
    * **Dependency Injection (DI):** I konfigurationen knyts varje interface i Application-lagret till sin konkreta implementation i Infrastructure-lagret (t.ex. `IRepository<User>` mappas till `Repository<User>`).

5.  **Retur:** Resultatet skickas tillbaka genom lagren till API:et och sedan till klienten.

**Sammanfattning av Repository-flödet:**

* **Interfaces (Application):** `IRepository<T>` definieras.
* **Implementationer (Infrastructure):** `Repository<T>` implementerar `IRepository<T>`.
* **DI-konfiguration:** Kopplar ihop de två.
* **Use Cases (Application):** Förbrukar `IRepository<T>` (t.ex. `IRepository<User>`).
