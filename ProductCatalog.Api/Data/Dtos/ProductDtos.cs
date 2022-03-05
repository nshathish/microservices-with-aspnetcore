namespace ProductCatalog.Api.Data.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Entities;

    public record ProductItemDto(Guid Id, string Name, string Description, Price Price, Attributes Attributes);

    public record CreateProductDto([Required] string Name, string Description, [Required] Price Price,
        [Required] Attributes Attributes);
}