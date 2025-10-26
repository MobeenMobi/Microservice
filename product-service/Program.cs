using Microsoft.EntityFrameworkCore;
using product_service.Data;
using product_service.Models;
using product_service.Data;
using product_service.Models;

var builder = WebApplication.CreateBuilder(args);

// ✅ SQL Server connection (local or from docker-compose)
var connectionString = builder.Configuration.GetConnectionString("SqlServer");

// ✅ Register EF Core DbContext
builder.Services.AddDbContext<ProductDbContex>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Enable Swagger UI for testing
app.UseSwagger();
app.UseSwaggerUI();

// ✅ Auto-migrate database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProductDbContex>();
    db.Database.Migrate();
}

// ✅ REST CRUD Endpoints
app.MapGet("/api/products", async (ProductDbContex db) =>
    await db.Products.ToListAsync());

app.MapGet("/api/products/{id:int}", async (int id, ProductDbContex db) =>
    await db.Products.FindAsync(id) is Product p ? Results.Ok(p) : Results.NotFound());

app.MapPost("/api/products", async (Product product, ProductDbContex db) =>
{
    db.Products.Add(product);
    await db.SaveChangesAsync();
    return Results.Created($"/api/products/{product.Id}", product);
});

app.MapPut("/api/products/{id:int}", async (int id, Product input, ProductDbContex db) =>
{
    var product = await db.Products.FindAsync(id);
    if (product is null) return Results.NotFound();

    product.Name = input.Name;
    product.Description = input.Description;
    product.Price = input.Price;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/api/products/{id:int}", async (int id, ProductDbContex db) =>
{
    var product = await db.Products.FindAsync(id);
    if (product is null) return Results.NotFound();

    db.Products.Remove(product);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
