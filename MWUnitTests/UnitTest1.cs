using DAL.Functions;
using Entities;
using MattioliWoodTest.Controllers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace MWUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [SetUp]
        public void Setup()
        {
           
        }

        [TestMethod]
        public void CheckForAlreadyExistingStaffReturnsTrue()
        {
            HomeController mockedHomeController = new HomeController();
            var result = mockedHomeController.CheckInputDoesNotAlreadyExist("jackie", "bicknell", "Staff");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void CheckForAlreadyExistingClientReturnsTrue()
        {
            HomeController mockedHomeController = new HomeController();
            var result = mockedHomeController.CheckInputDoesNotAlreadyExist("Leanne", "bob", "Client");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void CheckForNotExistingClientReturnsFalse()
        {
            HomeController mockedHomeController = new HomeController();
            var result = mockedHomeController.CheckInputDoesNotAlreadyExist("Jim", "bob", "Client");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void CheckForNotExistingStaffReturnsFalse()
        {
            HomeController mockedHomeController = new HomeController();
            var result = mockedHomeController.CheckInputDoesNotAlreadyExist("Jim", "bob", "Staff");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void TestInsertingAnStaffRecord()
        {
            // Arrange 
            var commandMock = new Mock<IDbCommand>();
            commandMock.Setup(m => m.ExecuteNonQuery()).Verifiable();

            var connectionMock = new Mock<IDbConnection>();
            connectionMock.Setup(m => m.CreateCommand()).Returns(commandMock.Object);

            var connectionFactoryMock = new Mock<IDbConnectionFactory>();
            connectionFactoryMock.Setup(m => m.CreateConnection(connectionMock.Object.ToString()));

            var mockUserFunction = new UserFunctions();

            Staff newStaff = new Staff();
            newStaff.Forename = "Charlotte";
            newStaff.Surname = "Harrison";
            newStaff.DateOfBirth = DateAndTime.DateValue("11/12/1997");

            // Act
            var result = mockUserFunction.AddStaffRecordToDB(newStaff);

            //Assert
            //commandMock.Setup(m => m.ExecuteNonQuery()).Equals(1);

            NUnit.Framework.Assert.IsTrue(result == "Staff has been successfully saved in database");
        }
    }
}
