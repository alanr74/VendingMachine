using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using VendingApp;
using VendingApp.Models;

namespace Tests.ServiceTests
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
                { new Coin {Weight = 5, Radius = 21}, 0 }, // Nickle
                { new Coin {Weight = 2, Radius = 18}, 0 }, // Dime
                { new Coin {Weight = 6, Radius = 24}, 0 }, // Quarter
            };

            initialStock = new Dictionary<Product, int>()
            { 
            { new Product { Name = "Cola", Price = 1.00m, Position = 1 }, 12},
            { new Product { Name = "Chips", Price = 0.5m, Position = 2 }, 0},
            { new Product { Name = "Candy", Price = 0.65m, Position = 3 }, 17},
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

        [Test]
        public void Test_Display_When_Empty()
        {
            MessageService _messageService = new MessageService();
            VendingMachineService vendingMachineService = new VendingMachineService(_messageService);

            // Setup Vending Defaults
            Assert.DoesNotThrow(() => vendingMachineService.SetupCashbox(initialCashBox));
            Assert.DoesNotThrow(() => vendingMachineService.SetupStock(initialStock));

            var returnedDisplay = vendingMachineService.CheckDisplay();

            Assert.AreEqual("INSERT COIN", returnedDisplay);
        }

        [Test]
        public void Test_Product_Select_No_Money_Value_1()
        {
            MessageService _messageService = new MessageService();
            VendingMachineService vendingMachineService = new VendingMachineService(_messageService);

            // Setup Vending Defaults
            Assert.DoesNotThrow(() => vendingMachineService.SetupCashbox(initialCashBox));
            Assert.DoesNotThrow(() => vendingMachineService.SetupStock(initialStock));

            Assert.DoesNotThrow(() => vendingMachineService.SelectProduct(1));
            var returnedDisplay = vendingMachineService.CheckDisplay();

            Assert.AreEqual("PRICE: 1.00", returnedDisplay);
        }

        [Test]
        public void Test_Product_Select_Not_Enough_Money_Product_Value_1_First_Check()
        {
            MessageService _messageService = new MessageService();
            VendingMachineService vendingMachineService = new VendingMachineService(_messageService);

            // Setup Vending Defaults
            Assert.DoesNotThrow(() => vendingMachineService.SetupCashbox(initialCashBox));
            Assert.DoesNotThrow(() => vendingMachineService.SetupStock(initialStock));

            Assert.DoesNotThrow(() => vendingMachineService.AcceptCoin(new Coin { Weight = 5, Radius = 21 }));
            Assert.DoesNotThrow(() => vendingMachineService.SelectProduct(1));


            var returnedDisplay = vendingMachineService.CheckDisplay();

            Assert.AreEqual("PRICE: 1.00", returnedDisplay);
        }

        [Test]
        public void Test_Product_Select_Not_Enough_Money_Product_Value_Subsequent_Check()
        {
            MessageService _messageService = new MessageService();
            VendingMachineService vendingMachineService = new VendingMachineService(_messageService);

            // Setup Vending Defaults
            Assert.DoesNotThrow(() => vendingMachineService.SetupCashbox(initialCashBox));
            Assert.DoesNotThrow(() => vendingMachineService.SetupStock(initialStock));

            Assert.DoesNotThrow(() => vendingMachineService.AcceptCoin(new Coin { Weight = 5, Radius = 21 }));
            Assert.DoesNotThrow(() => vendingMachineService.SelectProduct(1));
            Assert.DoesNotThrow(() => vendingMachineService.SelectProduct(1));

            var returnedDisplay = vendingMachineService.CheckDisplay();

            Assert.AreEqual("PRICE: 0.50", returnedDisplay);
        }


        [Test]
        public void Test_Product_Select_Add_2_Nickles_Correct_Change()
        {
            MessageService _messageService = new MessageService();
            VendingMachineService vendingMachineService = new VendingMachineService(_messageService);

            // Setup Vending Defaults
            Assert.DoesNotThrow(() => vendingMachineService.SetupCashbox(initialCashBox));
            Assert.DoesNotThrow(() => vendingMachineService.SetupStock(initialStock));

            Assert.DoesNotThrow(() => vendingMachineService.AcceptCoin(new Coin { Weight = 5, Radius = 21 }));
            Assert.DoesNotThrow(() => vendingMachineService.AcceptCoin(new Coin { Weight = 5, Radius = 21 }));

            Assert.DoesNotThrow(() => vendingMachineService.SelectProduct(1));

            var returnedDisplay = vendingMachineService.CheckDisplay();

            Assert.AreEqual("THANK YOU", returnedDisplay);
        }

        [Test]
        public void Test_Product_Select_Add_2_Nickles_Correct_Change_Second_Check_Display()
        {
            MessageService _messageService = new MessageService();
            VendingMachineService vendingMachineService = new VendingMachineService(_messageService);

            // Setup Vending Defaults
            Assert.DoesNotThrow(() => vendingMachineService.SetupCashbox(initialCashBox));
            Assert.DoesNotThrow(() => vendingMachineService.SetupStock(initialStock));

            Assert.DoesNotThrow(() => vendingMachineService.AcceptCoin(new Coin { Weight = 5, Radius = 21 }));
            Assert.DoesNotThrow(() => vendingMachineService.AcceptCoin(new Coin { Weight = 5, Radius = 21 }));

            Assert.DoesNotThrow(() => vendingMachineService.SelectProduct(1));

            Assert.DoesNotThrow(() => vendingMachineService.CheckDisplay());

            var returnedDisplay = vendingMachineService.CheckDisplay();

            Assert.AreEqual("INSERT COIN", returnedDisplay);
        }


        [Test]
        public void Test_Product_Select_Inserted_To_Much_Product_Value_1()
        {
            MessageService _messageService = new MessageService();
            VendingMachineService vendingMachineService = new VendingMachineService(_messageService);

            // Setup Vending Defaults
            Assert.DoesNotThrow(() => vendingMachineService.SetupCashbox(initialCashBox));
            Assert.DoesNotThrow(() => vendingMachineService.SetupStock(initialStock));

            // 3 Dimes
            Assert.DoesNotThrow(() => vendingMachineService.AcceptCoin(new Coin { Weight = 5, Radius = 21 }));
            Assert.DoesNotThrow(() => vendingMachineService.AcceptCoin(new Coin { Weight = 5, Radius = 21 }));
            Assert.DoesNotThrow(() => vendingMachineService.AcceptCoin(new Coin { Weight = 5, Radius = 21 }));

            Assert.DoesNotThrow(() => vendingMachineService.SelectProduct(1));

            var returnedDisplay = vendingMachineService.CheckDisplay();
            var returnedChange = vendingMachineService.ReturnCoins();

            Assert.AreEqual("THANK YOU", returnedDisplay);
        }

        [Test]
        public void Test_Product_Select_Correct_Change_Only()
        {
            MessageService _messageService = new MessageService();
            VendingMachineService vendingMachineService = new VendingMachineService(_messageService);

            // Setup Vending Defaults
            Assert.DoesNotThrow(() => vendingMachineService.SetupCashbox(initialCashBox));
            Assert.DoesNotThrow(() => vendingMachineService.SetupStock(initialStock));

            // 3 Dimes
            Assert.DoesNotThrow(() => vendingMachineService.AcceptCoin(new Coin { Weight = 5, Radius = 21 }));
            Assert.DoesNotThrow(() => vendingMachineService.AcceptCoin(new Coin { Weight = 5, Radius = 21 }));

            // Candy
            Assert.DoesNotThrow(() => vendingMachineService.SelectProduct(3));

            var returnedDisplay = vendingMachineService.CheckDisplay();

            Assert.AreEqual("EXACT CHANGE ONLY", returnedDisplay);
        }

        [Test]
        public void Test_Product_Sold_Out()
        {
            MessageService _messageService = new MessageService();
            VendingMachineService vendingMachineService = new VendingMachineService(_messageService);

            // Setup Vending Defaults
            Assert.DoesNotThrow(() => vendingMachineService.SetupCashbox(initialCashBox));
            Assert.DoesNotThrow(() => vendingMachineService.SetupStock(initialStock));

            // 3 Dimes
            Assert.DoesNotThrow(() => vendingMachineService.AcceptCoin(new Coin { Weight = 5, Radius = 21 }));
            Assert.DoesNotThrow(() => vendingMachineService.AcceptCoin(new Coin { Weight = 5, Radius = 21 }));

            // Candy
            Assert.DoesNotThrow(() => vendingMachineService.SelectProduct(2));

            var returnedDisplay = vendingMachineService.CheckDisplay();

            Assert.AreEqual("SOLD OUT", returnedDisplay);
        }

        [Test]
        public void Test_Product_REeturned_Change_After_Purchase()
        {
            MessageService _messageService = new MessageService();
            VendingMachineService vendingMachineService = new VendingMachineService(_messageService);

            // Setup Vending Defaults
            Assert.DoesNotThrow(() => vendingMachineService.SetupCashbox(initialCashBox));
            Assert.DoesNotThrow(() => vendingMachineService.SetupStock(initialStock));

            // 3 Dimes
            Assert.DoesNotThrow(() => vendingMachineService.AcceptCoin(new Coin { Weight = 5, Radius = 21 }));
            Assert.DoesNotThrow(() => vendingMachineService.AcceptCoin(new Coin { Weight = 5, Radius = 21 }));
            Assert.DoesNotThrow(() => vendingMachineService.AcceptCoin(new Coin { Weight = 5, Radius = 21 }));

            Assert.DoesNotThrow(() => vendingMachineService.SelectProduct(1));

            var returnedDisplay = vendingMachineService.CheckDisplay();
            var returnedChange = vendingMachineService.ReturnCoins();

            Assert.AreEqual("THANK YOU", returnedDisplay);
        }

        [Test]
        public void Test_Product_Sold_Out_Check_Display_Second_Time()
        {
            MessageService _messageService = new MessageService();
            VendingMachineService vendingMachineService = new VendingMachineService(_messageService);

            // Setup Vending Defaults
            Assert.DoesNotThrow(() => vendingMachineService.SetupCashbox(initialCashBox));
            Assert.DoesNotThrow(() => vendingMachineService.SetupStock(initialStock));

            // 3 Dimes
            Assert.DoesNotThrow(() => vendingMachineService.AcceptCoin(new Coin { Weight = 5, Radius = 21 }));
            Assert.DoesNotThrow(() => vendingMachineService.AcceptCoin(new Coin { Weight = 5, Radius = 21 }));

            // Candy
            Assert.DoesNotThrow(() => vendingMachineService.SelectProduct(2));

            Assert.DoesNotThrow(() => vendingMachineService.CheckDisplay());
            var returnedDisplay = vendingMachineService.CheckDisplay();

            Assert.AreEqual("INSERT COIN", returnedDisplay);
        }


    }
}
