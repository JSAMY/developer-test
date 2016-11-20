using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Models;
using OrangeBricks.Web.Helper;
using Microsoft.AspNet.Identity;

namespace OrangeBricks.Web.Controllers.Property.Builders
{
    public class PropertiesViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        private readonly string _userId;

        public PropertiesViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public PropertiesViewModelBuilder(IOrangeBricksContext context,string userId)
        {
            _context = context;
            _userId = userId;
        }

        public PropertiesViewModel Build(PropertiesQuery query)
        {
            //var properties = _context.Properties
            //    .Where(p => p.IsListedForSale);

            var properties = _context.CompletePropertiesDetails().Where(p => p.IsListedForSale);


            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                properties = properties.Where(x => x.StreetName.Contains(query.Search) 
                    || x.Description.Contains(query.Search));
            }

            return new PropertiesViewModel
            {
                Properties = properties
                    .ToList()
                    .Select(d => MapViewModel(d,_userId))
                    .ToList(),
                Search = query.Search
            };
        }

        private static PropertyViewModel MapViewModel(Models.Property property, string userId)
        {

            var model = new PropertyViewModel
            {
                Id = property.Id,
                StreetName = property.StreetName,
                Description = property.Description,
                NumberOfBedrooms = property.NumberOfBedrooms,
                PropertyType = property.PropertyType,               
            };

            if (property.Offers != null && property.Offers.Any())
            {
                var offerObj = property.Offers.LastOrDefault(d => d.BuyerUserId == userId);
                if (offerObj != null )
                {
                    model.OfferStatus = offerObj.Status.ToString();
                    model.OfferStatusDateTime = offerObj.UpdatedAt;
                }
            }

            return model;
        }
    }
}