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
