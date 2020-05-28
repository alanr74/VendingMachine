using Microsoft.AspNetCore.Mvc.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingApp.Models;


namespace VendingApp
{

    public interface IVendingMachineService
    {
        bool AcceptCoin(Coin InsertedCoin);
        bool SelectProduct(int ProductNumber);
        bool MakeChange();
        Dictionary<Coin, int> ReturnCoins();
        string CheckDisplay();
        bool SetupCashbox(Dictionary<Coin, int> coins);
        bool SetupStock(Dictionary<Product, int> stock);
    }

    public class VendingMachineService : IVendingMachineService
    {
        private readonly IMessageService messageService;


        // Temp var used instead of SQL to hold initial starting coins
        private CoinStack cashBox = new CoinStack();
        private StockStack stockItems = new StockStack();
        private CoinStack currentCoins = new CoinStack();
        private CoinStack coinReturn = new CoinStack();

        public VendingMachineService(IMessageService messageService )
        {
            this.messageService = messageService;
        }

        public bool AcceptCoin(Coin InsertedCoin)
        {
            // Return Invalid Coin
            if (!InsertedCoin.IsValid)
            {
                coinReturn.AddCoinsToStack(InsertedCoin);
            }

            currentCoins.AddCoinsToStack(InsertedCoin);
            return true;
        }

        public bool SelectProduct(int position)
        {
            var stockedItem = stockItems.GetItemInformation(position);

            if (stockedItem == null || stockedItem.FirstOrDefault().Value < 1)
            {
                messageService.ChangeVendingMessage("SOLD OUT");
                return true;
            }

            var itemPrice = stockedItem.FirstOrDefault().Key.Price;

            if (CanProductBePurchased(itemPrice))
            {
                cashBox.AddCoinsToStack(currentCoins.RemoveAllCoins());
                messageService.ChangeVendingMessage("THANK YOU");
                return true;
            }
            // Item in stock and we have the money.

            return true;
        }

        private bool CanProductBePurchased(decimal itemPrice)
        {
            // Exact Change was used
            if (currentCoins.StackValue() == itemPrice)
            {
                return true;
            }

            // Not enough Money in
            if (currentCoins.StackValue() < itemPrice)
            {
                if (messageService.CurrentMessage().Contains("PRICE:"))
                {
                    messageService.ChangeVendingMessage($"PRICE: {itemPrice - currentCoins.StackValue()}");
                }
                else
                {
                    messageService.ChangeVendingMessage($"PRICE: {itemPrice}");
                }
                return false;
            }

            // More money in that required. Check change can be given
            if (!CheckChangeCanBeGiven(itemPrice))
            {
                messageService.ChangeVendingMessage("EXACT CHANGE ONLY");
                return false;
            }

            return true;
        }

        private bool CheckChangeCanBeGiven(decimal itemPrice)
        {
            var combinedResources = new CoinStack();

            combinedResources.AddCoinsToStack(currentCoins.GetAllCoins());
            combinedResources.AddCoinsToStack(cashBox.GetAllCoins());

            var ordered = combinedResources.GetAllCoins().OrderBy(x => x.Key.CoinWorth);
            foreach(var coinType in ordered)
            {
                var coinsAvailable = coinType.Value;
                while (itemPrice >= coinType.Key.CoinWorth && coinsAvailable > 0)
                {
                    itemPrice -= coinType.Key.CoinWorth;
                    coinsAvailable -= 1;
                }
            }

            return itemPrice == 0 ? true : false;
        }

        public bool MakeChange()
        {
            return true;
        }

        public Dictionary<Coin, int> ReturnCoins()
        {
            var returnedCoins = currentCoins.RemoveAllCoins();
            messageService.ResetVendingMessage();
            return returnedCoins;
        }

        public string CheckDisplay()
        {
            return messageService.CurrentMessage();
        }

        public bool SetupCashbox(Dictionary<Coin, int> coins)
        {
            cashBox.AddCoinsToStack(coins);
            return true;
        }

        public bool SetupStock(Dictionary<Product, int> stock)
        {
            stockItems.AddItemsToStack(stock);
            return true;
        }
    }
}
