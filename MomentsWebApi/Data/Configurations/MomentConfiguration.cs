using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MomentsWebApi.Models;

namespace MomentsWebApi.Data.Configurations
{
    public class MomentConfiguration : IEntityTypeConfiguration<Moment>
    {
        public void Configure(EntityTypeBuilder<Moment> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(m => m.Description)
                .HasMaxLength(200);
        }
    }
}
