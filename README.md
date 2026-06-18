# Revenue Recognition System

A REST API built for a corporate client (ABC Corp) to manage software license sales and revenue recognition — modeling the business rule that **receiving a payment is not the same as recognizing it as revenue**.

## Overview

In real-world business systems, money received can't always be counted as revenue the moment it arrives. It depends on what was paid for, when the related service will actually be delivered, and what rules the company follows. A simple purchase is straightforward, but services delivered over time — subscriptions, multi-year contracts — require careful separation between money *received*, money *owed*, and money that can actually be treated as *recognized revenue*.

This system encodes that distinction directly into the domain logic: creating a contract or offer does **not** by itself generate revenue. Revenue is only recognized once specific business conditions are met — most importantly, full and on-time payment of a signed contract.

## Key Features

- **Customer Management** — Supports both individual customers (PESEL) and companies (KRS), with immutable legal identifiers. Individual customers can be soft-deleted; company records cannot be deleted.
- **Software Catalog** — Tracks software products by name, description, version, and category, sold as one-time licenses with update access.
- **License Contracts** — Generates purchase offers for one-time software licenses (valid for 3–30 days), each guaranteeing at least 1 year of updates, with optional paid support extensions (+1000 PLN per additional year, up to 3 years).
- **Discount Engine** — Applies time-bound percentage discounts and a stackable 5% loyalty discount for returning customers; always selects the highest applicable discount when multiple apply.
- **Payments** — Supports single or installment payments per contract, validates payment totals against the contract amount, and automatically voids/refunds offers that miss their payment deadline.
- **Revenue Calculation** — Computes both *current* revenue (money actually earned and recognized) and *projected* revenue (assuming pending contracts get signed), scoped to the whole company or a single product, with optional currency conversion.
- **Role-Based Access Control** — All endpoints require employee authentication. Editing or deleting customer records is restricted to Admin users; all other operations are available to standard users.

## Tech Stack

- ASP.NET Core Web API — .NET 9
- Entity Framework Core (Code-First) — SQL Server (runtime), SQLite (test database)
- Swagger / OpenAPI for interactive API documentation and testing
- xUnit for unit testing business logic
- JWT-based authentication with role-based authorization

## Architecture

The project follows a layered architecture separating API controllers, domain models, business logic (discount rules, revenue calculation, contract validation), and data access (EF Core, code-first migrations) — keeping business rules testable independently of the HTTP layer.

## Getting Started

### Prerequisites
- .NET 9 SDK
- SQL Server instance (or LocalDB)

### Setup
```bash
git clone https://github.com/xdino241/system_uznawania_przychodow.git
cd system_uznawania_przychodow
dotnet restore
dotnet ef database update
dotnet run
```

Then open `https://localhost:5075/swagger` to explore and test the API endpoints interactively.

### Running Tests
```bash
dotnet test
```
Tests run against a SQLite database, so no SQL Server instance is required to run the test suite.

## Project Structure

```
system_uznawania_przychodow/
├── Configurations/   # App/service configuration
├── Controllers/       # REST API endpoints
├── Data/              # EF Core DbContext & DB setup
├── DTOs/              # Data transfer objects for API requests/responses
├── Entities/          # Domain models (Customer, Contract, Payment, ...)
├── Exceptions/        # Custom exception types
├── Migrations/        # EF Core code-first migrations
├── Services/          # Business logic: discounts, revenue calculation, contract rules
├── appsettings.json
└── Program.cs

Unit_tests/
├── ClientServiceTest.cs
├── ContractServiceTest.cs
└── TestDbContext.cs
```

## Notes

This project was built as part of a university database/backend systems course, with a focus on domain modeling, business rule correctness, and clean REST API design rather than UI work.
