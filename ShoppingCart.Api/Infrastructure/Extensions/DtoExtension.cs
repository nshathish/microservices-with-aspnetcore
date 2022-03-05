namespace ShoppingCart.Api.Infrastructure.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Data.Dtos;
    using Data.Entities;

    public static class DtoExtension
    {
        public static CartDto AsDto(this Cart cart, IEnumerable<ProductCatalogItemDto> productCatalogItems)
        {
            var cartItems = cart.Products.Select(p =>
            {
                var productCatalog = productCatalogItems.Single(pc => pc.Id == p.Id);
                return productCatalog.AsDto(p.Quantity);
            }).ToList();

            return new(cart.Id, cartItems, cartItems.Sum(c => c.SubTotal));
        }

        public static CartItemDto AsDto(this ProductCatalogItemDto product, int quantity) => new(product.Name,
            product.Description, product.Price.Amount, quantity, product.Price.Amount * quantity);
    }
}