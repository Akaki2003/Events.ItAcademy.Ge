using Events.ItAcademy.Domain.Events;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.ItAcademy.Domain.EventSetup;

namespace Events.ItAcademy.Persistence.Configurations
{
    public class EventsSetupConfiguration : IEntityTypeConfiguration<EventSetup>
    {
        public void Configure(EntityTypeBuilder<EventSetup> builder)
        {
            builder.ToTable("EventSetups");
            builder.Property(x => x.EditEventAfterUploadInDays)
                .HasAnnotation("MinValue", 1.0);
            builder.Property(x => x.ReserveTimeLengthInMinutes)
                .HasAnnotation("MinValue", 1.0);
        }
    }
}
