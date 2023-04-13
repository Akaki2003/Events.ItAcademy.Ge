using Events.ItAcademy.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Events.ItAcademy.Persistence.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasMany(x => x.ArchivedEvents).WithOne(x => x.User);
            builder.HasMany(user => user.Events).WithOne(@event => @event.User);
            builder.Property(user => user.Email).IsUnicode(false).IsRequired().HasMaxLength(50);
            builder.Property(user => user.PasswordHash).IsRequired();
            builder.Property(user => user.CreatedAt).IsRequired().HasColumnType("datetime");
            builder.Property(user => user.ModifiedAt).IsRequired().HasColumnType("datetime");
        }
    }
}
