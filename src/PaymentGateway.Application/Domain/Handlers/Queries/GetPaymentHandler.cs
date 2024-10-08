using MediatR;

using PaymentGateway.Application.Domain.Model.Entities;
using PaymentGateway.Application.Domain.Model.Queries;
using PaymentGateway.Application.Ports;

namespace PaymentGateway.Application.Domain.Handlers.Queries
{
    public class GetPaymentHandler : IRequestHandler<GetPaymentQuery, Payment>
    {
        private readonly IPaymentsRepository _paymentsRepository;

        public GetPaymentHandler(IPaymentsRepository paymentsRepository)
        {
            _paymentsRepository = paymentsRepository;
        }

        public Task<Payment> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
        {

            var payment = _paymentsRepository.Get(request.Id);

            return Task.FromResult(payment);
        }
    }
}
