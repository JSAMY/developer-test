using OrangeBricks.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class BookAppointmentCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public BookAppointmentCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(BookAppointmentCommand command)
        {
            var property = _context.Properties.Find(command.PropertyId);

            if (property != null)
            {
                var appointment = new Appointment
                {
                    Property_Id = command.PropertyId,
                    AppointmentDateTime = command.AppointmentDateTime,
                    BuyerUserId = command.BuyerUserId,
                    Status = AppointmentStatus.Accepted, // as of now it will be accepted, but it needs to be pending by default and appointment should be accepted by seller/agent in future
                    CreatedAt = DateTime.Now,
                };

                if (property.Appointments == null)
                {
                    property.Appointments = new List<Appointment>();
                }

                property.Appointments.Add(appointment);

                _context.SaveChanges();
            }
            
        }
    }
}