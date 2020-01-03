using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasketCore.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SourceProductId { get; set; }
        public int SourceProductQuantity { get; set; }
        public int TargetProductId { get; set; }
        public decimal DiscountFactor { get; set; }
    }
}
