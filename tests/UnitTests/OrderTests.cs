using Domain.Orders;

namespace UnitTests;

public sealed class OrderTests
{
    [Fact]
    public void Submit_requires_at_least_one_item()
    {
        var order = new Order(Guid.NewGuid());

        var exception = Assert.Throws<InvalidOperationException>(order.Submit);

        Assert.Contains("at least one item", exception.Message);
    }

    [Fact]
    public void RegisterPayment_captures_total_and_marks_order_paid()
    {
        var order = new Order(Guid.NewGuid());
        order.AddItem("sku-1", "Professional support plan", 2, 150m);
        order.Submit();

        order.RegisterPayment("credit-card", "pay_123");

        Assert.Equal(OrderStatus.Paid, order.Status);
        Assert.Equal(300m, order.Payments.Single().Amount);
        Assert.Equal(PaymentStatus.Captured, order.Payments.Single().Status);
    }
}
