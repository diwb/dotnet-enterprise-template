namespace Domain.Orders;

public sealed class Payment
{
    private Payment() { }

    public Payment(decimal amount, string method, string externalReference)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Payment amount must be positive.");

        Id = Guid.NewGuid();
        Amount = amount;
        Method = method.Trim();
        ExternalReference = externalReference.Trim();
        Status = PaymentStatus.Pending;
    }

    public Guid Id { get; private set; }
    public decimal Amount { get; private set; }
    public string Method { get; private set; } = string.Empty;
    public string ExternalReference { get; private set; } = string.Empty;
    public PaymentStatus Status { get; private set; }

    public void Capture() => Status = PaymentStatus.Captured;
    public void Reject() => Status = PaymentStatus.Rejected;
}
