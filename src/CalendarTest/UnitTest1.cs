using NUnit.Framework;
using System.IO;
using System;
using Calendar.Controllers;
using Calendar.Models;
using Calendar.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace CalendarTest
{
    public class UnitTest1
    {
        private readonly ApplicationDbContext _context;
        public UnitTest1()
        {
            _context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                           .UseInMemoryDatabase(databaseName: "EventDataBase")
                           .Options);
        }


        [SetUp]
        public void Setup()
        {
            {
                _context.Team.Add(new Team
                {
                    ID = 1,
                    Name = "HA2",
                    Description = "HA2",
                    CalendarStyle = "1",
                    DomainGroup = "CORP"
                });

                _context.Team.Add(new Team
                {
                    ID = 2,
                    Name = "HA3",
                    Description = "HA3",
                    CalendarStyle = "2",
                    DomainGroup = "CORP"
                });
                _context.SaveChanges();
            }
            
        }

        [Test]
        public async Task TestMethod1()
        {
            {
                // Arrange
                TeamsController controller = new TeamsController(_context);

                // Act
                var result = await controller.Details(1) as ViewResult;

                // Assert
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Model); // add additional checks on the Model
                //Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");

                Assert.AreEqual("HA2", ((Team)result.Model).Name);
                Assert.AreEqual("HA2", ((Team)result.Model).Description);
                Assert.AreEqual("CORP", ((Team)result.Model).DomainGroup);
            }
            
        }
    }
}