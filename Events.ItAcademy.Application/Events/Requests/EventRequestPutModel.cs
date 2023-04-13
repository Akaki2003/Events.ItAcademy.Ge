using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Application.Events.Requests
{
    public class EventRequestPutModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(1,double.MaxValue)]
        public double Price { get; set; }
        [Required]
        [Range(1,int.MaxValue)]
        public int TicketCount { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public string PhotoPath { get; set; }

    }
}
