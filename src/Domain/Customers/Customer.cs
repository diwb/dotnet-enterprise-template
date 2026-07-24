using Domain.Common;

namespace Domain.Customers;

public sealed class Customer : AggregateRoot
{
    private Customer() { }

    public Customer(string legalName, string document, string email, Address billingAddress)
    {
        LegalName = legalName.Trim();
        Document = document.Trim();
        Email = email.Trim().ToLowerInvariant();
        BillingAddress = billingAddress;
        AddDomainEvent(new CustomerCreatedDomainEvent(Id, LegalName, DateTimeOffset.UtcNow));
    }

    public string LegalName { get; private set; } = string.Empty;
    public string Document { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public Address BillingAddress { get; private set; } = new("", "", "", "", "", "", "");

    public void UpdateProfile(string legalName, string email, Address billingAddress)
    {
        LegalName = legalName.Trim();
        Email = email.Trim().ToLowerInvariant();
        BillingAddress = billingAddress;
    }
}

public sealed record CustomerCreatedDomainEvent(Guid CustomerId, string LegalName, DateTimeOffset OccurredOnUtc) : IDomainEvent;
