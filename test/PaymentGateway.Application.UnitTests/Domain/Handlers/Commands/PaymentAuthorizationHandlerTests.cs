using Moq;

using PaymentGateway.Application.Domain.Handlers.Commands;
using PaymentGateway.Application.Domain.Model.Commands;
using PaymentGateway.Application.Domain.Model.Entities;
using PaymentGateway.Application.Ports;

namespace PaymentGateway.Application.UnitTests.Domain.Handlers.Commands
{
    public class PaymentAuthorizationHandlerTests
    {
        readonly IPaymentsRepository _paymentsRepository;
        readonly IAcquirerBankService _acquirerBankService;
        readonly PaymentAuthorizationHandler _handler;
        public PaymentAuthorizationHandlerTests()
        {
             _paymentsRepository = Mock.Of<IPaymentsRepository>();
             _acquirerBankService = Mock.Of<IAcquirerBankService>();
            _handler = new PaymentAuthorizationHandler(_paymentsRepository, _acquirerBankService);
        }

        [Fact]
        public async Task GivenPayment_WhenValid_ThenItShouldAuthoriseThePayment()
        {
            //Given
            var authorisePaymentCommand = new AuthorisePaymentCommand()
            {
                Amount = 1,
                CardNumber = "1234567898765432",
                Currency = "GBP",
                Cvv = "123",
                ExpiryMonth = 1,
                ExpiryYear = 2025,
                Id = Guid.NewGuid()
            };
            Mock.Get(_acquirerBankService).Setup(x => x.AuthoriseAsync(It.IsAny<AcquirerPaymentRequest>(), CancellationToken.None)).ReturnsAsync(new AcquirerBankResponse()
            {
                AuthorizationCode = Guid.NewGuid().ToString(),
                Authorized = true
            });
                
             var payment=   await _handler.Handle(authorisePaymentCommand, CancellationToken.None);

            Assert.Equal(PaymentStatus.Authorized, payment.Status);           
            
        }

        [Fact]
        public async Task GivenPayment_WheniNValid_ThenItShouldnOTAuthoriseThePayment()
        {
            //Given
            var authorisePaymentCommand = new AuthorisePaymentCommand()
            {
                Amount = 1,
                CardNumber = "124",
                Currency = "GBP",
                Cvv = "123",
                ExpiryMonth = 1,
                ExpiryYear = 2025,
                Id = Guid.NewGuid()
            };



            var payment = await _handler.Handle(authorisePaymentCommand, CancellationToken.None);

            Mock.Get(_acquirerBankService).Verify(x => x.AuthoriseAsync(It.IsAny<AcquirerPaymentRequest>(), CancellationToken.None), Times.Never);
            Assert.Equal(PaymentStatus.Rejected, payment.Status);

        }
    }
}
