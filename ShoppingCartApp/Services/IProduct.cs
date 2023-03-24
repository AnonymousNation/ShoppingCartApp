using ShoppingCartApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartApp.Services
{
    public interface IProductService
    {
        Product LookupProduct(char productCode);

    }
}
