namespace Domain.Customers;

public sealed record Address(
    string Street,
    string Number,
    string District,
    string City,
    string State,
    string PostalCode,
    string Country);
