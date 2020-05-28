using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication11.Repositories
{
    public interface IMessageRepository
    {
        bool AddVendingMessage(string Message);
    }

    public class MessageRepository : IMessageRepository
    {
        public bool AddVendingMessage(string Message)
        {
            return true;
        }
    }
}
