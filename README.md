Hey – projektet er sat op, sådan kommer I i gang:

1. Clone repo + gå ind i mappen

2. Hent nyeste:
   git pull

3. Start database:
   docker compose up -d

4. Opret database/tabeller:
   dotnet ef database update -p Data -s Data

5. (Valgfrit test)
   dotnet run --project App

Hvis noget ikke virker:

* Sørg for Docker kører
* Vi bruger port 5433 (ikke 5432)

Vigtige regler:

* Lav ALDRIG ændringer direkte på main/master

* Lav altid en branch:
  git checkout -b feature/navn

* Når du ændrer database (models):

  1. dotnet ef migrations add Navn -p Data -s Data
  2. dotnet ef database update -p Data -s Data
  3. commit ALLE migrations

* Når du har pulled ny kode:
  dotnet ef database update -p Data -s Data


  ## Database Triggers (EF Core + PostgreSQL)

Denne guide viser, hvordan man korrekt opretter triggers i projektet, så hele teamet får dem automatisk via migrations.

---

### Vigtigt princip

Alle ændringer til databasen skal laves via **Entity Framework migrations**.

Hvis du laver ændringer direkte i databasen (fx via SQL i Rider), vil de kun gælde lokalt og ikke for resten af teamet.

---

## Fremgangsmåde

### 1. Opret en migration

```bash
dotnet ef migrations add AddUserTrigger -p Data -s Data
```

---

### 2. Tilføj SQL til migrationen

Åbn den nye migration i `Data/Migrations/` og rediger `Up()` og `Down()`.

---

## Up() og Down()

### Up()

`Up()` beskriver hvordan databasen ændres fremad.

Det er her du:

* opretter tabeller
* tilføjer kolonner
* opretter triggers og functions

Denne metode bliver kørt når du kører:

```bash
dotnet ef database update -p Data -s Data
```

---

### Down()

`Down()` beskriver hvordan ændringen fortrydes (rollback).

Det er her du:

* sletter det du oprettede i `Up()`
* nulstiller databasen til tidligere tilstand

---

### Regel

Alt du laver i `Up()` skal kunne fjernes i `Down()`.

---

## Eksempel

### Up()

```csharp
migrationBuilder.Sql(@"
CREATE OR REPLACE FUNCTION user_insert_log()
RETURNS TRIGGER AS $$
BEGIN
    RAISE NOTICE 'New user created: %', NEW.""Name"";
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER user_insert_trigger
AFTER INSERT ON ""Users""
FOR EACH ROW
EXECUTE FUNCTION user_insert_log();
");
```

---

### Down()

```csharp
migrationBuilder.Sql(@"
DROP TRIGGER IF EXISTS user_insert_trigger ON ""Users"";
DROP FUNCTION IF EXISTS user_insert_log();
");
```

---

## 3. Opdater databasen

```bash
dotnet ef database update -p Data -s Data
```

---

## 4. Test triggeren

```sql
INSERT INTO "Users" ("Name") VALUES ('Test');
```

---

## 5. Commit ændringer

```bash
git add .
git commit -m "Add user insert trigger"
git push
```

---

## 6. Team workflow

Når andre i teamet henter ændringer:

```bash
git pull
dotnet ef database update -p Data -s Data
```

---

## Regler

* Lav aldrig triggers direkte i databasen
* Undgå manuelle ændringer i schema
* Brug altid migrations
* Commit migrations sammen med kodeændringer

---

## Tips

* Brug `CREATE OR REPLACE FUNCTION` for at undgå fejl ved ændringer
* Brug `IF EXISTS` i `Down()` for sikker rollback
* Hold migrations små og overskuelige

---

## Kort sagt

`Up()` = hvad du tilføjer
`Down()` = hvordan du fjerner det igen

Hvis det ikke er i en migration, eksisterer det ikke for teamet.

