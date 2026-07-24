using Application.Abstractions;
using Domain.Customers;
using FluentValidation;
using MediatR;
using Shared.Results;

namespace Application.Customers;

public sealed record CreateCustomerCommand(string LegalName, string Document, string Email, AddressDto BillingAddress) : IRequest<Result<CustomerDto>>;

public sealed class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerValidator()
    {
        RuleFor(command => command.LegalName).NotEmpty().MaximumLength(160);
        RuleFor(command => command.Document).NotEmpty().MaximumLength(32);
        RuleFor(command => command.Email).NotEmpty().EmailAddress().MaximumLength(254);
        RuleFor(command => command.BillingAddress.City).NotEmpty().MaximumLength(80);
        RuleFor(command => command.BillingAddress.Country).NotEmpty().MaximumLength(80);
    }
}

public sealed class CreateCustomerHandler(IApplicationDbContext dbContext, IValidator<CreateCustomerCommand> validator)
    : IRequestHandler<CreateCustomerCommand, Result<CustomerDto>>
{
    public async Task<Result<CustomerDto>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request, cancellationToken);
        if (!validation.IsValid)
            return Result.Failure<CustomerDto>(new Error("customers.validation", string.Join("; ", validation.Errors.Select(error => error.ErrorMessage))));

        var customer = new Customer(request.LegalName, request.Document, request.Email, request.BillingAddress.ToAddress());
        dbContext.Customers.Add(customer);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(new CustomerDto(customer.Id, customer.LegalName, customer.Document, customer.Email, AddressDto.FromAddress(customer.BillingAddress)));
    }
}
