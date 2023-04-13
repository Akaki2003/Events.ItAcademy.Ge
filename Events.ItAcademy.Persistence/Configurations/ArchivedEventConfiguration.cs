using Events.ItAcademy.Domain.Events;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.ItAcademy.Persistence.Configurations
{
    internal class ArchivedEventConfiguration : IEntityTypeConfiguration<ArchivedEvent>
    {
        public void Configure(EntityTypeBuilder<ArchivedEvent> builder)
        {
            builder.ToTable("ArchivedEvents");
        }
    }
}
