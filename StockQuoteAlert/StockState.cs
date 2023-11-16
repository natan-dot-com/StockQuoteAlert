using NamespaceStockAPI;
using NamespaceStockObserver;

namespace NamespaceStockState;

internal class StockState
{
    private List<StockObserver> _observers = new List<StockObserver>();
    private StockAPI _API;
    
    public string targetStock { get; set; }
    public string currency { get; set; } = string.Empty;
    public decimal price { get; set; } = -1;

    public StockState(string stock, StockAPI stockAPI)
    {
        targetStock = stock;
        _API = stockAPI;
    }

    public void Attach(StockObserver observer) => _observers.Add(observer);

    public bool Remove(StockObserver observer) => _observers.Remove(observer);

    public async Task updateAndNotify()
    {
        (decimal fetchedPrice, string fetchedCurrency) = await _API.fetch(targetStock);
        
        price = fetchedPrice;
        currency = fetchedCurrency;

        foreach (StockObserver observer in _observers)
        {
            observer.Update(this);
        }
    }


}
