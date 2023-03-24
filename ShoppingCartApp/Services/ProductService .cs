using ShoppingCartApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartApp.Services
{
    public class ProductService : IProductService
    {
        //Could also be exteneded to take in a list of products via DI
        //public ProductService() { }

        public Product LookupProduct(char productCode)
        {
            Product product;

            switch (productCode)
            {
                case 'A':
                    product = new Product { Code = productCode, Price = 2.0M, DiscountQuantity = 4, DiscountQuantityPrice = 7.0M };
                    break;

                case 'B':
                    product = new Product { Code = productCode, Price = 12.0M };
                    break;

                case 'C':
                    product = new Product { Code = productCode, Price = 1.250M, DiscountQuantity = 6, DiscountQuantityPrice = 6.0M };
                    break;

                case 'D':
                    product = new Product { Code = productCode, Price = 0.150M };
                    break;

                default:
                    throw new ArgumentException("No product found for product code: " + productCode);

            }

            return product;
        }      
    }
}
