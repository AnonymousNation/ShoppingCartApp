using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartApp.Services
{
    public interface ITerminal
    {
        void Scan(string item);
        void Clear();
        decimal Total();

    }
}
