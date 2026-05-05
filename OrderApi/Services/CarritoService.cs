using OrderApi.Models;

namespace OrderApi.Services;

// Lógica para manejar el carrito de compras
public class CarritoService
{
    // Simulamos una base de datos en memoria
    private List<Carrito> _carrito = new();

    // Obtener todos los carritos
    public List<Carrito> GetAll()
    {
        return _carrito;
    }

    // Crear un nuevo carrito
    public Carrito Create(Carrito carro)
    {
        //Validar que el carrito tenga al menos un producto
        if (carro.Items == null || !carro.Items.Any())
        {            throw new ArgumentException("El carrito debe contener al menos un producto.");
        }   

        //Validar que cada producto tenga cantidad y precio válidos
        foreach (var item in carro.Items)
        {
            if (item.Cantidad <= 0)
            {
                throw new ArgumentException("La cantidad de cada producto debe ser mayor a cero.");
            }
            if (item.Precio <= 0)
            {
                throw new ArgumentException("El precio de cada producto debe ser mayor a cero.");
            }
        }

        //Logica del carro
        carro.Id = _carrito.Count + 1;
        carro.Total = CalculateTotal(carro);

        _carrito.Add(carro);

        return carro;
    }

    // Método para calcular el total del carrito
    private decimal CalculateTotal(Carrito carro)
    {
        return carro.Items.Sum(item => item.Cantidad * item.Precio);
    }
}