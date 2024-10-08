using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Application.Domain.Model.Entities
{
    public class Payment
    {
        public Payment(
            string number,
            int expiryMonth,
            int expiryYear,
            string cvv,
            string currency,
            int amount)
        {
            Id = Guid.NewGuid();
            Card = new Card(number, expiryMonth, expiryYear, cvv);            
            Amount = new Amount(amount, currency);
        }

        public Guid Id { get; private set; }
        public PaymentStatus Status { get; private set; }
        public Card Card { get; private set; }
        public Amount Amount { get; private set; }

        public void Authorize()
        {
            Status = PaymentStatus.Authorized;
        }

        public void Decline()
        {
            Status = PaymentStatus.Declined;
        }

        public void Reject()
        {
            Status = PaymentStatus.Rejected;
        }

        public bool IsValid()
        {
            return Card.IsValid() && Amount.IsValid();
        }
    }
}
