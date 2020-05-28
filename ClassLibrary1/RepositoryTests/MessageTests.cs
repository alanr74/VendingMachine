using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using WebApplication11;
using WebApplication11.Controllers;
using WebApplication11.Models;


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
        }

        [Ignore("Ignore Test")]
        public void Test_To_ignore()
        {

        }
    }
}