using NamespaceStockState;
using NamespaceStockAPI;
using NamespaceEmailHandler;
using NamespaceUtility;
using NamespaceConfig;
using NamespaceArguments;
using NamespaceStockObserver;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        Arguments parsedArgs = Utility.parseArgs(args);
        Config? parsedConfig = Utility.parseConfiguration("C:\\Users\\natan\\source\\repos\\StockQuoteAlert\\StockQuoteAlert\\Config.json");
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