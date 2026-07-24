using Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Pagination;

namespace Application.Customers;

public sealed record GetCustomersQuery(QueryParameters Parameters) : IRequest<PagedResult<CustomerDto>>;

public sealed class GetCustomersHandler(IApplicationDbContext dbContext) : IRequestHandler<GetCustomersQuery, PagedResult<CustomerDto>>
{
    public async Task<PagedResult<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.Customers.AsNoTracking().Where(customer => !customer.IsDeleted);

        if (!string.IsNullOrWhiteSpace(request.Parameters.Search))
        {
            var term = request.Parameters.Search.Trim();
            query = query.Where(customer => customer.LegalName.Contains(term) || customer.Email.Contains(term) || customer.Document.Contains(term));
        }

        query = (request.Parameters.SortBy?.ToLowerInvariant(), request.Parameters.SortDescending) switch
        {
            ("email", true) => query.OrderByDescending(customer => customer.Email),
            ("email", false) => query.OrderBy(customer => customer.Email),
            ("document", true) => query.OrderByDescending(customer => customer.Document),
            ("document", false) => query.OrderBy(customer => customer.Document),
            (_, true) => query.OrderByDescending(customer => customer.LegalName),
            _ => query.OrderBy(customer => customer.LegalName)
        };

        var total = await query.CountAsync(cancellationToken);
        var items = await query.Skip(request.Parameters.Skip).Take(request.Parameters.Take)
            .Select(customer => new CustomerDto(
                customer.Id,
                customer.LegalName,
                customer.Document,
                customer.Email,
                AddressDto.FromAddress(customer.BillingAddress)))
            .ToListAsync(cancellationToken);

        return new PagedResult<CustomerDto>(items, request.Parameters.PageNumber, request.Parameters.Take, total);
    }
}
