# DataPoints

DataPoints is an API for transferring internal points between users using a private blockchain-style ledger. This project is developed in C# with .NET 10, DbUp, and Entity Framework (EF) over PostgreSQL (Supabase).

## Description

The goal of this project is to provide a secure and transparent system for distributing points among users. Every transfer is grouped into a block chained to the previous one (hash + Merkle root over the transactions), giving tamper-evidence over the ledger.

## Technologies Used

- **C#**
- **.NET 10**
- **PostgreSQL** (Supabase) via **Npgsql** / Entity Framework Core
- **DbUp** (schema migrations, run automatically on startup)
- **NBitcoin** (ECDSA signatures)
- **Hangfire** (background chain validation)

## Features

- Transfer points between users, authorized by a client-side ECDSA signature (the private key never leaves the client)
- Idempotent transfers (`Idempotency-Key` header) and anti-replay signed payloads
- Transaction history and continuous chain validation
- Row-Level Security (RLS) on every table, in addition to application-level authorization

## How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/GuilhermeNono/DataPoints.git
   ```
2. Navigate to the project directory:
   ```bash
   cd DataPoints
   ```
3. Restore project dependencies:
   ```bash
   dotnet restore
   ```
4. Provide a PostgreSQL/Supabase connection string via User Secrets (never commit it to `appsettings*.json`):
   ```bash
   cd DataPoints.Api
   dotnet user-secrets set "ConnectionStrings:MainDatabase" "Host=...;Port=6543;Database=postgres;Username=app_user;Password=...;SSL Mode=Require"
   dotnet user-secrets set "JwtConfiguration:SecretKey" "<32+ random bytes, base64>"
   dotnet user-secrets set "ChainSigningConfiguration:PublicKey" "<system ECDSA public key, base64>"
   dotnet user-secrets set "ChainSigningConfiguration:PrivateKey" "<system ECDSA private key, base64>"
   ```
   See [DataPoints.Api/appsettings.Development.example.json](DataPoints.Api/appsettings.Development.example.json) for the expected shape.
5. Start the application (schema migrations, including RLS policies, run automatically on boot):
   ```bash
   dotnet run --project DataPoints.Api
   ```

There is no manual migration step (`dotnet ef ...`) — schema changes live in versioned SQL scripts under `DataPoints.Infrastructure/DbUp/Scripts/Postgres/` and are applied by DbUp when the API starts.

## Contribution

If you wish to contribute to this project, please follow these steps:

1. Fork the repository
2. Create a new branch (`git checkout -b feature/new-feature`)
3. Commit your changes (`git commit -m 'Add new feature'`)
4. Push to the branch (`git push origin feature/new-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

Developed by [GuilhermeNono](https://github.com/GuilhermeNono)
