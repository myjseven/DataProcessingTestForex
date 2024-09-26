public class BuySellOrder
{
    public string CurrencyPair { get; set; } // e.g., "BTC/USD"
    public decimal BuyPrice { get; set; }
    public decimal SellPrice { get; set; }
    public DateTime Timestamp { get; set; } // When the order was placed
}