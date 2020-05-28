using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using WebApplication11;
using WebApplication11.Controllers;
using WebApplication11.Models;
using WebApplication11.Repositories;

namespace Nunit.RepositoryTests.MessageTests
{
    [TestFixture]
    public class MessageTest
    {
        [TestCase(1, 1, 2)]
        [TestCase(2, 1, 2)]
        [TestCase(3, 1, 2)]
        public void Test_Addition_With_Valid_Integers(int first, int second, int expectedResult)
        {
            //Coin newCoin = new Coin(5, 12);

            //// Arrange
            //Mock<IMessageRepository> _messageRepository = new Mock<IMessageRepository>();

            //_messageRepository.Setup(x => x.(first, second)).Returns(expectedResult);

            //MessageService _calculatorService = new MessageService(_messageRepository.Object);
            //var result = _calculatorService.Addition(first, second);
            //// Assert
            //Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Ignore("Ignore Test")]
        public void Test_To_ignore()
        {

        }
    }
}