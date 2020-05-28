using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendingApp
{
    public interface IMessageService
    {
        bool ChangeVendingMessage(string Message);
        string CurrentMessage();
        void ResetVendingMessage();
    }

    public class MessageService : IMessageService
    {
        private const string defaultMessage = "INSERT COIN";
        private string currentMessage = defaultMessage;
        public bool ChangeVendingMessage(string message)
        {
            currentMessage= message;
            return true;
        }

        public string CurrentMessage()
        {
            var messageToreturn = currentMessage;

            switch (messageToreturn)
                {
                case "THANK YOU":
                    currentMessage = defaultMessage;
                    break;
            }

            return messageToreturn.Trim();
        }

        public void ResetVendingMessage()
        {
            currentMessage = defaultMessage;
        }
    }
}
