using Domain.Customers;

namespace Application.Customers;

public sealed record AddressDto(string Street, string Number, string District, string City, string State, string PostalCode, string Country)
{
    public Address ToAddress() => new(Street, Number, District, City, State, PostalCode, Country);
    public static AddressDto FromAddress(Address address) => new(address.Street, address.Number, address.District, address.City, address.State, address.PostalCode, address.Country);
}

public sealed record CustomerDto(Guid Id, string LegalName, string Document, string Email, AddressDto BillingAddress);
