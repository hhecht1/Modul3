using System;
using System.Text;
using System.Text.Json;

public class Customer
{
    public string? Name { get; set; }
    public string? Email { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public Customer? Customer { get; set; }
    public double Amount { get; set; }
    public bool IsProcessed { get; set; }

    public string GetContactEmail()
    {
        return Customer?.Email ?? "no-reply@shop.com";
    }
}

public class OrderParsingException : Exception
{
    public OrderParsingException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

public class OrderProcessor
{
    public Action<Order>? PremiumOrderHandler { get; set; }
    public Func<double, double>? TaxCalculator { get; set; }

    public async Task<List<Order>> LoadOrderAsync(Stream stream)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<Order>? orders =
                await JsonSerializer.DeserializeAsync<List<Order>>(stream, options);

            return orders ?? new List<Order>();
        }
        catch (JsonException ex)
        {
            throw new OrderParsingException("Failed to parse orders", ex);
        }
    }

    public void ProcessOrders(List<Order> orders)
    {
        TaxCalculator ??= amount => amount * 1.20;

        var validOrders = orders
            .Where(o => o.Customer != null && o.IsProcessed == false);

        foreach (Order order in validOrders)
        {
            order.Amount = TaxCalculator(order.Amount);

            if (order.Amount > 100)
            {
                PremiumOrderHandler?.Invoke(order);
            }

            order.IsProcessed = true;
        }
    }
}

class Program
{
    public static async Task Main()
    {
        string jsonMock = """
        [
            {"id":1,"customer":{"name":"Alice","email":"alice@example.com"},"amount":150,"isProcessed":false},
            {"id":2,"customer":{"name":"Bob","email":"bob@example.com"},"amount":80,"isProcessed":false},
            {"id":3,"customer":null,"amount":50,"isProcessed":false},
            {"id":4,"customer":{"name":"Charlie","email":"charlie@example.com"},"amount":120,"isProcessed":false}
        ]
        """;

        using MemoryStream stream =
            new MemoryStream(Encoding.UTF8.GetBytes(jsonMock));

        OrderProcessor processor = new();

        processor.PremiumOrderHandler = order =>
        {
            Console.WriteLine(
                $"Premium order detected: Id={order.Id}, Amount={order.Amount}, Customer={order.Customer?.Name}"
            );
        };

        processor.TaxCalculator = amount => amount * 1.15;

        List<Order> orders = await processor.LoadOrderAsync(stream);

        processor.ProcessOrders(orders);

        Console.WriteLine("\nAlle Bestellungen:");

        foreach (Order order in orders)
        {
            Console.WriteLine(
                $"Id={order.Id}, Amount={order.Amount}, IsProcessed={order.IsProcessed}, ContactEmail={order.GetContactEmail()}"
            );
        }
    }
}