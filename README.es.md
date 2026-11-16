# 🧾 Order API

🌍 **[English](README.md) · [Español](README.es.md)**

[![CI](https://github.com/Rodri1923/order-api/actions/workflows/ci.yml/badge.svg)](https://github.com/Rodri1923/order-api/actions/workflows/ci.yml)

> API REST para gestión de pedidos construida con ASP.NET Core, con foco en arquitectura por capas y testing automatizado.

## 📋 Descripción
Proyecto backend para practicar diseño de APIs REST en .NET: arquitectura por capas (Controllers, Services, Models), validación de entradas y una estrategia de testing sólida que incluye unit tests, integration tests y mutation testing para verificar que los propios tests detectan fallas reales.

## 🚀 Funcionalidades
- Crear y listar pedidos (carritos) con líneas de producto
- Validación de entradas: el carrito debe tener al menos un producto, cantidad > 0, precio > 0
- Cálculo automático del total
- Manejo correcto de códigos HTTP (200 OK / 400 Bad Request)
- Arquitectura por capas: Controllers → Services → Models
- Dependency Injection en todo el proyecto

## 🧱 Stack técnico
- .NET 10
- ASP.NET Core Web API
- C#
- xUnit (unit e integration tests)
- Dependency Injection

## 📁 Estructura del proyecto
```
OrderApi/             → API principal (Controllers, Services, Models)
OrderApi.Tests/        → Tests unitarios y de integración
OrderSolution.slnx
```

## ▶️ Cómo ejecutar
Cloná el repo y ejecutá desde la raíz del proyecto:
```bash
dotnet run --project OrderApi
```
La API se levanta en `http://localhost:5285`.

### Endpoints
| Método | Ruta      | Descripción              |
|--------|-----------|----------------------------|
| GET    | `/orders` | Lista todos los pedidos    |
| POST   | `/orders` | Crea un nuevo pedido       |

Ejemplo de body para `POST /orders`:
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
32 tests automatizados, que GitHub Actions corre en cada push:

| Tipo | Qué verifica |
|------|--------------|
| Unit tests (`CarritoServiceTests`) | Total = cantidad × precio, valores borde (0 y negativos se rechazan), ids consecutivos, estado inicial, un pedido que falla no se guarda |
| Integration tests (`CarritoControllerTests`) | Llamadas HTTP reales contra la API en memoria: códigos 200 / 400, cuerpo de la respuesta, mensaje de error, `GET /orders` |

### Mutation testing
Que los tests pasen solo prueba que el código funciona hoy. El mutation testing prueba a los tests: [Stryker.NET](https://stryker-mutator.io/docs/stryker-net/introduction/) mete errores chicos (mutantes) en el código, por ejemplo cambiar `<=` por `<`, y vuelve a correr la suite. Si algún test falla, el mutante queda "muerto".

```bash
dotnet tool install --global dotnet-stryker
cd OrderApi.Tests
dotnet stryker
```
Puntaje actual: ver el job *Mutation testing* de la última [corrida de CI](https://github.com/Rodri1923/order-api/actions/workflows/ci.yml) (el reporte HTML queda adjunto como artefacto).

> Nota: los datos se almacenan en memoria y se pierden al reiniciar la aplicación. Persistencia con base de datos es un próximo paso planificado.

## 👤 Autor
**Rodrigo Navone**
[GitHub](https://github.com/Rodri1923) · [LinkedIn](https://www.linkedin.com/in/rodrigonavone)
