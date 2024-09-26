using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class ConversionRate
{
    public int Id { get; set; }
    public string CurrencyPair { get; set; } 
    public decimal BuyPrice { get; set; }
    public decimal SellPrice { get; set; }
    public DateTime Timestamp { get; set; } 
}

public class ApplicationDbContext : DbContext
{
    public DbSet<ConversionRate> ConversionRates { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
}
