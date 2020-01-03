using ShoppingBasketCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingBasketCore.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public List<BasketItem> Items { get; } = new List<BasketItem>();
        public string AppliedDiscounts { get; set; }
        public void AddItem(Product product)
        {
            if (!Items.Any(i => i.Product.Id == product.Id))
            {
                Items.Add(new BasketItem()
                {
                    Product = product,
                    Quantity = 0,
                    Total = 0
                });

                return;
            }
            else
            {
                Console.WriteLine($"Product {product.Name} already added, please increase quantity if you wish to purchase multiple items.");
            }
        }
        public void RemoveItem(BasketItem basketItem) 
        {
            bool exists = Items.Any(i => i.Product.Id == basketItem.Product.Id);
            if (exists)
            {
                Items.Remove(basketItem);
            }
        }
        public void ChangeItemQuantity(Product product, int quantity) 
        {
            var existingItem = Items.FirstOrDefault(i => i.Product.Id == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity = quantity;
                existingItem.Total = existingItem.Product.Price * existingItem.Quantity;
            }
            else
            {
                Console.WriteLine($"Product {product.Name} doesn't exist, please add product to basket.");
            }
        }
        public void ApplyDiscount() 
        {
            // Get all discounts
            List<Discount> discounts = DiscountRepository.GetDiscounts();
            foreach (Discount discount in discounts)
            {
                // Get source product
                BasketItem sourceProduct = this.Items.FirstOrDefault(i => i.Product.Id == discount.SourceProductId);
                if (sourceProduct != null)
                {
                    // Check if source item meets the discount criteria
                    int numberOfItemsToDiscount = sourceProduct.Quantity / discount.SourceProductQuantity;

                    if (numberOfItemsToDiscount != 0)
                    {
                        // Get target item
                        BasketItem targetProduct = this.Items.FirstOrDefault(i => i.Product.Id == discount.TargetProductId);
                        if (targetProduct != null)
                        {
                            // Logging helper for AppliedDiscounts property
                            Log(discount.Name, numberOfItemsToDiscount, targetProduct.Product.Name);

                            decimal originalPrice = targetProduct.Product.Price;
                            decimal discountAmount = (originalPrice * discount.DiscountFactor) * numberOfItemsToDiscount;

                            targetProduct.Total -= discountAmount;
                        }
                    }
                }
            }
        }
        public decimal Total()
        {
            decimal total = 0m;
            foreach (BasketItem item in this.Items)
            {
                total += item.Total;
            }
            return Decimal.Round(total, 2);
        }
        public void Log(string discountName, int numberOfItemsToDiscount, string targetProduct)
        {
            if (string.IsNullOrEmpty(this.AppliedDiscounts))
            {
                this.AppliedDiscounts = $"Applied discount: {discountName} x {numberOfItemsToDiscount} to basket item {targetProduct}";
            }
            else
            {
                this.AppliedDiscounts += $"\nApplied discount: {discountName} x {numberOfItemsToDiscount} to basket item {targetProduct}";
            }
        }
    }
}
