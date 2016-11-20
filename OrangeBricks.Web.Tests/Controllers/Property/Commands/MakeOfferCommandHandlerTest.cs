using System.Data.Entity;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Property.Commands;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Tests.Controllers.Property.Commands
{
    [TestFixture]
    public class MakeOfferCommandHandlerTest
    {
        private MakeOfferCommandHandler _handler;
        private IOrangeBricksContext _context;
        private IDbSet<Models.Property> _properties;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
            _properties = Substitute.For<IDbSet<Models.Property>>();
            _context.Properties.Returns(_properties);            
            _handler = new MakeOfferCommandHandler(_context);
        }


        [Test]
        public void HandleShouldAddOffer()
        {
            // Arrange
            var command = new MakeOfferCommand
            {
                PropertyId = 1,
                BuyerUserId = "111",
                Offer = 1000,
                                
            };

            var property = new Models.Property
            {
                Description = "Test Property",
                IsListedForSale = true,
                Id=1,
                
            };

            _properties.Find(1).Returns(property);


            // Act
            _handler.Handle(command);

            // Assert
            _context.Properties.Received(1).Find(1);
            _context.Received(1).SaveChanges();
            Assert.AreNotEqual(property.Offers, null);

        }
    }
}
