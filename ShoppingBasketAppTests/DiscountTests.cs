using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingBasketCore.Models;
using ShoppingBasketCore.Repository;

namespace ShoppingBasketAppTests
{
    [TestClass]
    public class DiscountTests
    {
        #region Fields
        private int _testBasketId = 1;
        private Product _testProductButter = new Product() { Id = 1, Name = "Butter", Price = 0.80m };
        private Product _testProductMilk = new Product() { Id = 2, Name = "Milk", Price = 1.15m };
        private Product _testProductBread = new Product() { Id = 3, Name = "Bread", Price = 1.00m };
        #endregion

        [TestMethod]
        public void CalculatesNotEligibleForDiscount()
        {
            var basket = new Basket();
            basket.Id = _testBasketId;
            basket.AddItem(_testProductButter);
            basket.ChangeItemQuantity(_testProductButter, 1);
            basket.AddItem(_testProductBread);
            basket.ChangeItemQuantity(_testProductBread, 1);
            basket.AddItem(_testProductMilk);
            basket.ChangeItemQuantity(_testProductMilk, 1);
            basket.ApplyDiscount();

            Assert.AreEqual(2.95m, basket.Total());
        }

        [TestMethod]
        public void AppliesBreadDiscount()
        {
            var basket = new Basket();
            basket.Id = _testBasketId;
            basket.AddItem(_testProductButter);
            basket.ChangeItemQuantity(_testProductButter, 2);
            basket.AddItem(_testProductBread);
            basket.ChangeItemQuantity(_testProductBread, 1);
            basket.ApplyDiscount();

            var basketItem = basket.Items.FirstOrDefault(i => i.Product.Name == "Bread");

            Assert.AreEqual(0.50m, basketItem.Total);
        }

        [TestMethod]
        public void AppliesMilkDiscountFor4()
        {
            var basket = new Basket();
            basket.Id = _testBasketId;
            basket.AddItem(_testProductMilk);
            basket.ChangeItemQuantity(_testProductMilk, 4);
            basket.ApplyDiscount();

            Assert.AreEqual(3.45m, basket.Total());
        }

        [TestMethod]
        public void AppliesMilkDiscountFor8()
        {
            var basket = new Basket();
            basket.Id = _testBasketId;
            basket.AddItem(_testProductMilk);
            basket.ChangeItemQuantity(_testProductMilk, 8);
            basket.ApplyDiscount();

            Assert.AreEqual(6.90m, basket.Total());
        }

        [TestMethod]
        public void AppliesMultipleDiscounts()
        {
            var basket = new Basket();
            basket.Id = _testBasketId;
            basket.AddItem(_testProductButter);
            basket.ChangeItemQuantity(_testProductButter, 2);
            basket.AddItem(_testProductMilk);
            basket.ChangeItemQuantity(_testProductMilk, 8);
            basket.AddItem(_testProductBread);
            basket.ChangeItemQuantity(_testProductBread, 1);
            basket.ApplyDiscount();

            var basketItemButter = basket.Items.FirstOrDefault(i => i.Product.Name == "Butter");
            var basketItemMilk = basket.Items.FirstOrDefault(i => i.Product.Name == "Milk");
            var basketItemBread = basket.Items.FirstOrDefault(i => i.Product.Name == "Bread");

            Assert.AreEqual(1.60m, basketItemButter.Total);
            Assert.AreEqual(6.90m, basketItemMilk.Total);
            Assert.AreEqual(0.50m, basketItemBread.Total);
            Assert.AreEqual(9.00m, basket.Total());
        }
        [TestMethod]
        public void CalculatesPricesWithoutDiscounts()
        {
            var basket = new Basket();
            basket.Id = _testBasketId;
            basket.AddItem(_testProductButter);
            basket.ChangeItemQuantity(_testProductButter, 2);
            basket.AddItem(_testProductMilk);
            basket.ChangeItemQuantity(_testProductMilk, 8);
            basket.AddItem(_testProductBread);
            basket.ChangeItemQuantity(_testProductBread, 1);

            var basketItemButter = basket.Items.FirstOrDefault(i => i.Product.Name == "Butter");
            var basketItemMilk = basket.Items.FirstOrDefault(i => i.Product.Name == "Milk");
            var basketItemBread = basket.Items.FirstOrDefault(i => i.Product.Name == "Bread");

            Assert.AreEqual(1.60m, basketItemButter.Total);
            Assert.AreEqual(9.20m, basketItemMilk.Total);
            Assert.AreEqual(1m, basketItemBread.Total);
            Assert.AreEqual(11.80m, basket.Total());
        }
    }
}
