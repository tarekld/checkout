using PaymentGateway.Application.Domain.Model.Entities;

namespace PaymentGateway.Application.Ports
{
    public interface IAcquirerBankService
    {
        Task<AcquirerBankResponse> AuthoriseAsync(AcquirerPaymentRequest acquirerPaymentRequest, CancellationToken cancellationToken);
    }
}