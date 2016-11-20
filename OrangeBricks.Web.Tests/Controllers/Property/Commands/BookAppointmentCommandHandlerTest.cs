using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Property.Commands;
using OrangeBricks.Web.Models;
using System;
using System.Data.Entity;

namespace OrangeBricks.Web.Tests.Controllers.Property.Commands
{
    [TestFixture]
    public class BookAppointmentCommandHandlerTest
    {
        private BookAppointmentCommandHandler _handler;
        private IOrangeBricksContext _context;
        private IDbSet<Models.Property> _properties;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
            _properties = Substitute.For<IDbSet<Models.Property>>();
            _context.Properties.Returns(_properties);
            _handler = new BookAppointmentCommandHandler(_context);
        }

        [Test]
        public void HandleShouldAddAppointment()
        {
            // Arrange
            var command = new BookAppointmentCommand
            {
                PropertyId = 1,
                BuyerUserId = "111",
                AppointmentDateTime = DateTime.Now,                
            };

            var property = new Models.Property
            {
                Description = "Test Property",
                IsListedForSale = true,
                Id = 1,

            };

            _properties.Find(1).Returns(property);


            // Act
            _handler.Handle(command);

            // Assert
            _context.Properties.Received(1).Find(1);
            _context.Received(1).SaveChanges();
            Assert.AreNotEqual(property.Appointments, null);

        }
    }
}
