namespace ProductCatalog.Api.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using NShathish.Mongo.Driver.Entities;

    public class Product : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Price Price { get; set; }
        public Attributes Attributes { get; set; }
    }

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