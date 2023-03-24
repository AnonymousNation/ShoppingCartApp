using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartApp.Model
{
    public class Cart
    {
        public Dictionary<char, Product> Products { get; set; } = new Dictionary<char, Product>();    

    }
}
