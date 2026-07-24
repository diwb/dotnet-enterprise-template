using Domain.Orders;

namespace Application.Orders;

public sealed record OrderItemDto(string Sku, string Description, int Quantity, decimal UnitPrice, decimal Total);
public sealed record CreateOrderItemRequest(string Sku, string Description, int Quantity, decimal UnitPrice);
public sealed record OrderDto(Guid Id, Guid CustomerId, OrderStatus Status, decimal TotalAmount, IReadOnlyCollection<OrderItemDto> Items);
