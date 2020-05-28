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
        public bool ChangeVendingMessage(string Message)
        {
            return true;
        }

        public string CurrentMessage()
        {
            var messageToreturn = currentMessage;

            switch (messageToreturn)
                {
                case defaultMessage:
                    currentMessage = defaultMessage;
                    break;
            }

            return messageToreturn;
        }

        public void ResetVendingMessage()
        {
            currentMessage = defaultMessage;
        }
    }
}
