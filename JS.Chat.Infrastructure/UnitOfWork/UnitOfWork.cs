using JS.Chat.DataAccess.Data;
using JS.Chat.DataAccess.Repositories;
using JS.Chat.Domain.Interfaces;

namespace JS.Chat.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Messages = new MessageRepository(_context);            
        }
        public IMessageRepository Messages { get; private set; }      
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
