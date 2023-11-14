using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

using NamespaceStock;
using NamespaceStockAPI;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        string stock = "PETR4";
        string key = "eTD1U371z4ybsxyDhkCCDg";

        var APIHandler = new StockAPI(key);
        Stock body = await APIHandler.fetch(stock);
        Console.WriteLine(body.ToString()); 
    }
}