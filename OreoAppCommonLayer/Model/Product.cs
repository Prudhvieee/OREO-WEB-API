using System;
using System.Collections.Generic;
using System.Text;

namespace OreoAppCommonLayer.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public long ProductQuantity { get; set; }
        public double ActualPrice { get; set; }
        public double DiscountedPrice { get; set; }
        public bool AddedToCart { get; set; }
    }
}
