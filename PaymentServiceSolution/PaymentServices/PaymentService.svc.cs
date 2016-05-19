using Common;
using System;
using System.ServiceModel;
using System.Globalization;

namespace PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private IHelper _helper;

        public PaymentService(IHelper helper)
        {
            _helper = helper;
        }

        public string WhatsYourId()
        {
            Guid candidateId = new Guid("2a8dec5a-5f70-45a2-9e0b-b14064850de0");

            return candidateId.ToString();
        }

        public bool IsCardNumberValid(string cardNumber)
        {
            long outCardNumber;

            try
            {
                if (!(long.TryParse(cardNumber, out outCardNumber)))
                    return false;

                return _helper.Mod10Check(cardNumber);
            }
            catch (Exception ex)
            {
                throw new FaultException<UnexpectedServiceFault>(
                                    new UnexpectedServiceFault
                                    {
                                        ErrorMessage = ex.Message,
                                        Source = ex.Source,
                                        StackTrace = ex.StackTrace,
                                        Target = ex.TargetSite.ToString()
                                    },
                                    new FaultReason(string.Format(CultureInfo.InvariantCulture,
                                    "{0}", "Service fault exception")));
            }
        }

        public bool IsValidPaymentAmount(long amount)
        {
            long amountInCents = amount * 100;

            try
            {
                return (amountInCents >= 99 && amountInCents <= 99999999);
            }
            catch (Exception ex)
            {
                throw new FaultException<UnexpectedServiceFault>(
                                    new UnexpectedServiceFault
                                    {
                                        ErrorMessage = ex.Message,
                                        Source = ex.Source,
                                        StackTrace = ex.StackTrace,
                                        Target = ex.TargetSite.ToString()
                                    },
                                    new FaultReason(string.Format(CultureInfo.InvariantCulture,
                                    "{0}", "Service fault exception")));
            }
        }

        public bool CanMakePaymentWithCard(string cardNumber, int expiryMonth, int expiryYear)
        {
            long outCardNumber;

            try
            {
                if (!(long.TryParse(cardNumber, out outCardNumber)) || cardNumber.Length < 16 || !_helper.Mod10Check(cardNumber))
                {
                    return false;
                }

                if (expiryMonth > 12 || expiryYear.ToString().Length > 4)
                {
                    return false;
                }

                var lastDateOfExpiryMonth = DateTime.DaysInMonth(expiryYear, expiryMonth);
                var cardExpiry = new DateTime(expiryYear, expiryMonth, lastDateOfExpiryMonth, 23, 59, 59);

                if (cardExpiry < DateTime.Now)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new FaultException<UnexpectedServiceFault>(
                                    new UnexpectedServiceFault
                                    {
                                        ErrorMessage = ex.Message,
                                        Source = ex.Source,
                                        StackTrace = ex.StackTrace,
                                        Target = ex.TargetSite.ToString()
                                    },
                                    new FaultReason(string.Format(CultureInfo.InvariantCulture,
                                    "{0}", "Service fault exception")));
            }
        }
    }
}
