using OrderApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Registrar servicios (DI)
builder.Services.AddSingleton<CarritoService>();

// Activar controllers
builder.Services.AddControllers();

var app = builder.Build();

// Mapear controllers
app.MapControllers();

app.Run();