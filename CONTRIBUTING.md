# Bidragsguide (Contributing)

Denna guide beskriver hur man förväntas arbeta med detta projekt för att säkerställa ett enhetligt och underhållbart kodbas.

## 🌳 Branching-strategi

Vi använder en förenklad **Git Flow/Feature Branching**-strategi:

1.  **`main`:** Endast stabil, produktionsfärdig kod. Inga direkta commits till `main`.
2.  **`develop`:** Huvudbranch för integration av slutförda features.
3.  **Feature Branches:** Alla nya funktioner eller buggfixar ska utvecklas i en egen branch utgången från `develop`.

### Namngivning av Branches

Använd ett prefix följt av en kort, beskrivande titel.

| Prefix | Syfte | Exempel |
| :--- | :--- | :--- |
| `feature/` | Ny funktionalitet | `feature/add-todo-endpoints` |
| `fix/` | Buggfixar | `fix/login-bug-bcrypt` |
| `refactor/` | Kodförbättringar utan funktionsändring | `refactor/improve-di-config` |

## 🛠️ Pull Requests (PRs)

När en feature är färdig:

1.  **Gör en Pull Request** från din `feature/`-branch till `develop`.
2.  **Beskriv:** Inkludera en tydlig beskrivning av vad PR:en gör, vilka problem den löser, och om den refererar till något i `specs.md`.
3.  **Granskning:** Alla PR:er måste granskas (Code Review) och godkännas av minst en annan utvecklare innan de kan slås samman (`merge`).

## ✍️ Kodstil och Konventioner

* **C# Naming Conventions:** Följ standard .NET konventioner (PascalCase för klasser/metoder/properties, camelCase för lokala variabler).
* **Explicit Access Modifiers:** Använd alltid explicita access modifiers (`private`, `public`, `internal`).
* **Typning:** Var tydlig med typning. Använd `var` sparsamt när typen är uppenbar.
* **Asynkron Kod:** Använd suffixet `Async` på alla metoder som returnerar en `Task` eller `Task<T>`.

## ✅ Tester

Alla affärslogiker (Use Cases i Application-lagret) ska ha tillhörande enhetstester.
* **Fokus:** Testa `CommandHandlers` och `QueryHandlers`.
* **Mocking:** Använd mocking-bibliotek (t.ex. Moq) för att isolera Application-logiken från databasen (Repository interfaces).
