using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Property.Builders;
using OrangeBricks.Web.Models;
namespace OrangeBricks.Web.Tests.Controllers.Property.Builders
{
           
    [TestFixture]
    public class BookAppointmentViewModelBuilderTest
    {
        private IOrangeBricksContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
        }

        [Test]
        public void BuildShouldReturnCorrecthStreetNameAndTypeOfTheProperty()
        {
            // Arrange
            var builder = new BookAppointmentViewModelBuilder(_context);

            var properties = new List<Models.Property>{
                new Models.Property{ Id = 1, StreetName = "Elmwood Court", Description = "test1", IsListedForSale = true , PropertyType = "House" },
                new Models.Property{ Id = 2 , StreetName = "Sandy Lane", Description = "test2", IsListedForSale = true, PropertyType = "House" }
            };

            var mockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(mockSet);
            _context.Properties.Find(1).Returns(properties.First(d => d.Id == 1));
            // Act
            var viewModel = builder.Build(1);

            // Assert
            Assert.AreEqual(viewModel.StreetName, properties.First().StreetName);
        }

       
    }
}
