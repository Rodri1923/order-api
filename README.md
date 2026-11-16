# 🧾 Order API

🌍 **[English](README.md) · [Español](README.es.md)**

[![CI](https://github.com/Rodri1923/order-api/actions/workflows/ci.yml/badge.svg)](https://github.com/Rodri1923/order-api/actions/workflows/ci.yml)

> REST API for order management built with ASP.NET Core, focused on clean layered architecture and automated testing.

## 📋 Overview
A backend project built to practice REST API design in .NET: layered architecture (Controllers, Services, Models), input validation, and a solid testing strategy including unit tests, integration tests, and mutation testing to verify the tests themselves catch real bugs.

## 🚀 Features
- Create and list orders (carts) with product line items
- Input validation: cart must have at least one item, quantity > 0, price > 0
- Automatic total calculation
- Proper HTTP status handling (200 OK / 400 Bad Request)
- Layered architecture: Controllers → Services → Models
- Dependency Injection throughout

## 🧱 Tech Stack
- .NET 10
- ASP.NET Core Web API
- C#
- xUnit (unit + integration tests)
- Dependency Injection

## 📁 Project Structure
```
OrderApi/            → Main API (Controllers, Services, Models)
OrderApi.Tests/       → Unit and integration tests, Stryker config
.github/workflows/    → CI: build, tests and mutation testing
OrderSolution.slnx
```

## ▶️ Getting Started
Clone the repo and run from the project root:
```bash
dotnet run --project OrderApi
```
The API starts at `http://localhost:5285`.

### Endpoints
| Method | Route     | Description         |
|--------|-----------|----------------------|
| GET    | `/orders` | List all orders      |
| POST   | `/orders` | Create a new order    |

Example `POST /orders` body:
```json
{
  "items": [
    { "nombreProducto": "A", "cantidad": 2, "precio": 100 }
  ]
}
```

## 🧪 Testing
```bash
dotnet test
```
32 automated tests, run on every push by GitHub Actions:

| Type | What it checks |
|------|----------------|
| Unit tests (`CarritoServiceTests`) | Total = quantity × price, boundary values (0 and negatives are rejected), consecutive ids, initial status, failed orders are not stored |
| Integration tests (`CarritoControllerTests`) | Real HTTP calls against the API in memory: 200 / 400 status codes, response body, error message, `GET /orders` |

### Mutation testing
Passing tests only prove the code works today. Mutation testing checks the tests themselves: [Stryker.NET](https://stryker-mutator.io/docs/stryker-net/introduction/) introduces small bugs (mutants) in the code, such as turning `<=` into `<`, and re-runs the suite. If a test fails, the mutant is "killed".

```bash
dotnet tool install --global dotnet-stryker
cd OrderApi.Tests
dotnet stryker
```
Current score: see the *Mutation testing* job in the latest [CI run](https://github.com/Rodri1923/order-api/actions/workflows/ci.yml) (HTML report attached as an artifact).

> Note: data is currently stored in memory and resets on restart. Persistence with a real database is a planned next step.

## 👤 Author
**Rodrigo Navone**
[GitHub](https://github.com/Rodri1923) · [LinkedIn](https://www.linkedin.com/in/rodrigonavone)
