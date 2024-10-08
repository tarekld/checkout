using System.Text.Json.Serialization;

namespace PaymentGateway.Application.Domain.Model.Entities
{
    public class AcquirerBankResponse
    {
        [JsonPropertyName("authorization_code")]
        public string AuthorizationCode { get; set; }

        [JsonPropertyName("authorized")]
        public bool Authorized { get; set; }
    }
}