using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using WebApplication11;
using WebApplication11.Controllers;
using WebApplication11.Models;
using WebApplication11.Repositories;

namespace Nunit.Model.Cointest
{
    [TestFixture]
    public class CoinTests
    {
        [TestCase(5, 21, "Nickel")]
        [TestCase(6, 24, "Quarter")]
        [TestCase(2, 18, "Dime")]
        [TestCase(3, 1, "Invalid")]
        public void Test_Coin_Correct_Types(int weight, int radius, string expectedResult)
        {
            Coin newCoin = new Coin(weight, radius);
            Assert.That(newCoin.CoinName, Is.EqualTo(expectedResult));
        }

        [Ignore("Ignore Test")]
        public void Test_To_ignore()
        {

        }
    }
}