using System.ServiceModel;

namespace PaymentServices
{
    [ServiceContract]
    public interface IPaymentService
    {
        [OperationContract]
        string WhatsYourId();

        [OperationContract]
        bool IsCardNumberValid(string cardNumber);

        [OperationContract]
        bool IsValidPaymentAmount(long amount);

        [OperationContract]
        bool CanMakePaymentWithCard(string cardNumber, int expiryMonth, int expiryYear);
    }
}
