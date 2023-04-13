using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Domain.EventSetup
{
    public class EventSetup
    {
        public int Id { get; set; }
        public int ReserveTimeLengthInMinutes { get; set; }
        public int EditEventAfterUploadInDays { get; set; }
    }
}
