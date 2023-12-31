﻿namespace StockQuoteAlert.Parsing;

internal class Arguments
{
    public string targetStock { get; set; }
    public decimal lowerbound { get; set; }
    public decimal upperbound { get; set; }

    public Arguments(string targetStock, decimal lowerbound, decimal upperbound) 
    {
        this.targetStock = targetStock;
        this.lowerbound = lowerbound;
        this.upperbound = upperbound;
    }
}
