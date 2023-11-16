namespace NamespaceArguments;

internal class Arguments
{
    public string targetStock { get; set; }
    public decimal lowerbound { get; set; }
    public decimal upperbound { get; set; }

    public Arguments(string _targetStock, decimal _lowerbound, decimal _upperbound) 
    {
        targetStock = _targetStock;
        lowerbound = _lowerbound;
        upperbound = _upperbound;
    }
}
