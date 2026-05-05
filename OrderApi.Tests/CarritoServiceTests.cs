using Xunit;
using OrderApi.Services;
using OrderApi.Models;

public class CarritoServiceTests
{
    [Fact]
    public void Create_DeberiaCalcularTotalCorrectamente()
    {
        var service = new CarritoService();

        var carrito = new Carrito
        {
            Items = new List<Producto>
            {
                new Producto { NombreProducto = "A", Cantidad = 2, Precio = 100 },
                new Producto { NombreProducto = "B", Cantidad = 1, Precio = 50 }
            }
        };

        var result = service.Create(carrito);

        Assert.Equal(250, result.Total);
    }

    [Fact]
    public void Create_DeberiaFallarSiCarritoVacio()
    {
        var service = new CarritoService();

        var carrito = new Carrito
        {
            Items = new List<Producto>()
        };

        Assert.Throws<ArgumentException>(() => service.Create(carrito));
    }

    [Fact]
    public void Create_DeberiaFallarSiCantidadEsInvalida()
    {
        var service = new CarritoService();

        var carrito = new Carrito
        {
            Items = new List<Producto>
            {
                new Producto { NombreProducto = "A", Cantidad = 0, Precio = 100 }
            }
        };

        Assert.Throws<ArgumentException>(() => service.Create(carrito));
    }

    [Fact]
    public void Create_DeberiaFallarSiPrecioEsInvalido()
    {
        var service = new CarritoService();

        var carrito = new Carrito
        {
            Items = new List<Producto>
            {
                new Producto { NombreProducto = "A", Cantidad = 1, Precio = 0 }
            }
        };

        Assert.Throws<ArgumentException>(() => service.Create(carrito));
    }
}