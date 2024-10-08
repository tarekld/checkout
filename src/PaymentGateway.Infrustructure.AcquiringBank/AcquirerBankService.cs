using System.Net.Http.Json;

using PaymentGateway.Application.Domain.Model.Entities;
using PaymentGateway.Application.Ports;

namespace PaymentGateway.Infrustructure.AcquiringBank
{
    public class AcquirerBankService : IAcquirerBankService
    {
        private readonly HttpClient _httpCLient;

        public AcquirerBankService(HttpClient httpClient)
        {
            _httpCLient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<AcquirerBankResponse> AuthoriseAsync(AcquirerPaymentRequest acquirerPaymentRequest, CancellationToken cancellationToken)
        {

            try
            {
                var response = await _httpCLient.PostAsJsonAsync(
                    "payments",
                    new
                    {
                        currency = acquirerPaymentRequest.Currency,
                        cvv = acquirerPaymentRequest.Cvv.ToString(),
                        amount = acquirerPaymentRequest.Amount,
                        expiry_date = acquirerPaymentRequest.ExpiryDate,
                        card_number = acquirerPaymentRequest.CardNumber
                    },
                    cancellationToken);

                response.EnsureSuccessStatusCode();

                var acquirerBankResponse = await response.Content.ReadFromJsonAsync<AcquirerBankResponse>(cancellationToken);

                return acquirerBankResponse;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
