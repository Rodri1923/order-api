# 🧾 Order API

🌍 **[English](README.md) · [Español](README.es.md)**

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
Incluye unit tests (lógica de negocio: totales, validaciones) e integration tests (HTTP end-to-end), además de mutation testing para confirmar que la suite de tests realmente falla cuando se rompe la lógica.

> Nota: los datos se almacenan en memoria y se pierden al reiniciar la aplicación. Persistencia con base de datos es un próximo paso planificado.

## 👤 Autor
**Rodrigo Navone**
[GitHub](https://github.com/Rodri1923) · [LinkedIn](https://www.linkedin.com/in/rodrigonavone)
