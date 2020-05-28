using Microsoft.AspNetCore.Mvc.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication11.Models;

namespace WebApplication11.Repositories
{

    public interface  IVendingMachineRepository
    {
        bool AcceptCoin(Coin InsertedCoin);
        bool SelectProduct(int ProductNumber);
        bool MakeChange();
        List<Coin> ReturnCoins();
        string GetVendingDisplay();
    }

    public class VendingMachineRepository : IVendingMachineRepository
    {
        public bool AcceptCoin(Coin InsertedCoin)
        {
            return true;
        }

        public bool SelectProduct(int ProductNumber)
        {
            return true;
        }

        public bool MakeChange()
        {
            return true;
        }

        public List<Coin> ReturnCoins()
        {
            return new List<Coin>();
        }

        public string GetVendingDisplay()
        {
            return "";
        }
    }
}

