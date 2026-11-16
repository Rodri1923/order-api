using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using OrderApi.Models;
using Xunit;

// Tests de integración: levantan la API en memoria y le pegan por HTTP,
// igual que lo haría un cliente real.
public class CarritoControllerTests
{
    // Cada test crea su propia API, así no comparten datos entre sí.
    private static HttpClient NuevoCliente() =>
        new WebApplicationFactory<Program>().CreateClient();

    private static object CarritoValido(int cantidad = 2, decimal precio = 100) => new
    {
        items = new[]
        {
            new { nombreProducto = "A", cantidad, precio }
        }
    };

    [Fact]
    public async Task Post_CarritoValido_ReturnsOk()
    {
        var client = NuevoCliente();

        var response = await client.PostAsJsonAsync("/orders", CarritoValido());

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Post_CarritoValido_DevuelveIdTotalYEstado()
    {
        var client = NuevoCliente();

        var response = await client.PostAsJsonAsync("/orders", CarritoValido(cantidad: 2, precio: 100));
        var creado = await response.Content.ReadFromJsonAsync<Carrito>();

        Assert.NotNull(creado);
        Assert.Equal(1, creado.Id);
        Assert.Equal(200m, creado.Total);
        Assert.Equal("Pendiente", creado.Estado);
        Assert.Single(creado.Items);
    }

    [Fact]
    public async Task Post_CarritoVacio_ReturnsBadRequest()
    {
        var client = NuevoCliente();

        var response = await client.PostAsJsonAsync("/orders", new { });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData(0, 100)]    // cantidad cero
    [InlineData(-1, 100)]   // cantidad negativa
    [InlineData(1, 0)]      // precio cero
    [InlineData(1, -5)]     // precio negativo
    public async Task Post_ItemInvalido_ReturnsBadRequest(int cantidad, int precio)
    {
        var client = NuevoCliente();

        var response = await client.PostAsJsonAsync("/orders", CarritoValido(cantidad, precio));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Post_CarritoInvalido_DevuelveMensajeDeError()
    {
        var client = NuevoCliente();

        var response = await client.PostAsJsonAsync("/orders", CarritoValido(cantidad: 0));
        var body = await response.Content.ReadAsStringAsync();

        Assert.Contains("error", body);
        Assert.Contains("cantidad", body);
    }

    [Fact]
    public async Task Post_CarritoInvalido_NoQuedaGuardado()
    {
        var client = NuevoCliente();

        await client.PostAsJsonAsync("/orders", CarritoValido(cantidad: 0));
        var todos = await client.GetFromJsonAsync<List<Carrito>>("/orders");

        Assert.NotNull(todos);
        Assert.Empty(todos);
    }

    [Fact]
    public async Task Get_SinPedidos_DevuelveListaVacia()
    {
        var client = NuevoCliente();

        var response = await client.GetAsync("/orders");
        var todos = await response.Content.ReadFromJsonAsync<List<Carrito>>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(todos);
        Assert.Empty(todos);
    }

    [Fact]
    public async Task Get_DespuesDeCrearDos_DevuelveLosDos()
    {
        var client = NuevoCliente();
        await client.PostAsJsonAsync("/orders", CarritoValido(cantidad: 1, precio: 10));
        await client.PostAsJsonAsync("/orders", CarritoValido(cantidad: 3, precio: 10));

        var todos = await client.GetFromJsonAsync<List<Carrito>>("/orders");

        Assert.NotNull(todos);
        Assert.Equal(2, todos.Count);
        Assert.Equal(new[] { 1, 2 }, todos.Select(c => c.Id));
        Assert.Equal(new[] { 10m, 30m }, todos.Select(c => c.Total));
    }
}
