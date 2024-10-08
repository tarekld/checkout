using PaymentGateway.Application.Domain.Model.Entities;

namespace PaymentGateway.Application.UnitTests.Domain.Model.Entities
{
    public class CardTests
    {
        [Theory]
        [InlineData("1234567898765432", 2025, 01, "123", true)]
        [InlineData("12345678987654323", 2025, 01, "123", false)]
        [InlineData("1234567", 2025, 01, "123", false)]
        [InlineData("1234567898765432", 2024, 01, "123", false)]
        [InlineData("1234567898765432", 2025, 01, "12", false)]
        [InlineData(null, 2025, 01, "12", false)]
        [InlineData("1234567898765432", 2025, 01, null, false)]
        public void CardValidationTest(string cardNumber, int expiryMonth, int expiryYear, string cvv, bool isValid)
        {
            var card = new Card(cardNumber,  expiryYear, expiryMonth, cvv);

            Assert.Equal(isValid, card.IsValid());
        }
    }
}
