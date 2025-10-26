using Microsoft.EntityFrameworkCore;
using order_service.Data;
using order_service.Models;
using order_service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with PostgreSQL
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Service
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/api/orders", async (IOrderService service) =>
{
    return Results.Ok(await service.GetAllAsync());
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/orders/{id:int}", async (int id, IOrderService service) =>
{
    var order = await service.GetByIdAsync(id);
    return order is not null ? Results.Ok(order) : Results.NotFound();
});

app.MapPost("/api/orders", async (Orders order, IOrderService service) =>
{
    var created = await service.CreateAsync(order);
    return Results.Created($"/api/orders/{created.id}", created);
});

app.MapPut("/api/orders/{id:int}", async (int id, Orders order, IOrderService service) =>
{
    var updated = await service.UpdateAsync(id, order);
    return Results.Ok(updated);
});

app.MapDelete("/api/orders/{id:int}", async (int id, IOrderService service) =>
{
    var deleted = await service.DeleteAsync(id);
    return deleted ? Results.NoContent() : Results.NotFound();
});

app.Run();
