using System.Text.Json.Nodes;

using NamespaceStockState;

namespace NamespaceStockAPI;

internal class StockAPI
{
    private string _key;
    private HttpClient _httpClient;

    public StockAPI(string key)
    {
        _key = key;
        _httpClient = new HttpClient();
    }

    public async Task<(decimal, string)> fetch(string stock)
    {
        string content = string.Empty;
        string url = makeUrl(stock);

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

    private string makeUrl(string stock) => String.Format("https://brapi.dev/api/quote/{0}?token={1}", stock, _key);
}
