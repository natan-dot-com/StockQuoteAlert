using NamespaceEmailHandler;
using NamespaceStockState;

namespace NamespaceStockObserver;

internal class StockObserver
{
    private decimal _lowerbound;
    private decimal _upperbound;
    private EmailHandler _emailHandler;

    public StockObserver(EmailHandler emailHandler, decimal lowerbound, decimal upperbound)
    {
        _lowerbound = lowerbound;
        _upperbound = upperbound;
        _emailHandler = emailHandler;
    }

    public void Update(StockState stock)
    {
        if (stock.price == -1 || stock.currency == string.Empty)
        {
            throw new InvalidOperationException("Received state with no information.");
        }

        if (stock.price < _lowerbound)
        {
            _emailHandler.sendEmails(stock, true);
        }
            
        else if (stock.price > _upperbound)
        {
            _emailHandler.sendEmails(stock, false);
        }
    }
}
