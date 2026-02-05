using ProductCatalog.Api.Data.Entities;
using ProductCatalog.Api.Endpoints.Products;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;
using NShathish.Mongo.Driver.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddMongo()
    .AddMongoRepository<Product>("productCatalog");
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapOpenApi();
app.MapScalarApiReference();

app.UseRouting();

// Map endpoints
app.MapProductsEndpoints();

app.Run();
