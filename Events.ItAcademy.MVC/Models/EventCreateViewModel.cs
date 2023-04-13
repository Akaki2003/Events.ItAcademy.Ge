using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Events.ItAcademy.MVC.Models
{
    public class EventCreateViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Price { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int TicketCount { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
