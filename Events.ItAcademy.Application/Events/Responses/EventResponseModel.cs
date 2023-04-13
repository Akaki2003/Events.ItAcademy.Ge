using Events.ItAcademy.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Application.Events.Responses
{
    public class EventResponseModel 
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int TicketCount { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public int EditDuration { get; set; }
        public int BookTimeInHours { get; set; }

        //public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PhotoPath { get; set; }
        //public DateTime ModifiedAt { get; set; }
        //public bool IsActive { get; set; }
        //public string UserId { get; set; }
        //public string Title { get; set; }
        //public string Description { get; set; }
        //public double Price { get; set; }
        //public int TicketCount { get; set; }
        //public int EditDuration { get; set; }
        //public int BookTimeInHours { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
    }
}
