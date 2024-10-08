namespace PaymentGateway.Application.Exceptions
{
    internal class PaymentValidationException:Exception
    {
        public PaymentValidationException(string validationError)
        {
            ValidationError = validationError;
        }

        public string ValidationError { get; }
    }
}
