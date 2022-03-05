namespace ShoppingCart.Api.Data.Dtos
{
    using System;using System.Collections.Generic;

    public record ProductCatalogItemDto(Guid Id, string Name, string Description, Price Price, Attributes Attributes);

    public class Price
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }

    public class Attributes
    {
        public IEnumerable<char> Sizes { get; set; }
        public IEnumerable<string> Colors { get; set; }
    }
}