using Application.Abstractions;
using Domain.Orders;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;

namespace Application.Orders;

public sealed record CreateOrderCommand(Guid CustomerId, IReadOnlyCollection<CreateOrderItemRequest> Items) : IRequest<Result<OrderDto>>;

public sealed class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(command => command.CustomerId).NotEmpty();
        RuleFor(command => command.Items).NotEmpty();
        RuleForEach(command => command.Items).ChildRules(item =>
        {
            item.RuleFor(x => x.Sku).NotEmpty().MaximumLength(64);
            item.RuleFor(x => x.Description).NotEmpty().MaximumLength(200);
            item.RuleFor(x => x.Quantity).GreaterThan(0);
            item.RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0);
        });
    }
}

public sealed class CreateOrderHandler(IApplicationDbContext dbContext, IValidator<CreateOrderCommand> validator)
    : IRequestHandler<CreateOrderCommand, Result<OrderDto>>
{
    public async Task<Result<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
            return Result.Failure<OrderDto>(new Error("orders.validation", string.Join("; ", validation.Errors.Select(error => error.ErrorMessage))));

        var customerExists = await dbContext.Customers.AnyAsync(customer => customer.Id == request.CustomerId && !customer.IsDeleted, cancellationToken);
        if (!customerExists)
            return Result.Failure<OrderDto>(new Error("orders.customer_not_found", "Customer was not found."));

        var order = new Order(request.CustomerId);
        foreach (var item in request.Items)
            order.AddItem(item.Sku, item.Description, item.Quantity, item.UnitPrice);

        order.Submit();
        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(ToDto(order));
    }

    private static OrderDto ToDto(Order order) => new(
        order.Id,
        order.CustomerId,
        order.Status,
        order.TotalAmount,
        order.Items.Select(item => new OrderItemDto(item.Sku, item.Description, item.Quantity, item.UnitPrice, item.Total)).ToArray());
}
