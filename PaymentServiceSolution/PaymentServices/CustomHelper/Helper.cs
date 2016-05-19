using System.Linq;

namespace PaymentServices
{
    public class Helper : IHelper
    {
        public bool Mod10Check(string creditCardNumber)
        {
            if (string.IsNullOrEmpty(creditCardNumber))
            {
                return false;
            }

            int sumOfDigits = creditCardNumber.Where((x) => x >= '0' && x <= '9')
                            .Reverse()
                            .Select((x, i) => ((int)x - 48) * (i % 2 == 0 ? 1 : 2))
                            .Sum((x) => x / 10 + x % 10);


            return sumOfDigits % 10 == 0;
        }
    }
}