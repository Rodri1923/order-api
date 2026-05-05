using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class CarritoControllerTests
{
    [Fact]
    public async Task Post_CarritoValido_ReturnsOk()
    {
        var app = new WebApplicationFactory<Program>();
        var client = app.CreateClient();

        var carrito = new
        {
            items = new[]
            {
                new { nombreProducto = "A", cantidad = 2, precio = 100 }
            }
        };

        var response = await client.PostAsJsonAsync("/orders", carrito);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Post_CarritoVacio_ReturnsBadRequest()
    {
        var app = new WebApplicationFactory<Program>();
        var client = app.CreateClient();

        var carrito = new { };

        var response = await client.PostAsJsonAsync("/orders", carrito);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}