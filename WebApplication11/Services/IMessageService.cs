using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication11.Repositories;

namespace WebApplication11
{
    public interface IMessageService
    {
        bool AddVendingMessage(string Message);
    }

    public class MessageService : IMessageService
    {
        private readonly IMessageRepository messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        public bool AddVendingMessage(string Message)
        {
            return true;
        }
    }
}
