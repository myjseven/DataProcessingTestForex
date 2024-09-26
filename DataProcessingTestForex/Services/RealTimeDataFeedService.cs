public class RealTimeDataFeedService : BackgroundService
{
    private readonly IOrderProcessingService _orderProcessingService;

    public RealTimeDataFeedService(IOrderProcessingService orderProcessingService)
    {
        _orderProcessingService = orderProcessingService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var random = new Random();
        var currencyPairs = new[] { "BTC/USD", "BTC/PHP", "BTC/EUR", "BTC/JPY" /* Add more pairs */ };

        while (!stoppingToken.IsCancellationRequested)
        {
            var newOrder = new BuySellOrder
            {
                CurrencyPair = currencyPairs[random.Next(currencyPairs.Length)],
                BuyPrice = (decimal)(random.NextDouble() * 10000 + 20000), // Random buy price
                SellPrice = (decimal)(random.NextDouble() * 10000 + 20000), // Random sell price
                Timestamp = DateTime.UtcNow
            };

            // Send the order for processing
            await _orderProcessingService.ProcessOrderAsync(newOrder);

            await Task.Delay(5); // Simulate high-frequency orders
        }
    }
}
