using System.Threading.Tasks;
using AidanTwomey.PaymentGateway.API.Model;

namespace AidanTwomey.PaymentsGateway.API.Command
{
    public interface ICardStorageCommand
    {
         Task StoreCardAsync(Card card);
    }
}