# 📚 BookClub

En webbaserad bokklubbsapp byggd med ASP.NET Core Razor Pages och Bootstrap 5.

## Funktioner

- Registrera och logga in
- Skapa bokklubbar med automatisk inbjudningskod
- Gå med i klubbar via kod
- Lägg till böcker i klubbar med status (Vill läsa / Läser / Klar)
- Diskutera böcker med andra medlemmar

## Tech Stack

- ASP.NET Core Razor Pages (.NET 8)
- Entity Framework Core + SQLite
- ASP.NET Identity (autentisering)
- Bootstrap 5

## Kom igång

### Krav
- .NET 8 SDK
- Visual Studio 2022

### Installation

1. Klona repot
```
   git clone https://github.com/Rindertanijk1997/BookClub.git
```

2. Navigera till projektmappen
```
   cd BookClub/Bookclub
```

3. Kör migrationer
```
   dotnet ef database update
```

4. Starta projektet
```
   dotnet run
```

5. Öppna `https://localhost:5001` i webbläsaren

## Projektstruktur
```
Bookclub/
├── Models/          # Datamodeller (Club, Book, Membership, Discussion)
├── Pages/
│   └── Clubs/       # Klubb- och boksidor
├── Data/            # ApplicationDbContext
├── Areas/Identity/  # Autentiseringssidor
└── wwwroot/         # CSS och statiska filer
```
