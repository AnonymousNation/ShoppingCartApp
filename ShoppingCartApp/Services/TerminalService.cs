using ShoppingCartApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartApp.Services
{
    public class TerminalService : ITerminal
    {
        private decimal _total;
        private Cart _cart = new Cart();
        private IProductService _productService;

        public TerminalService()
        {
            _productService = new ProductService();
        }

        public TerminalService(IProductService productService)
        {
            _productService = productService ?? new ProductService();
        }

        public void Clear()
        {
            _total = 0;
            _cart?.Products?.Clear();
        }

        public void Scan(string item)
        {
            //Can take input as, "ABC" or just "A"
            char[] productCodes = item.ToArray();  

            foreach(var productCode in productCodes)
            {
                Product product = null;

                try
                {
                    product = _productService.LookupProduct(productCode);
                }
                catch
                {
                    Console.WriteLine($"Product code: '{productCode}' not found.");
                }

                if(product != null)
                {
                    if(_cart.Products.ContainsKey(productCode))
                    {
                        _cart.Products[productCode].Quantity++;
                    }
                    else
                    {
                        product.Quantity = 1;
                        _cart.Products.Add(productCode, product);
                    }
                }
            }
        }

        public decimal Total()
        {
            if(_cart?.Products?.Count > 0)
            {
                foreach(var productCode in _cart.Products.Keys)
                {
                    int bulkDiscountMulitplier = 0;
                    int productUnitCount = 1;

                    Product currentProduct = _cart.Products[productCode];

                    if (currentProduct.HasBulkDiscont && currentProduct.Quantity >= currentProduct.DiscountQuantity)
                    {
                        bulkDiscountMulitplier = (int)currentProduct.Quantity / currentProduct.DiscountQuantity;
                        productUnitCount = currentProduct.Quantity % currentProduct.DiscountQuantity;
                    }
                    else
                    {
                        productUnitCount = currentProduct.Quantity;
                    }

                    _total += bulkDiscountMulitplier * currentProduct.DiscountQuantityPrice + productUnitCount * currentProduct.Price;

                }

            }
            else
            {
                Console.WriteLine("Shopping cart is empty.");
            }

            return _total;
        }
    }
}
