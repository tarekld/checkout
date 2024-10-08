using System.Text.RegularExpressions;

using PaymentGateway.Application.Exceptions;

namespace PaymentGateway.Application.Domain.Model.Entities
{
    public class Amount
    {
        private string _currency;
        private bool _isValid = true;

        public Amount(int value, string currency)
        {
            Value = value;
            Currency = currency;
        }
        public int Value { get; private set; }
        public string Currency
        {
            get { return _currency; }
            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length != 3)
                {
                    _isValid = false;
                }
                
                //check against valid international currecy codes                
                _currency = value;
            }
        }

        internal bool IsValid()
        {
            return _isValid;
        }
    }
}
