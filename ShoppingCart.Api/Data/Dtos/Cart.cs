namespace ShoppingCart.Api.Data.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Entities;

    public record CreateCartDto([Required] int UserId,
        [Required, MinLength(1, ErrorMessage = "At least one product required")] IEnumerable<Product> Products);

    public record CartDto(Guid Id, IEnumerable<CartItemDto> Items, decimal Total);

    public record CartItemDto(string Name, string Description, decimal UnitPrice, int Quantity, decimal SubTotal);
}