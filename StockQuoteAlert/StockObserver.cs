using StockQuoteAlert.Email;
using StockQuoteAlert.State;

namespace StockQuoteAlert.Observer;

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
            Console.WriteLine(@$"Detected price for {stock.targetStock} lower than the specified threshold. Sending a notification e-mail...");
            _emailHandler.SendEmails(stock, true);
        }
        else if (stock.price > _upperbound)
        {
            Console.WriteLine(@$"Detected price for {stock.targetStock} greater than the specified threshold. Sending a notification e-mail...");
            _emailHandler.SendEmails(stock, false);
        }
    }
}
