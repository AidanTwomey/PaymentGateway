using System.Threading.Tasks;

namespace AidanTwomey.PaymentsGateway.API.Command
{
    public interface ICardStorageCommand
    {
         Task StoreCardAsync();
    }

    public class CardStorageCommand : ICardStorageCommand
    {
        public Task StoreCardAsync()
        {
            return Task.CompletedTask;
        }
    }
}