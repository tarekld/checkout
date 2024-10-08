using MediatR;

using PaymentGateway.Application.Domain.Model.Entities;

namespace PaymentGateway.Application.Domain.Model.Commands
{
    public record AuthorisePaymentCommand : IRequest<Payment>
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public string CardNumber { get; set; }
        public string Currency { get; set; }
        public string Cvv { get; set; }
        public int ExpiryYear { get; set; }
        public int ExpiryMonth { get; set; }
    }
}
