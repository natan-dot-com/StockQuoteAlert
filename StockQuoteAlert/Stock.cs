using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamespaceStock;

internal class Stock
{
    string _stock;
    string _currency;
    decimal _price;

    public Stock(string stock, decimal price, string currency)
    {
        _stock = stock;
        _price = price;
        _currency = currency;
    }

    public override string ToString()
    {
        return  _stock + ": " + _price.ToString() + _currency;
    }
}
