# Order API

REST API desarrollada con ASP.NET Core para la gestión de pedidos (carritos), incluyendo validación de datos, cálculo de totales y testing automatizado.

---

## 🚀 Features

* Crear pedidos (carritos)
* Validación de datos de entrada
* Cálculo automático del total
* Manejo de errores HTTP (400 / 200)
* Arquitectura por capas:

  * Controllers
  * Services
  * Models
* Tests automatizados:

  * Unit tests (lógica)
  * Integration tests (HTTP)

---

## 🛠️ Tecnologías

* .NET 10
* ASP.NET Core Web API
* xUnit
* Dependency Injection

---

## 📁 Estructura del proyecto

API Pedidos/

* OrderApi/ → API principal
* OrderApi.Tests/ → Tests
* OrderSolution.slnx

---

## ▶️ Ejecutar la API

Desde la raíz del proyecto:

dotnet run --project OrderApi

La API se levanta en:

http://localhost:5285

---

## 🧪 Ejecutar tests

dotnet test

---

## 📌 Endpoints principales

### GET /orders

Obtiene todos los pedidos.

---

### POST /orders

Crea un nuevo pedido.

Ejemplo de body:

{
"items": [
{
"nombreProducto": "A",
"cantidad": 2,
"precio": 100
}
]
}

---

## ❌ Validaciones

* El carrito debe tener al menos un producto
* Cantidad > 0
* Precio > 0

---

## 🧠 Notas

Actualmente la información se almacena en memoria (RAM).
Los datos se pierden al reiniciar la aplicación.

---

## 📈 Próximos pasos

* Persistencia con base de datos
* Mejora de arquitectura
* Más casos de testing
* Autenticación
