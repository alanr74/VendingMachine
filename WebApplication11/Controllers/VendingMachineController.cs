using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication11.Models;

namespace WebApplication11.Controllers
{
    public class VendingMachineController : Controller
    {
        private readonly IVendingMachineService vendingMachineService;

        public VendingMachineController(IVendingMachineService vendingMachineService)
        {
            this.vendingMachineService = vendingMachineService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public bool AcceptCoin(Coin InsertedCoin)
        {
            return vendingMachineService.AcceptCoin(InsertedCoin);
        }

        public bool SelectProduct(int ProductNumber)
        {
            return vendingMachineService.SelectProduct(ProductNumber);
        }

        public bool MakeChange()
        {
            return vendingMachineService.MakeChange();
        }

        public Dictionary<Coin, int> ReturnCoins()
        {
            return vendingMachineService.ReturnCoins();
        }

        public string GetVendingDisplay()
        {
            return vendingMachineService.CheckDisplay();
        }
    }
}