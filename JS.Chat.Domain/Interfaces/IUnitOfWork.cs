using System;

namespace JS.Chat.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMessageRepository Messages { get; }       
        int Complete();
    }
}
