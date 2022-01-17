using RestFull.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestFull.Data
{
    public static class DbInitializer
    {
        public static void Initializer(StoreContext context)
        {
            if (context.Products.Any()) return;

            var products = new List<Product>
            {
                new Product
                {
                    Name ="Angular Blue Boots",
                    Description = "Cool to do",
                    Price = 1800,
                    PictureUrl ="/image/product/boot.png",
                    Brand = "Angular",
                    Type ="Boots",
                    QuantityInStock =100
                },
                new Product
                {
                    Name ="Core Purple Boots",
                    Description = "Good to go",
                    Price = 1250,
                    PictureUrl ="/image/product/boot.png",
                    Brand = "NetCore",
                    Type ="Boots",
                    QuantityInStock =100
                },
                new Product
                {
                    Name ="React Gloves",
                    Description = "Become cool",
                    Price = 1890,
                    PictureUrl ="/image/product/boot.png",
                    Brand = "React",
                    Type ="Glover",
                    QuantityInStock =100
                },
            };
            foreach (var product in products)
            {
                context.Products.Add(product);
            }
            context.SaveChanges();
        }


    }
}
