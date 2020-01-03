using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingBasketCore.Models;
using ShoppingBasketCore.Repository;

namespace ShoppingBasketAppTests
{
    [TestClass]
    public class BasketTests
    {
        #region Fields
        private int _testBasketId = 1;
        private Product _testProductButter = new Product() { Id = 1, Name = "Butter", Price = 0.80m };
        private Product _testProductMilk = new Product() { Id = 2, Name = "Milk", Price = 1.15m };
        private Product _testProductBread = new Product() { Id = 3, Name = "Bread", Price = 1.00m };
        #endregion

        [TestMethod]
        public void AddsSingleProduct()
        {
            var basket = new Basket();
            basket.Id = _testBasketId;
            basket.AddItem(_testProductButter);

            var item = basket.Items.First();

            Assert.AreEqual(_testProductButter.Id, item.Product.Id);
            Assert.AreEqual(_testProductButter.Name, item.Product.Name);
            Assert.AreEqual(_testProductButter.Price, item.Product.Price);
            Assert.AreEqual(0, item.Quantity);
            Assert.AreEqual(0, item.Total);
            Assert.AreEqual(0, basket.Total());
        }

        [TestMethod]
        public void AddsAllProducts()
        {
            var basket = new Basket();
            basket.Id = _testBasketId;
            basket.AddItem(_testProductButter);
            basket.AddItem(_testProductMilk);
            basket.AddItem(_testProductBread);

            Assert.AreEqual(3, basket.Items.Count());
        }

        [TestMethod]
        public void ChangesItemQuantityAndTotal()
        {
            var basket = new Basket();
            basket.Id = _testBasketId;
            basket.AddItem(_testProductButter);
            basket.ChangeItemQuantity(_testProductButter, 2);

            var item = basket.Items.First();

            Assert.AreEqual(2, item.Quantity);
            Assert.AreEqual(1.60m, item.Total);
            Assert.AreEqual(1.60m, basket.Total());
        }

        [TestMethod]
        public void LogsAppliedDiscounts()
        {
            var basket = new Basket();
            basket.Id = _testBasketId;
            basket.AddItem(_testProductButter);
            basket.ChangeItemQuantity(_testProductButter, 2);
            basket.AddItem(_testProductBread);
            basket.ChangeItemQuantity(_testProductBread, 1);
            basket.ApplyDiscount();

            Assert.AreEqual(true, !String.IsNullOrEmpty(basket.AppliedDiscounts));
        }

        [TestMethod]
        public void RemovesItem()
        {
            Basket basket = new Basket();
            basket.Id = _testBasketId;
            basket.AddItem(_testProductButter);
            basket.ChangeItemQuantity(_testProductButter, 1);

            var itemForRemoval = basket.Items.First();
            basket.RemoveItem(itemForRemoval);

            Assert.AreEqual(0, basket.Items.Count());
        }
    }
}
