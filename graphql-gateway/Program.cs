using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

using graphql_gateway.GraphQL;

var builder = WebApplication.CreateBuilder(args);

// Register HTTP Clients (make sure URLs match your services)
builder.Services.AddHttpClient("Orders", client =>
{
    client.BaseAddress = new Uri("http://localhost:5010/"); // Order microservice
});

builder.Services.AddHttpClient("Products", client =>
{
    client.BaseAddress = new Uri("http://localhost:5038/"); // Product microservice
});

// GraphQL Setup
builder.Services
   .AddGraphQLServer()
.AddQueryType<Query>();


var app = builder.Build();

// Enable GraphQL Endpoint
app.MapGraphQL("/graphql");

app.Run();
