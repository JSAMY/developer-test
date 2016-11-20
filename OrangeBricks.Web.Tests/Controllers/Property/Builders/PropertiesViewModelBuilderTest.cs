using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Property.Builders;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Tests.Controllers.Property.Builders
{
    public static class ExtentionMethods
    {
        public static IDbSet<T> Initialize<T>(this IDbSet<T> dbSet, IQueryable<T> data) where T : class
        {
            dbSet.Provider.Returns(data.Provider);
            dbSet.Expression.Returns(data.Expression);
            dbSet.ElementType.Returns(data.ElementType);
            dbSet.GetEnumerator().Returns(data.GetEnumerator());
            return dbSet;
        }     
    }

    [TestFixture]
    public class PropertiesViewModelBuilderTest
    {
        private IOrangeBricksContext _context;
        private readonly string _userId = "Buyer1";

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
        }

        [Test]
        public void BuildShouldReturnPropertiesWithMatchingStreetNamesWhenSearchTermIsProvided()
        {
            // Arrange
            var builder = new PropertiesViewModelBuilder(_context);

            var properties = new List<Models.Property>{
                new Models.Property{ StreetName = "Smith Street", Description = "", IsListedForSale = true },
                new Models.Property{ StreetName = "Jones Street", Description = "", IsListedForSale = true}
            };

            var mockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(mockSet);

            var query = new PropertiesQuery
            {
                Search = "Smith Street"
            };

            // Act
            var viewModel = builder.Build(query);

            // Assert
            Assert.That(viewModel.Properties.Count, Is.EqualTo(1));
        }

        [Test]
        public void BuildShouldReturnPropertiesWithMatchingDescriptionsWhenSearchTermIsProvided()
        {
            // Arrange
            var builder = new PropertiesViewModelBuilder(_context);

            var properties = new List<Models.Property>{
                new Models.Property{ StreetName = "", Description = "Great location", IsListedForSale = true },
                new Models.Property{ StreetName = "", Description = "Town house", IsListedForSale = true }
            };

            var mockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(mockSet);

            var query = new PropertiesQuery
            {
                Search = "Town"
            };

            // Act
            var viewModel = builder.Build(query);

            // Assert
            Assert.That(viewModel.Properties.Count, Is.EqualTo(1));
            Assert.That(viewModel.Properties.All(p => p.Description.Contains("Town")));
        }

        [Test]
        public void BuildShouldReturnPropertiesWithOfferStatus()
        {
            // Arrange
            var builder = new PropertiesViewModelBuilder(_context,_userId);

            var properties = new List<Models.Property>{
                new Models.Property{ Id = 1, StreetName = "", Description = "Great location", IsListedForSale = true,Offers = new List<Offer>() },
                new Models.Property{ Id = 2, StreetName = "", Description = "Town house", IsListedForSale = true , Offers = new List<Offer> ()    }
            };
            properties.First().Offers = new List<Offer>() { new Offer { Id = 1, Amount = 1000, BuyerUserId = _userId, Status = OfferStatus.Accepted } };

            var mockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(mockSet);

            var query = new PropertiesQuery
            {
                Search = ""
            };

            // Act
            var viewModel = builder.Build(query);

            // Assert
            Assert.That(viewModel.Properties.Count, Is.EqualTo(2));
            Assert.AreEqual(viewModel.Properties.First().OfferStatus, "Accepted");
        }
    }
}
