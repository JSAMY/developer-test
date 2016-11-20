using OrangeBricks.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OrangeBricks.Web.Helper
{
    public static class Extensions
    {
        public static IQueryable<Property> CompletePropertiesDetails(this IOrangeBricksContext _context)
        {
            return _context.Properties
                .Include("Offers")
                .Include("Appointments");
        }

        public static IQueryable<Property> CompletePropertyDetails(this IOrangeBricksContext _context, int id)
        {
            return _context.Properties.Where(data => data.Id == id)
                .Include("Offers")
                .Include("Appointments");
        }
    }
}