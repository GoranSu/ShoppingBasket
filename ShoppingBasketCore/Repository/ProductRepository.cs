using ShoppingBasketCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasketCore.Repository
{
    public class ProductRepository
    {
        public static List<Product> GetProducts()
        {
            var products = new List<Product>()
            {
                new Product() {Id = 1, Name = "Butter", Price = 0.80m },
                new Product() {Id = 2, Name = "Milk", Price = 1.15m },
                new Product() {Id = 3, Name = "Bread", Price = 1.00m }
            };

            return products;
        }
    }
}
