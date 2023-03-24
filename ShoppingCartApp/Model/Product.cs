using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartApp.Model
{
    public class Product
    {
        public decimal Price { get; set; }
        public char Code { get; set; }
        public int Quantity { get; set; }
        public int DiscountQuantity { get; set; } = 1;
        public decimal DiscountQuantityPrice { get; set; } = 1.0M;
        public bool HasBulkDiscont { get { return DiscountQuantity > 1; } }

    }
}
