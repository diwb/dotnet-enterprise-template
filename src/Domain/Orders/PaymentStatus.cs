namespace Domain.Orders;

public enum PaymentStatus
{
    Pending = 0,
    Authorized = 1,
    Captured = 2,
    Rejected = 3,
    Refunded = 4
}
