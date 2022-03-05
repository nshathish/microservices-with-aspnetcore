namespace ShoppingCart.Api.Infrastructure.Services
{
    using System;
    using System.Threading.Tasks;
    using Data.Dtos;

    public interface IProductCatalogService
    {
        Task<ProductCatalogItemDto> GetProductCatalogItemAsync(Guid id);
    }
}