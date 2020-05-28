using Microsoft.AspNetCore.Mvc.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication11.Models;
using WebApplication11.Repositories;

namespace WebApplication11
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
        private readonly IVendingMachineRepository vendingMachineRepository;

        // Temp var used instead of SQL to hold initial starting coins
        private CoinStack cashBox = new CoinStack();
        private StockStack stockItems = new StockStack();
        private CoinStack currentCoins = new CoinStack();
        private CoinStack coinReturn = new CoinStack();

        public VendingMachineService(IMessageService messageService, IVendingMachineRepository vendingMachineRepository)
        {
            this.messageService = messageService;
            this.vendingMachineRepository = vendingMachineRepository;
        }

        public bool AcceptCoin(Coin InsertedCoin)
        {
            // Return Invalid Coin
            if(!InsertedCoin.IsValid)
            {
                coinReturn.AddCoinsToStack(InsertedCoin);
            }

            currentCoins.AddCoinsToStack(InsertedCoin);
            return true;
        }

        public bool SelectProduct(int position)
        {
            var stockedItem = stockItems.GetItemInformation(position);

            if(stockedItem == null || stockedItem.FirstOrDefault().Value < 1)
            {
                // No Item in stock (not in spec)
            }

            if(currentCoins.StackValue() >= stockedItem.FirstOrDefault().Key.Price)
            {
                
            }
            return true;
        }

        public bool MakeChange()
        {
            return true;
        }

        public Dictionary<Coin, int> ReturnCoins()
        {
            var returnedCoins = currentCoins.RemoveAllCoins();
            return returnedCoins;
        }

        public string CheckDisplay()
        {
            return "";
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
