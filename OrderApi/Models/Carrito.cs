namespace OrderApi.Models;

public class Carrito
{
    public int Id { get; set; }

    public string Estado { get; set; } = "Pendiente";

    public decimal Total { get; set; }

    public List<Producto> Items { get; set; } = new();
}