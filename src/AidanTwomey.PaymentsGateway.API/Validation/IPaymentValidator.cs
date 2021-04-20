using System.Threading.Tasks;
using AidanTwomey.PaymentGateway.API.Model;
using AidanTwomey.PaymentsGateway.API.Payments;

namespace AidanTwomey.PaymentsGateway.API.Validation
{
    public abstract class ValidationResponse
    {

    }
    internal static class ValidationResponseExtensions
    {
        internal static Payment ToPayment(this ValidationResponse response)
        {
            return new Payment();
        }
    }
    public class ValidPayment : ValidationResponse
    {

    }
    public class InvalidPayment : ValidationResponse
    {
        
    }
    public interface IPaymentValidator
    {
         ValidationResponse Validate(MakePaymentRequest request);
    }
}