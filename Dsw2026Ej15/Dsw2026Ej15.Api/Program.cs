using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Registramos nuestra persistencia como un "Singleton".
builder.Services.AddSingleton<IPersistence, PersistenceInMemory>();

// También agregamos el Health Check que pide la consigna.
builder.Services.AddHealthChecks();

// Add services to the container.
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dsw2026Ej15 API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dsw2026Ej15 API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapHealthChecks("/health-check"); // ruta para el sondeo de estado.
app.MapControllers();

app.Run();
