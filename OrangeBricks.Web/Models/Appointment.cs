using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        
        public int Property_Id { get; set; }
        
        public string BuyerUserId { get; set; }
        
        public AppointmentStatus Status { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime AppointmentDateTime { get; set; }


        public DateTime CreatedAt { get; set; }
        

        [ForeignKey("Property_Id")]
        public  Property Property { get; set; }
    }
}