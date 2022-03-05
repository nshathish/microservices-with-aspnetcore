namespace ShoppingCart.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data.Dtos;
    using Data.Entities;
    using Infrastructure.Extensions;
    using Infrastructure.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using NShathish.Mongo.Driver.Repository;

    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IProductCatalogService _productCatalogService;

        public ShoppingCartController(IRepository<Cart> cartRepository, IProductCatalogService productCatalogService)
        {
            _cartRepository = cartRepository;
            _productCatalogService = productCatalogService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var cart = await _cartRepository.GetAsync(id);
            if (cart == null) return NotFound(id);

            var productCatalogItems = new List<ProductCatalogItemDto>();
            foreach (var cartProduct in cart.Products)
            {
                var productCatalogItem = await _productCatalogService.GetProductCatalogItemAsync(cartProduct.Id);
                if (productCatalogItem != null)
                    productCatalogItems.Add(productCatalogItem);
            }

            return Ok(cart.AsDto(productCatalogItems));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] CreateCartDto model)
        {
            foreach (var modelProduct in model.Products)
                if (await _productCatalogService.GetProductCatalogItemAsync(modelProduct.Id) == null)
                    return NotFound(modelProduct.Id);

            var cart = new Cart
            {
                UserId = model.UserId,
                Products = model.Products
            };

            await _cartRepository.CreateAsync(cart);

            // ReSharper disable once Mvc.ActionNotResolved
            return CreatedAtAction(nameof(GetAsync), new {id = cart.Id}, cart);
        }
    }
}