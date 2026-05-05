using Microsoft.AspNetCore.Mvc;
using OrderApi.Services;
using OrderApi.Models;

namespace OrderApi.Controllers;

[ApiController]
[Route("orders")]
public class CarritoController : ControllerBase
{
    private readonly CarritoService _service;

    public CarritoController(CarritoService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpPost]
    public IActionResult Create(Carrito carrito)
    {
        try
        {
            var result = _service.Create(carrito);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}