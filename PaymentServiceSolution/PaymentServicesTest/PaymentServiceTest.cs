using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PaymentServices.Test
{
    [TestClass]
    public class PaymentServiceTest
    {
        public PaymentService _paymentService;
        public PaymentServiceTest()
        {
            _paymentService = new PaymentService(new Helper());
        }

        [TestMethod]
        public void WhatsYourId_should_return_proper_id()
        {
            Assert.AreEqual("2a8dec5a-5f70-45a2-9e0b-b14064850de0", _paymentService.WhatsYourId());
        }

        [TestMethod]
        public void IsCardNumberValid_should_return_false_if_cardNumber_contains_letter()
        {
            Assert.AreEqual(false, _paymentService.IsCardNumberValid("234234b234234"));
        }

        [TestMethod]
        public void IsCardNumberValid_should_return_false_if_cardNumber_is_null()
        {
            Assert.AreEqual(false, _paymentService.IsCardNumberValid(null));
        }

        [TestMethod]
        public void IsCardNumberValid_should_return_true_if_cardNumber_is_valid()
        {
            Assert.AreEqual(true, _paymentService.IsCardNumberValid("4012888888881881"));
        }

        [TestMethod]
        public void IsValidPaymentAmount_should_return_false_if_amount_more_than_99999999Cents()
        {
            Assert.AreEqual(false, _paymentService.IsValidPaymentAmount(10000001));
        }

        [TestMethod]
        public void IsValidPaymentAmount_should_return_true_if_amount_less_than_99999999Cents()
        {
            Assert.AreEqual(true, _paymentService.IsValidPaymentAmount(999999));
        }

        [TestMethod]
        public void CanMakePaymentWithCard_should_return_false_if_cardNumber_is_notvalid()
        {
            Assert.AreEqual(false, _paymentService.CanMakePaymentWithCard("4012888888881880", 11, 2017));
        }

        [TestMethod]
        public void CanMakePaymentWithCard_should_return_false_if_cardNumber_is_notvalid_with_lessthan16()
        {
            Assert.AreEqual(false, _paymentService.CanMakePaymentWithCard("401288888888188", 11, 2017));
        }

        [TestMethod]
        public void CanMakePaymentWithCard_should_return_false_if_cardNumber_is_notvalid_with_expirymonth_greaterthan12()
        {
            Assert.AreEqual(false, _paymentService.CanMakePaymentWithCard("4012888888881881", 13, 2017));
        }

        [TestMethod]
        public void CanMakePaymentWithCard_should_return_false_if_cardNumber_is_notvalid_with_expiryyear_has5digits()
        {
            Assert.AreEqual(false, _paymentService.CanMakePaymentWithCard("4012888888881881", 11, 20171));
        }

        [TestMethod]
        public void CanMakePaymentWithCard_should_return_false_if_cardNumber_is_notvalid_with_expirydate()
        {
            Assert.AreEqual(false, _paymentService.CanMakePaymentWithCard("4012888888881881", 04, 2016));
        }

        [TestMethod]
        public void CanMakePaymentWithCard_should_return_true_if_cardNumber_valid()
        {
            Assert.AreEqual(true, _paymentService.CanMakePaymentWithCard("4012888888881881", 08, 2016));
        }
    }
}
