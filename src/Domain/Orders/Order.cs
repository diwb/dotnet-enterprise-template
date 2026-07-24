using Domain.Common;

namespace Domain.Orders;

public sealed class Order : AggregateRoot
{
    private readonly List<OrderItem> _items = [];
    private readonly List<Payment> _payments = [];

    private Order() { }

    public Order(Guid customerId)
    {
        CustomerId = customerId;
        Status = OrderStatus.Draft;
        AddDomainEvent(new OrderCreatedDomainEvent(Id, customerId, DateTimeOffset.UtcNow));
    }

    public Guid CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    public IReadOnlyCollection<Payment> Payments => _payments.AsReadOnly();
    public decimal TotalAmount => _items.Sum(item => item.Total);

    public void AddItem(string sku, string description, int quantity, decimal unitPrice)
    {
        if (Status != OrderStatus.Draft)
            throw new InvalidOperationException("Only draft orders can be changed.");

        _items.Add(new OrderItem(sku, description, quantity, unitPrice));
    }

    public void Submit()
    {
        if (_items.Count == 0)
            throw new InvalidOperationException("An order requires at least one item.");

        Status = OrderStatus.Submitted;
        AddDomainEvent(new OrderSubmittedDomainEvent(Id, TotalAmount, DateTimeOffset.UtcNow));
    }

    public void RegisterPayment(string method, string externalReference)
    {
        if (Status is OrderStatus.Cancelled or OrderStatus.Draft)
            throw new InvalidOperationException("Only submitted orders can be paid.");

        var payment = new Payment(TotalAmount, method, externalReference);
        payment.Capture();
        _payments.Add(payment);
        Status = OrderStatus.Paid;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Paid)
            throw new InvalidOperationException("Paid orders cannot be cancelled in this template flow.");

        Status = OrderStatus.Cancelled;
    }
}

public sealed record OrderCreatedDomainEvent(Guid OrderId, Guid CustomerId, DateTimeOffset OccurredOnUtc) : IDomainEvent;
public sealed record OrderSubmittedDomainEvent(Guid OrderId, decimal TotalAmount, DateTimeOffset OccurredOnUtc) : IDomainEvent;
