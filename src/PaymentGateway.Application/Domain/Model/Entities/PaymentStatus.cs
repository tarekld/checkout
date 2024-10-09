namespace PaymentGateway.Application.Domain.Model.Entities
{
    public enum PaymentStatus
    {
        Requested,
        Authorized,
        Declined,
        Rejected
    }
}