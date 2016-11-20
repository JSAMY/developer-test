using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.Property.ViewModels
{
    public class BookAppointmentViewModel
    {

        public string StreetName { get; set; }

        public string PropertyType { get; set; }

        public int PropertyId { get; set; }

        public string BuyerUserId { get; set; }

        [Required]
        [Display(Name = "Appointment Date & Time")]
        public DateTime  AppointmentDateTime { get; set; }

         
    }
}