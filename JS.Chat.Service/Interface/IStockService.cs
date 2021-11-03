using JS.Chat.Domain.Models;

namespace JS.Chat.Service.Interface
{
    public interface IStockService
    {
        BotModel BotDetection(string message);
    }
}
