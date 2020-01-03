using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasketCore.Models
{
    public class BasketItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
