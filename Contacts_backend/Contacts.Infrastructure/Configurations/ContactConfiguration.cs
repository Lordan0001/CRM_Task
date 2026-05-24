using Contacts.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contacts.Infrastructure.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.MobilePhone)
                .IsRequired()
                .HasMaxLength(20);
            builder.HasIndex(x => x.MobilePhone)
                .IsUnique();

            builder.Property(x => x.JobTitle)
                .HasMaxLength(100);

            builder.Property(x => x.BirthDate)
                .IsRequired();
        }
    }
}
