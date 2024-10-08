using Moq;

using PaymentGateway.Application.Domain.Handlers.Queries;
using PaymentGateway.Application.Domain.Model.Entities;
using PaymentGateway.Application.Ports;

namespace PaymentGateway.Application.UnitTests.Domain.Handlers.Queries
{
    public class GetPaymentHandlerTests
    {
        private readonly IPaymentsRepository _paymentsRepository;
        private readonly GetPaymentHandler _handler;
        public GetPaymentHandlerTests()
        {
            _paymentsRepository = Mock.Of<IPaymentsRepository>();
            _handler = new GetPaymentHandler(_paymentsRepository);
        }

        [Fact]
        public async Task GivenPayment_WHenExits_ShouldReturnThePayment()
        {
            var paymentId = Guid.NewGuid();
            var payment = new Payment("1234567891234567", 1, 2025, "123", "GBP", 100);
            Mock.Get(_paymentsRepository).Setup(x => x.Get(paymentId)).Returns(payment);
            var paymentFound = await _handler.Handle(new Application.Domain.Model.Queries.GetPaymentQuery() { Id = paymentId }, CancellationToken.None);

            Assert.NotNull(paymentFound);
            Assert.Equal(payment.Card.Number, paymentFound.Card.Number);
        }

        [Fact]
        public async Task GivenPayment_WHenExits_ShouldReturnNull()
        {
            var paymentId = Guid.NewGuid();
            Mock.Get(_paymentsRepository).Setup(x => x.Get(paymentId)).Returns(null as Payment);
            var paymentFound = await _handler.Handle(new Application.Domain.Model.Queries.GetPaymentQuery() { Id = paymentId }, CancellationToken.None);

            Assert.Null(paymentFound);
        }

    }
}
