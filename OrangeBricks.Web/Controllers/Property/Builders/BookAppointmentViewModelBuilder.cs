using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.Property.Builders
{
    public class BookAppointmentViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public BookAppointmentViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public BookAppointmentViewModel Build(int id)
        {
            var property = _context.Properties.Find(id);

            return new BookAppointmentViewModel
            {
                PropertyId = property.Id,
                PropertyType = property.PropertyType,
                StreetName = property.StreetName,                
                AppointmentDateTime = DateTime.Now  // Default today
            };
        }
    }
}