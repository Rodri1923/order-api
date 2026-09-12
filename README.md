# 🧾 Order API

🌍 **[English](README.md) · [Español](README.es.md)**

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
OrderApi.Tests/       → Unit and integration tests
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
Includes unit tests (business logic: totals, validations) and integration tests (end-to-end HTTP), plus mutation testing to confirm the test suite actually fails when the logic is broken.

> Note: data is currently stored in memory and resets on restart. Persistence with a real database is a planned next step.

## 👤 Author
**Rodrigo Navone**
[GitHub](https://github.com/Rodri1923) · [LinkedIn](https://www.linkedin.com/in/rodrigonavone)
