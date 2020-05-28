using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication11.Repositories;

namespace WebApplication11
{
    public interface IMessageService
    {
        bool ChangeVendingMessage(string Message);

        string CurrentMessage();
    }

    public class MessageService : IMessageService
    {
        private string message = "INSERT COIN";
        public bool ChangeVendingMessage(string Message)
        {
            return true;
        }

        public string CurrentMessage()
        {
            var messageToreturn = message;

            switch (messageToreturn)
                {
                case "THANK YOU":
                    message = "INSERT COIN";
                    break;
            }

            return messageToreturn;
        }
    }
}
