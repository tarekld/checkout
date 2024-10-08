using System.Text.RegularExpressions;

using PaymentGateway.Application.Exceptions;

namespace PaymentGateway.Application.Domain.Model.Entities
{
    public class Card
    {
        private string _number;
        private string _cvv;
        private bool _isValid = true;
        public Card(string number, int expiryMonth, int expiryYear, string cvv)
        {
            Number = number;
            Cvv = cvv;
            ExpiryMonth = expiryMonth;
            ExpiryYear = expiryYear;
            ExpiryDate = SetExpiryDate(expiryMonth, expiryYear);
        }

        public string Number
        {
            get
            {
                return _number;
            }
            private set
            {
                Regex regex = new Regex("^[0-9]{16}$");
                if (string.IsNullOrEmpty(value) || !regex.IsMatch(value))
                {
                    _isValid = false;
                }
                _number = value;
            }
        }

        public int ExpiryYear { get; private set; }
        public int ExpiryMonth { get; private set; }
        public string ExpiryDate { get; private set; }

        public string Cvv
        {
            get { return _cvv; }
            private set
            {
                Regex regex = new Regex("^[0-9]{3}$");
                if (string.IsNullOrEmpty(value) || !regex.IsMatch(value))
                {
                    _isValid = false;
                }
                _cvv = value;
            }
        }


        public bool IsValid()
        {
            return _isValid;
        }

        private string? SetExpiryDate(int expiryMonth, int expiryYear)
        {
            var expiryDate = new DateTime(expiryYear, expiryMonth, 1);
            var now = DateTime.UtcNow;
            if (expiryDate >= new DateTime(now.Year, now.Month, 1))
            {
                return $"{expiryMonth.ToString("00")}/{expiryYear}";
            }
            _isValid = false;
            return string.Empty;
        }
    }
}
