namespace ShoppingCart.Api.Infrastructure.Services
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Data.Dtos;

    public class ProductCatalogService : IProductCatalogService
    {
        private readonly HttpClient _httpClient;

        public ProductCatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductCatalogItemDto> GetProductCatalogItemAsync(Guid id)
        {
            var item = await _httpClient.GetFromJsonAsync<ProductCatalogItemDto>($"/api/products/{id}");
            return item;
        }
    }
}