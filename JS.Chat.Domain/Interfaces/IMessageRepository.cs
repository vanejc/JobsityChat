using JS.Chat.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JS.Chat.Domain.Interfaces
{
    public interface IMessageRepository
    {
        Task<IEnumerable<TMessage>> GetMessages();

        void AddMessage(TMessage message);
    }
}
