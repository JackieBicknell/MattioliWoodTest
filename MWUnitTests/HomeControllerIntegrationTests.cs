using DAL.Functions;
using Entities;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Text;

namespace MWUnitTests
{
    [TestClass]
    public class HomeControllerIntegrationTests
    {

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
