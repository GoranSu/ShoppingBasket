using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingBasketCore.Models;
using ShoppingBasketCore.Repository;

namespace ShoppingBasketApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Product> products = ProductRepository.GetProducts();
            Product butter = products.FirstOrDefault(i => i.Name == "Butter");
            Product milk = products.FirstOrDefault(i => i.Name == "Milk");
            Product bread = products.FirstOrDefault(i => i.Name == "Bread");
            
            #region Scenario 1 --> 1 butter, 1 bread, 1 milk = $2.95
            Basket basket = new Basket() { Id = 1};
            basket.AddItem(butter);
            basket.ChangeItemQuantity(butter, 1);
            basket.AddItem(milk);
            basket.ChangeItemQuantity(milk, 1);
            basket.AddItem(bread);
            basket.ChangeItemQuantity(bread, 1);
            basket.ApplyDiscount();
            Checkout(basket);
            #endregion
            #region Scenario 2 --> 2 butter, 2 bread = $3.10
            Basket basket2 = new Basket() { Id = 2 };
            basket2.AddItem(butter);
            basket2.ChangeItemQuantity(butter, 2);
            basket2.AddItem(bread);
            basket2.ChangeItemQuantity(bread, 2);
            basket2.ApplyDiscount();
            Checkout(basket2);
            #endregion
            #region Scenario 3 --> 4 milk = $3.45
            Basket basket3 = new Basket() { Id = 3 };
            basket3.AddItem(milk);
            basket3.ChangeItemQuantity(milk, 4);
            basket3.ApplyDiscount();
            Checkout(basket3);
            #endregion
            #region Scenario 4 --> 2 butter, 1 bread, 8 milk = $9.00
            Basket basket4 = new Basket() { Id = 4};
            basket4.AddItem(butter);
            basket4.ChangeItemQuantity(butter, 2);
            basket4.AddItem(milk);
            basket4.ChangeItemQuantity(milk, 8);
            basket4.AddItem(bread);
            basket4.ChangeItemQuantity(bread, 1);
            basket4.ApplyDiscount();
            Checkout(basket4);
            #endregion

            Console.ReadKey();
        }
        public static void Checkout(Basket basket)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Basket id: {basket.Id} total requested @ {DateTime.Now}");

            foreach (BasketItem item in basket.Items)
            {
                sb.AppendLine($"Product name: {item.Product.Name}, Quantity: {item.Quantity}, Total: ${item.Total}");
            }

            if (!String.IsNullOrEmpty(basket.AppliedDiscounts))
            {
                sb.AppendLine(basket.AppliedDiscounts);
            }

            sb.AppendLine($"Basket total: ${basket.Total()}");

            CreateLogFile(sb.ToString());
            Console.WriteLine(sb.ToString());
        }
        private static void CreateLogFile(string logString)
        {
            string folderName = "Logs";
            string fileName = $"{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}_Log.txt";
            string filePathString = System.IO.Path.Combine(folderName, fileName);
            try
            {
                if (!System.IO.Directory.Exists(folderName))
                {
                    System.IO.Directory.CreateDirectory(folderName);
                }

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePathString, true))
                {
                    file.WriteLine(logString);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
