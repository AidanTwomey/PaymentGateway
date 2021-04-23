using System.Threading.Tasks;
using AidanTwomey.PaymentGateway.API.Model;

namespace AidanTwomey.PaymentsGateway.API.Validation
{
    public interface IPaymentValidator
    {
         ValidationResponse Validate(MakePaymentRequest request);
    }
}