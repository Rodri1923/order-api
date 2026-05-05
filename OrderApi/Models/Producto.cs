namespace OrderApi.Models;

public class Producto
{
    public string NombreProducto { get; set; } = "";

    public int Cantidad { get; set; }

    public decimal Precio { get; set; }
}