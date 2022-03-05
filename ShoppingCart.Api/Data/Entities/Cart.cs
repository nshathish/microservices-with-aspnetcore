namespace ShoppingCart.Api.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using NShathish.Mongo.Driver.Entities;

    public class Cart: IEntity
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }

    public class Product
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}