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
        Arguments parsedArgs = Utility.ParseArgs(args);
        Config? parsedConfig = Utility.ParseConfiguration("appconfig_original.json");
        Console.WriteLine($"Initializing Stock Alert for {parsedArgs!.targetStock}");

        var apiHandler = new StockAPI(parsedConfig!.api.key);
        var emailHandler = new EmailHandler(parsedConfig!.email.host, parsedConfig!.email.username, parsedConfig!.email.password);
        emailHandler.AddSender(parsedConfig!.email.senderList);

        var observer = new StockObserver(emailHandler, parsedArgs.lowerbound, parsedArgs.upperbound);
        var state = new StockState(parsedArgs.targetStock, apiHandler);
        state.Attach(observer);
        
        while (true)
        {
            await state.UpdateAndNotify();
            Thread.Sleep(10000);
        }
    }
}