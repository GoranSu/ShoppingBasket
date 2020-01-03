using ShoppingBasketCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasketCore.Repository
{
    public class DiscountRepository
    {
        public static List<Discount> GetDiscounts()
        {
            var discounts = new List<Discount>()
            {
                new Discount {  Id = 1,
                                Name = "Bread Discount 50%",
                                SourceProductId = 1, // Butter
                                SourceProductQuantity = 2, // 2 butters trigger a discount
                                DiscountFactor = 0.5m, // Discount 50%
                                TargetProductId = 3 // Bread
                             },
                new Discount {  Id = 2,
                                Name = "Fourth Milk free",
                                SourceProductId = 2, // Milk
                                SourceProductQuantity = 4, // 4 milks trigger discount, get 1 free 
                                DiscountFactor = 1m, // 100%
                                TargetProductId = 2 // Milk
                             }
            };

            return discounts;
        }
    }
}
