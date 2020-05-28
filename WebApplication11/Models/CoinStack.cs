using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace WebApplication11.Models
{
    public class CoinStack
    {
        private Dictionary<Coin, int> CoinsInStack = new Dictionary<Coin, int>();

        public bool AddCoinsToStack(Dictionary<Coin, int> Coins)
        {
            foreach (var coin in Coins)
            {
                AddCoinsToStack(coin.Key, coin.Value);
            }
            return true;
        }

        public bool AddCoinsToStack(Coin Coin)
        {
            if (CoinsInStack.TryGetValue(Coin, out int numberInStack))
            {
                numberInStack += 1;
                CoinsInStack[Coin] = numberInStack;
                return true;
            }

            CoinsInStack[Coin] = 1;
            return true;
        }

        public bool AddCoinsToStack(Coin Coin, int quatity)
        {
            if (CoinsInStack.TryGetValue(Coin, out int numberInStack))
            {
                numberInStack += quatity;
                CoinsInStack[Coin] = numberInStack;
                return true;
            }

            CoinsInStack[Coin] = quatity;
            return true;
        }

        public bool RemoveCoinsFromStack(Dictionary<Coin, int> Coins)
        {
            return true;
        }

        public bool RemoveCoinsFromStack(Coin Coin)
        {
            if (CoinsInStack.TryGetValue(Coin, out int numberInStack))
            {
                numberInStack -= 1;
                if (numberInStack >= 0)
                {
                    CoinsInStack[Coin] = numberInStack;
                    return true;
                }
            }

            return false;
        }

        public Dictionary<Coin, int> RemoveAllCoins()
        {
            Dictionary<Coin, int> returnedCoins = new Dictionary<Coin, int>(CoinsInStack);
            CoinsInStack = new Dictionary<Coin, int>();
            return returnedCoins;
        }

        public decimal StackValue()
        {
            decimal total = default;
            foreach (var coin in CoinsInStack)
            {
                total += (coin.Key.CoinWorth * coin.Value);
            }
            return total;
        }
    }
}
