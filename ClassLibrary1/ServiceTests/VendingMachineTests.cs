using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication11;
using WebApplication11.Controllers;
using WebApplication11.Models;

namespace ClassLibrary1.ServiceTests
{

    [TestFixture]
    class VendingMachineTests
    {
        Dictionary<Coin, int> initialCashBox;
        Dictionary<Product, int> initialStock;

        [SetUp]
        public void TestInit()
        {
            initialCashBox = new Dictionary<Coin, int>()
            {
                { new Coin {Weight = 5, Radius = 21}, 10 }, 
                { new Coin {Weight = 2, Radius = 18}, 12 }, 
                { new Coin {Weight = 6, Radius = 24}, 13 }, 
            };

            initialStock = new Dictionary<Product, int>()
            { 
            { new Product { Name = "Cola", Price = 1.00m }, 12},
            { new Product { Name = "Chips", Price = 0.5m }, 15},
            { new Product { Name = "Candy", Price = 0.65m }, 17},
            };
        }

        [Test]
        public void Test_Vending_Machine_Cash_Setup()
        {
            Mock<IMessageService> _messageService = new Mock<IMessageService>();
            VendingMachineService vendingMachineService = new VendingMachineService(_messageService.Object);


            Assert.DoesNotThrow(() => vendingMachineService.SetupCashbox(initialCashBox));
        }

        [Test]
        public void Test_Vending_Machine_Stock_Setup()
        {
            Mock<IMessageService> _messageService = new Mock<IMessageService>();
            VendingMachineService vendingMachineService = new VendingMachineService(_messageService.Object);


            Assert.DoesNotThrow(() => vendingMachineService.SetupStock(initialStock));
        }

        [Test]
        public void Test_Inserting_Nickle()
        {
            Mock<IMessageService> _messageService = new Mock<IMessageService>();
            VendingMachineService vendingMachineService = new VendingMachineService(_messageService.Object);

            // Setup Vending Defaults
            Assert.DoesNotThrow(() => vendingMachineService.SetupCashbox(initialCashBox));
            Assert.DoesNotThrow(() => vendingMachineService.SetupStock(initialStock));

            Assert.DoesNotThrow(() => vendingMachineService.AcceptCoin(new Coin { Weight = 5, Radius = 21 }));
        }

        [TestCase(5, 21)]
        [TestCase(2, 18)]
        [TestCase(6, 24)]
        public void Test_Insert_And_return_Of_Coins(int first, int second)
        {
            Mock<IMessageService> _messageService = new Mock<IMessageService>();
            VendingMachineService vendingMachineService = new VendingMachineService(_messageService.Object);

            // Setup Vending Defaults
            Assert.DoesNotThrow(() => vendingMachineService.SetupCashbox(initialCashBox));
            Assert.DoesNotThrow(() => vendingMachineService.SetupStock(initialStock));

            Assert.DoesNotThrow(() => vendingMachineService.AcceptCoin(new Coin { Weight = first, Radius = second }));

            var returnedStack = vendingMachineService.ReturnCoins();

            Assert.AreEqual((new Coin { Weight = first, Radius = second }), returnedStack.FirstOrDefault().Key);
        }
    }
}
