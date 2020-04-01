using System;
using System.Collections.Generic;
using System.Text;

namespace DelegatesAndEvents.Entities
{    
    public class Product
    {
        #region Properties

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public Category ProductCategory { get; set; }
        public int  Price { get; set; }

        #endregion

        /// <summary>
        /// Retreives the list of Products
        /// </summary>
        /// <returns></returns>
        public IList<Product> GetProducts() {
            var productList = new List<Product>();

            productList.Add(new Product() { ProductCategory = Category.Electronics, ProductId = 1, ProductName = "Samsung Mobile", Price = 12000 });
            productList.Add(new Product() { ProductCategory = Category.Electronics, ProductId = 2, ProductName = "IPhone", Price = 65000 });
            productList.Add(new Product() { ProductCategory = Category.Electronics, ProductId = 3, ProductName = "Samsung Washing Machine", Price = 30000 });
            productList.Add(new Product() { ProductCategory = Category.FootWear, ProductId = 4, ProductName = "Bata Shoes", Price = 2500 });
            productList.Add(new Product() { ProductCategory = Category.FootWear, ProductId = 5, ProductName = "Wood Land Shoes", Price = 2500 });
            productList.Add(new Product() { ProductCategory = Category.FootWear, ProductId = 6, ProductName = "Sparx Shoes", Price = 3000 });
            productList.Add(new Product() { ProductCategory = Category.FootWear, ProductId = 7, ProductName = "Nike Shoes", Price = 4000 });
            productList.Add(new Product() { ProductCategory = Category.Cloths, ProductId = 8, ProductName = "Branded Shirts", Price = 2000 });
            productList.Add(new Product() { ProductCategory = Category.Cloths, ProductId = 9, ProductName = "Trousers", Price = 2500 });

            return productList;
        }
    }

    public enum Category { 
        Electronics,
        FootWear, 
        Cloths
    }
}
