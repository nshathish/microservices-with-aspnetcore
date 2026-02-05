using System;
using System.Collections.Generic;
using ProductCatalog.Api.Data.Dtos;
using ProductCatalog.Api.Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NShathish.Mongo.Driver.Repository;

namespace ProductCatalog.Api.Endpoints.Products;

public static class ProductsEndpoints
{
    public static void MapProductsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/products")
            .WithTags("Products");

        group.MapGet("/", async (IRepository<Product> productRepository) =>
        {
            var products = await productRepository.GetAllAsync();
            return Results.Ok(products);
        })
        .WithName("GetProducts")
        .Produces<IEnumerable<Product>>(StatusCodes.Status200OK);

        group.MapGet("/{id:guid}", async (Guid id, IRepository<Product> productRepository) =>
        {
            var product = await productRepository.GetAsync(id);
            if (product == null)
                return Results.NotFound(id);
            return Results.Ok(product);
        })
        .WithName("GetProduct")
        .Produces<Product>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/", async (CreateProductDto model, IRepository<Product> productRepository) =>
        {
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Attributes = model.Attributes
            };

            await productRepository.CreateAsync(product);

            return Results.CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        })
        .WithName("CreateProduct")
        .Produces<Product>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
