using Microsoft.EntityFrameworkCore;

namespace DataProcessingTestForex.Services
{
    public class OrderProcessingService : IOrderProcessingService
    {
        private readonly ApplicationDbContext _context;

        public OrderProcessingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ProcessOrderAsync(BuySellOrder order)
        {
            var existingRate = await _context.ConversionRates
                .FirstOrDefaultAsync(r => r.CurrencyPair == order.CurrencyPair);

            if (existingRate == null)
            {
                existingRate = new ConversionRate
                {
                    CurrencyPair = order.CurrencyPair,
                    BuyPrice = order.BuyPrice,
                    SellPrice = order.SellPrice,
                    Timestamp = order.Timestamp
                };

                _context.ConversionRates.Add(existingRate);
            }
            else
            {
                existingRate.BuyPrice = order.BuyPrice;
                existingRate.SellPrice = order.SellPrice;
                existingRate.Timestamp = order.Timestamp;
            }

            await _context.SaveChangesAsync();
        }
    }

}
