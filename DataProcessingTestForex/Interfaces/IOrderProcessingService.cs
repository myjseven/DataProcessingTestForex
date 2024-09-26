using Microsoft.EntityFrameworkCore;

public interface IOrderProcessingService
{
    Task ProcessOrderAsync(BuySellOrder order);
}
