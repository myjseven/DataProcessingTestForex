using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderProcessingService _orderProcessingService;

    public OrderController(IOrderProcessingService orderProcessingService)
    {
        _orderProcessingService = orderProcessingService;
    }

    // POST api/order
    [HttpPost]
    public async Task<IActionResult> PostOrder([FromBody] BuySellOrder order)
    {
        if (order == null || string.IsNullOrWhiteSpace(order.CurrencyPair))
        {
            return BadRequest("Invalid order data.");
        }

        await _orderProcessingService.ProcessOrderAsync(order);

        return Ok("Order processed successfully.");
    }
}
