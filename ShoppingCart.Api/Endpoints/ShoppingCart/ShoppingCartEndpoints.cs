using System;
using System.Collections.Generic;
using ShoppingCart.Api.Data.Dtos;
using ShoppingCart.Api.Data.Entities;
using ShoppingCart.Api.Infrastructure.Extensions;
using ShoppingCart.Api.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NShathish.Mongo.Driver.Repository;

namespace ShoppingCart.Api.Endpoints.ShoppingCart;

public static class ShoppingCartEndpoints
{
    public static void MapShoppingCartEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/shoppingcart")
            .WithTags("ShoppingCart");

        group.MapGet("/{id:guid}", async (Guid id, IRepository<Cart> cartRepository, IProductCatalogService productCatalogService) =>
        {
            var cart = await cartRepository.GetAsync(id);
            if (cart == null)
                return Results.NotFound(id);

            var productCatalogItems = new List<ProductCatalogItemDto>();
            foreach (var cartProduct in cart.Products)
            {
                var productCatalogItem = await productCatalogService.GetProductCatalogItemAsync(cartProduct.Id);
                if (productCatalogItem != null)
                    productCatalogItems.Add(productCatalogItem);
            }

            return Results.Ok(cart.AsDto(productCatalogItems));
        })
        .WithName("GetShoppingCart")
        .Produces<CartDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/", async (CreateCartDto model, IRepository<Cart> cartRepository, IProductCatalogService productCatalogService) =>
        {
            foreach (var modelProduct in model.Products)
            {
                if (await productCatalogService.GetProductCatalogItemAsync(modelProduct.Id) == null)
                    return Results.NotFound(modelProduct.Id);
            }

            var cart = new Cart
            {
                UserId = model.UserId,
                Products = model.Products
            };

            await cartRepository.CreateAsync(cart);

            return Results.CreatedAtRoute("GetShoppingCart", new { id = cart.Id }, cart);
        })
        .WithName("CreateShoppingCart")
        .Produces<Cart>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);
    }
}
