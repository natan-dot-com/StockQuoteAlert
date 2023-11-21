using System.Globalization;
using System.Text.Json;

using StockQuoteAlert.Parsing;

namespace StockQuoteAlert.Utility;

internal class Utility
{
    public static Config? ParseConfiguration(string jsonFilePath)
    {
        string text = File.ReadAllText(jsonFilePath);
        return JsonSerializer.Deserialize<Config>(text);
    }

    public static Arguments ParseArgs(string[] args)
    {
        string targetStock = args[0];
        decimal lowerbound = 0.0M;
        decimal upperbound = 0.0M;
        
        try
        {
            lowerbound = Convert.ToDecimal(args[1], CultureInfo.InvariantCulture);
            upperbound = Convert.ToDecimal(args[2], CultureInfo.InvariantCulture);
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Failed to parse CLI arguments: {ex.Message}");
        }

        return new Arguments(targetStock, lowerbound, upperbound);
    }
}
