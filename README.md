# Web API for Contacts 2.0

Αυτό είναι ένα ASP.NET Core Web API για διαχείριση επαφών, με υποστήριξη για χώρες, επαγγέλματα και χόμπι.

Δομή Project

- `Controllers/`: API controllers για κάθε οντότητα (Person, Country, Hobby κ.λπ.)
- `Data/`: EF Core `DbContext`
- `Dtos/`: Data Transfer Objects
- `Models/`: Οντότητες της βάσης
- `Profiles/`: AutoMapper profiles
- `Migrations/`: EF Core migration history (προαιρετικά)

Προαπαιτούμενα

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) ή νεότερο
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ή VS Code με C# extension

Εκκίνηση Development

```bash
git clone https://github.com/MichaelKolliniatis/API-for-Contacts
cd Web-API-for-Contacts-2.0
dotnet restore
dotnet ef database update
dotnet run
