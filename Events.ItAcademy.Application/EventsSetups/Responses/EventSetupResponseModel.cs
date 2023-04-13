using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Application.EventsSetups.Responses
{
    public class EventSetupResponseModel
    {
        [Required]
        [Range(1, int.MaxValue,ErrorMessage ="Min value must be 1")]
        public int ReserveTimeLengthInMinutes { get; set; }


        [Required]
        [Range(1, int.MaxValue,ErrorMessage ="Min value must be 1")]
        public int EditEventAfterUploadInDays { get; set; }
    }
}
