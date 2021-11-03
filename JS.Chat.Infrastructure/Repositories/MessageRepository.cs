using JS.Chat.DataAccess.Data;
using JS.Chat.Domain.Interfaces;
using JS.Chat.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JS.Chat.DataAccess.Repositories
{
    public class MessageRepository : GenericRepository<TMessage>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TMessage>> GetMessages()
        {
            return await _context.TMessage.OrderByDescending(m => m.DateMessage).Take(50).OrderBy(m => m.DateMessage).ToListAsync();
        }

        public void AddMessage(TMessage message)
        {
            _context.TMessage.AddAsync(message);
        }
    }
}
