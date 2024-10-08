using MediatR;

using PaymentGateway.Application.Domain.Model.Commands;
using PaymentGateway.Application.Domain.Model.Entities;
using PaymentGateway.Application.Ports;

namespace PaymentGateway.Application.Domain.Handlers.Commands
{
    public class PaymentAuthorizationHandler : IRequestHandler<AuthorisePaymentCommand, Payment>
    {
        private readonly IPaymentsRepository _paymentsRepository;
        private readonly IAcquirerBankService _acquirerBankService;

        public PaymentAuthorizationHandler(
            IPaymentsRepository paymentsRepository,
            IAcquirerBankService acquirerBankService)
        {
            _paymentsRepository = paymentsRepository;
            _acquirerBankService = acquirerBankService;
        }

        public async Task<Payment> Handle(AuthorisePaymentCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            Payment payment = new Payment(
                request.CardNumber,
                request.ExpiryMonth,
                request.ExpiryYear,
                request.Cvv,
                request.Currency,
                request.Amount);
            _paymentsRepository.Add(payment);

            if (!payment.IsValid())
            {
                payment.Reject();
                return payment;
            }

            var response = await _acquirerBankService.AuthoriseAsync(
                new AcquirerPaymentRequest()
                {
                    Amount = request.Amount,
                    CardNumber = request.CardNumber,
                    Currency = request.Currency,
                    Cvv = request.Cvv,
                    ExpiryDate = $"{request.ExpiryMonth.ToString("00")}/{request.ExpiryYear}"
                  },
                cancellationToken);
            if(response.Authorized)
            {
                payment.Authorize();
            }
            else
            {
                payment.Decline();
            }

            return payment;
        }
    }
}
