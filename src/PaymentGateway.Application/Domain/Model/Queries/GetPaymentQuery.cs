using MediatR;

using PaymentGateway.Application.Domain.Model.Entities;

namespace PaymentGateway.Application.Domain.Model.Queries
{
    public record GetPaymentQuery : IRequest<Payment>
    {
        public Guid Id { get; set; }
    }
}
