using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class BookAppointmentCommand
    {
        public int PropertyId { get; set; }

        public string BuyerUserId { get; set; }

        public DateTime AppointmentDateTime { get; set; }

    }
}