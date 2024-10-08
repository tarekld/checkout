
using PaymentGateway.Application.Domain.Model.Entities;

namespace PaymentGateway.Application.Ports
{
    public interface IPaymentsRepository
    {
        void Add(Payment payment);
        Payment Get(Guid id);
    }
}
