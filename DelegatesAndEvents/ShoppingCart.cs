using System;
using System.Collections.Generic;
using System.Linq;
using Common.Utilities;
using DelegatesAndEvents.Entities;

namespace DelegatesAndEvents
{
    /// <summary>
    /// Demostrates the Shopping Cart Task using Delegates and Events
    /// </summary>
    public class ShoppingCart
    {
        #region Declarations

        // Declare delegates
        private delegate void PrintProductsDelegate();
        private delegate void PrintSeasonalDiscountDelegate();
        private delegate void AddProductToCartDelegate();
        private delegate void PrintCartProductsDelegate();

        // Declare event
        static event PrintProductsDelegate ShowProductsEvent;
        static event PrintSeasonalDiscountDelegate ShowDiscountsEvent;
        static event AddProductToCartDelegate AddProductToCartEvent;
        static event PrintCartProductsDelegate ShowCartProductsEvent;

        // products added to carts
        private static IList<CartProduct> ProductsAddedToCart;
        private static IDictionary<string, int> CategoryWiseDiscount;
        enum Festivals
        {
            NewYear,
            Ugadi,
            Ramadan,
            Dussehra,
            Diwali,
            Christmas,
        }

        #endregion

        #region Implementation of Shopping Cart Task

        public static void PerformBilling()
        {
            ProductsAddedToCart = new List<CartProduct>();
            ShowProductsEvent += ShowProductsEventHandler;
            ShowDiscountsEvent += ShowDiscountsEventHandler;

            // Displays the Available Products
            ShowProductsEvent();

            // Shows the Seasonal Discounts
            ShowDiscountsEvent();

            AddProductToCartEvent += AddProductToCartEventHandler;
            ShowCartProductsEvent += ShowCartProductsEventHandler;

            Message.Print("\nNote: After selecting three products, Invoice will be generated.");
            // Add 3 products to Cart.
            for (int i = 0; i < 3; i++)
            {
                AddProductToCartEvent();
            }

            // Displays the Cart Products and Total Amount
            ShowCartProductsEvent();
        }

        #endregion

        #region Helper Methods and Event Handlers
        private static void ShowCartProductsEventHandler()
        {
            if (ProductsAddedToCart.Count > 0)
            {
                int TotalPrice = ProductsAddedToCart.Sum(x => x.ActualPrice);
                int TotalDiscount = ProductsAddedToCart.Sum(x => x.DiscountPrice);
                int TotalNetPrice = TotalPrice - TotalDiscount;

                Message.PrintTask("\nPlease wait... Generating Invoice for the Products");
                //Message.Print("------------------------------------------------------");
                foreach (var cartProduct in ProductsAddedToCart)
                    Message.Print(string.Format("Product Id: {0}, Name: {1}, Acutal Price: {2}, Discount Price (%):{3} ({4}%),  Final Price: {5}", cartProduct.ProductId, cartProduct.ProductName, cartProduct.ActualPrice, cartProduct.DiscountPrice, cartProduct.DiscountPercent, cartProduct.FinalPrice));

                Message.Print(string.Format("\nTotal Price \t\t: \t Rs. {0} \nTotal Discount \t\t: \t Rs. {1} \nTotal Net Price \t: \t Rs. {2}", TotalPrice, TotalDiscount, TotalNetPrice));
            }
            else
                Message.Print("Your cart is empty. Please add products to your cart");
        }

        private static void AddProductToCartEventHandler()
        {
            int productId = SelectInput();
            Product products = new Product();

            var product = products.GetProducts().Where(x => x.ProductId == productId).SingleOrDefault();
            if (product != null)
            {
                CartProduct cartProduct = new CartProduct
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ActualPrice = product.Price,
                    DiscountPercent = CategoryWiseDiscount[product.ProductCategory.ToString()],
                    DiscountPrice = CalculateDiscount(product.ProductCategory.ToString(), product.Price),
                    FinalPrice = CalculateDiscountPrice(product.ProductCategory.ToString(), product.Price)
                };

                ProductsAddedToCart.Add(cartProduct);
                Message.Print(string.Format("Product Id: {0}, Name: {1}, Acutal Price: {2}, Discount Price (%):{3} ({4}%),  Final Price: {5}", cartProduct.ProductId, cartProduct.ProductName, cartProduct.ActualPrice, cartProduct.DiscountPrice, cartProduct.DiscountPercent, cartProduct.FinalPrice));
                Message.Print(string.Format("Product '{0}' added to your cart, {1}. Total Products in the cart is {2}", cartProduct.ProductName, cartProduct.DiscountPercent > 0 ? cartProduct.DiscountPercent + "% discount applied" : "No discount", ProductsAddedToCart.Count()));
            }
        }

        private static int SelectInput()
        {
            Message.PrintWithOutNewLine("\nPlease enter Product Id: ");
            int productId;
            int.TryParse(Console.ReadLine(), out productId);
            if (productId == 0 || productId > 9)
            {
                Message.Print("\nInvalid Product Id. Please enter valid Product Id");
                return SelectInput();
            }
            else
                return productId;
        }

        private static void ShowProductsEventHandler()
        {
            Message.PrintTask("\nProducts available in the Shop");
            Product objProduct = new Product();
            foreach (var product in objProduct.GetProducts())
            {
                Message.Print(string.Format("Product Id: {0}, Name: {1}, Category: {2}, Price: {3}", product.ProductId, product.ProductName, product.ProductCategory, product.Price));
            }
        }

        private static void ShowDiscountsEventHandler()
        {
            string seasonName = GetFestivalNameByDate();
            int[] discounts;
            switch (seasonName)
            {
                case "NewYear":
                    discounts = new int[3] { 10, 20, 0 };
                    AddCategoryDiscount(discounts, seasonName);
                    break;
                case "Ugadi":
                    discounts = new int[3] { 30, 0, 20 };
                    AddCategoryDiscount(discounts, seasonName);
                    break;
                case "Ramadan":
                    discounts = new int[3] { 40, 10, 20 };
                    AddCategoryDiscount(discounts, seasonName);
                    break;
                case "Dussehra":
                    discounts = new int[3] { 30, 20, 25 };
                    AddCategoryDiscount(discounts, seasonName);
                    break;
                default:
                    discounts = new int[3] { 5, 10, 2 };
                    AddCategoryDiscount(discounts, "Default");
                    break;
            }
        }

        private static string GetFestivalNameByDate()
        {
            if (DateTime.Today >= new DateTime(2020, 1, 1) && DateTime.Today <= new DateTime(2020, 1, 28))
                return Festivals.NewYear.ToString();

            if (DateTime.Today >= new DateTime(2020, 3, 22) && DateTime.Today <= new DateTime(2020, 3, 31))
                return Festivals.Ugadi.ToString();

            if (DateTime.Today >= new DateTime(2020, 4, 23) && DateTime.Today <= new DateTime(2020, 5, 23))
                return Festivals.Ramadan.ToString();

            if (DateTime.Today >= new DateTime(2020, 10, 20) && DateTime.Today <= new DateTime(2020, 10, 30))
                return Festivals.Dussehra.ToString();

            return string.Empty;
        }

        private static void AddCategoryDiscount(int[] discounts, string seasonName)
        {
            CategoryWiseDiscount = new Dictionary<string, int>
            {
                { Category.Electronics.ToString(), discounts[0] },
                { Category.FootWear.ToString(), discounts[1] },
                { Category.Cloths.ToString(), discounts[2] }
            };

            Message.Print(string.Format("\n {0} Season is ON. You are eligible for ", seasonName));
            foreach (var category in CategoryWiseDiscount)
                if (category.Value > 0)   // Displays the categories which have discounts
                    Message.Print(string.Format("{0}% discount on {1}", category.Value, category.Key));

        }

        private static int CalculateDiscount(string category, int price)
        {
            int discountPercent = CategoryWiseDiscount[category];
            return price * discountPercent / 100;
        }

        private static int CalculateDiscountPrice(string category, int price)
        {
            int discountPercent = CategoryWiseDiscount[category];
            return price - price * discountPercent / 100;
        }

        #endregion
    }
}
