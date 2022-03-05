namespace ProductCatalog.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Data.Dtos;
    using Data.Entities;
    using Microsoft.AspNetCore.Http;
    using NShathish.Mongo.Driver.Repository;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<Product> _productRepository;

        public ProductsController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync() => Ok(await _productRepository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var product = await _productRepository.GetAsync(id);
            if (product == null) return NotFound(id);
            return Ok(product);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] CreateProductDto model)
        {
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Attributes = model.Attributes
            };

            await _productRepository.CreateAsync(product);

            // ReSharper disable once Mvc.ActionNotResolved
            return CreatedAtAction(nameof(GetAsync), new {id = product.Id}, product);
        }
    }
}