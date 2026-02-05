using System;
using ShoppingCart.Api.Data.Entities;
using ShoppingCart.Api.Endpoints.ShoppingCart;
using ShoppingCart.Api.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;
using NShathish.Mongo.Driver.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddMongo()
    .AddMongoRepository<Cart>("baskets");
builder.Services.AddHttpClient<IProductCatalogService, ProductCatalogService>(client =>
{
    client.BaseAddress = new Uri("http://productcatalog.api:80");
});
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
app.MapShoppingCartEndpoints();

app.Run();
