using StockQuoteAlert.State;
using StockQuoteAlert.API;
using StockQuoteAlert.Email;
using StockQuoteAlert.Utility;
using StockQuoteAlert.Parsing;
using StockQuoteAlert.Observer;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        Arguments parsedArgs = Utility.parseArgs(args);
        Config? parsedConfig = Utility.parseConfiguration("appconfig.json");
        Console.WriteLine($"Initializing Stock Alert for {parsedArgs!.targetStock}");

        var APIHandler = new StockAPI(parsedConfig!.API.key);
        var emailHandler = new EmailHandler(parsedConfig!.Email.host, parsedConfig!.Email.username, parsedConfig!.Email.password);
        emailHandler.addSender(parsedConfig!.Email.senderList);

        var observer = new StockObserver(emailHandler, parsedArgs.lowerbound, parsedArgs.upperbound);
        var state = new StockState(parsedArgs.targetStock, APIHandler);
        state.Attach(observer);
        
        while (true)
        {
            await state.updateAndNotify();
            Thread.Sleep(10000);
        }
    }
}