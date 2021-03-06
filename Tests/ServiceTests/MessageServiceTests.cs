﻿using Microsoft.Extensions.DependencyInjection;
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
    class MessageMachineTests
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
        public void Test_Display_When_Empty()
        {
            MessageService _messageService = new MessageService();

            var returnedDisplay = _messageService.CurrentMessage();

            Assert.AreEqual("INSERT COIN", returnedDisplay);
        }
    }
}
