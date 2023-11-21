using System.Text.Json.Nodes;

namespace StockQuoteAlert.API;

internal class StockAPI
{
    private string _key;
    private HttpClient _httpClient;

    public StockAPI(string key)
    {
        _key = key;
        _httpClient = new HttpClient();
    }

    public async Task<(decimal, string)> Fetch(string stock)
    {
        string content = string.Empty;
        string url = MakeUrl(stock);

        try
        {
            using var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            content = await response.Content.ReadAsStringAsync();
        } 
        catch (Exception ex) 
        {
            Console.WriteLine($"Failed to fetch data from {stock}: {ex.Message}");
        }

        var parsed = JsonNode.Parse(content)!["results"]![0]!;
        
        decimal price = parsed["regularMarketPrice"]!.GetValue<decimal>();
        string currency = parsed["currency"]!.GetValue<string>();
        return (price, currency);
    }

    private string MakeUrl(string stock) => String.Format("https://brapi.dev/api/quote/{0}?token={1}", stock, _key);
}
