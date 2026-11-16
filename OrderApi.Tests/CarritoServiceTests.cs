using Xunit;
using OrderApi.Services;
using OrderApi.Models;

// Tests unitarios: prueban la lógica de CarritoService sin levantar la API.
public class CarritoServiceTests
{
    private static Carrito CarritoCon(params Producto[] items) =>
        new Carrito { Items = items.ToList() };

    private static Producto Item(int cantidad, decimal precio) =>
        new Producto { NombreProducto = "A", Cantidad = cantidad, Precio = precio };

    // ---------- Total ----------

    [Fact]
    public void Create_DeberiaCalcularTotalCorrectamente()
    {
        var service = new CarritoService();

        var result = service.Create(CarritoCon(Item(2, 100), Item(1, 50)));

        Assert.Equal(250m, result.Total);
    }

    [Theory]
    [InlineData(1, 1, 1)]          // mínimo válido
    [InlineData(3, 10, 30)]        // cantidad por precio (no suma)
    [InlineData(2, 0.5, 1)]        // precio con decimales
    [InlineData(1000, 99.99, 99990)]
    public void Create_TotalEsCantidadPorPrecio(int cantidad, double precio, double esperado)
    {
        var service = new CarritoService();

        var result = service.Create(CarritoCon(Item(cantidad, (decimal)precio)));

        Assert.Equal((decimal)esperado, result.Total);
    }

    [Fact]
    public void Create_IgnoraElTotalQueMandaElCliente()
    {
        var service = new CarritoService();
        var carrito = CarritoCon(Item(2, 100));
        carrito.Total = 999999;

        var result = service.Create(carrito);

        Assert.Equal(200m, result.Total);
    }

    // ---------- Validaciones ----------

    [Fact]
    public void Create_DeberiaFallarSiCarritoVacio()
    {
        var service = new CarritoService();

        var ex = Assert.Throws<ArgumentException>(() => service.Create(CarritoCon()));

        Assert.Contains("al menos un producto", ex.Message);
    }

    [Fact]
    public void Create_DeberiaFallarSiItemsEsNull()
    {
        var service = new CarritoService();
        var carrito = new Carrito { Items = null! };

        Assert.Throws<ArgumentException>(() => service.Create(carrito));
    }

    [Theory]
    [InlineData(0)]    // borde: cero no es válido
    [InlineData(-1)]
    [InlineData(-50)]
    public void Create_DeberiaFallarSiCantidadEsInvalida(int cantidad)
    {
        var service = new CarritoService();

        var ex = Assert.Throws<ArgumentException>(() => service.Create(CarritoCon(Item(cantidad, 100))));

        Assert.Contains("cantidad", ex.Message);
    }

    [Theory]
    [InlineData(0)]    // borde: cero no es válido
    [InlineData(-0.01)]
    [InlineData(-100)]
    public void Create_DeberiaFallarSiPrecioEsInvalido(double precio)
    {
        var service = new CarritoService();

        var ex = Assert.Throws<ArgumentException>(() => service.Create(CarritoCon(Item(1, (decimal)precio))));

        Assert.Contains("precio", ex.Message);
    }

    [Fact]
    public void Create_DeberiaFallarSiUnSoloItemEsInvalido()
    {
        var service = new CarritoService();

        // El primero es válido; el segundo no. Tiene que revisar todos.
        Assert.Throws<ArgumentException>(() => service.Create(CarritoCon(Item(1, 100), Item(0, 100))));
    }

    [Fact]
    public void Create_SiFalla_NoGuardaElCarrito()
    {
        var service = new CarritoService();

        Assert.Throws<ArgumentException>(() => service.Create(CarritoCon(Item(0, 100))));

        Assert.Empty(service.GetAll());
    }

    // ---------- Id, estado y listado ----------

    [Fact]
    public void Create_AsignaIdsConsecutivosDesdeUno()
    {
        var service = new CarritoService();

        var primero = service.Create(CarritoCon(Item(1, 10)));
        var segundo = service.Create(CarritoCon(Item(1, 10)));

        Assert.Equal(1, primero.Id);
        Assert.Equal(2, segundo.Id);
    }

    [Fact]
    public void Create_ElEstadoInicialEsPendiente()
    {
        var service = new CarritoService();

        var result = service.Create(CarritoCon(Item(1, 10)));

        Assert.Equal("Pendiente", result.Estado);
    }

    [Fact]
    public void GetAll_SinCarritos_DevuelveListaVacia()
    {
        var service = new CarritoService();

        Assert.Empty(service.GetAll());
    }

    [Fact]
    public void GetAll_DevuelveLosCarritosCreados()
    {
        var service = new CarritoService();
        service.Create(CarritoCon(Item(1, 10)));
        service.Create(CarritoCon(Item(2, 20)));

        var todos = service.GetAll();

        Assert.Equal(2, todos.Count);
        Assert.Equal(10m, todos[0].Total);
        Assert.Equal(40m, todos[1].Total);
    }
}
