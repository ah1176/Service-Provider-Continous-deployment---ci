using ServiceProvider_BLL.Interfaces;
using ServiceProvider_DAL.Data;
using ServiceProvider_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Reposatories
{
    public class MessageRepository : BaseRepository<Message> , IMessageRepository
    {
        private readonly AppDbContext _context;

        public MessageRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
