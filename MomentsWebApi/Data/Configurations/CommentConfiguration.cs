using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MomentsWebApi.Models;

namespace MomentsWebApi.Data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Moment)
                .WithMany(m => m.Comments)
                .HasForeignKey(c => c.Moment.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(m => m.Text)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(m => m.UserName)
                   .IsRequired();
        }
    }
}
