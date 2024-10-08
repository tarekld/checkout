using PaymentGateway.Application.Domain.Model.Entities;
using PaymentGateway.Application.Ports;

namespace PaymentgGateway.Infrasturcture.Payments
{
    public class PaymentRepository: IPaymentsRepository
    {
        public List<Payment> Payments = new();

        public void Add(Payment payment)
        {
            Payments.Add(payment);
        }

        public Payment Get(Guid id)
        {
            return Payments.FirstOrDefault(p => p.Id == id);
        }
    }
}
