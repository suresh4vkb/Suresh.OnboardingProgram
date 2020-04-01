using System;
using System.Collections.Generic;
using System.Text;

namespace DelegatesAndEvents.Entities
{
    public class CartProduct
    {
        #region Properties

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ActualPrice { get; set; }
        public int DiscountPrice { get; set; }
        public int DiscountPercent { get; set; }
        public int FinalPrice { get; set; }
        #endregion
    }
}
