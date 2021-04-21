using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.ToTable("RX_Job");

            builder.HasKey(j => j.Id);

            builder.HasOne(j => j.RoomType)
                .WithMany(rt => rt.Jobs)
                .HasForeignKey(j => j.RoomTypeId)
                .IsRequired(false);

            builder.Property(j => j.Name)
                .HasMaxLength(50);

            builder.Property(j => j.Status)
                .HasMaxLength(50);

            builder.Property(j => j.DelayReason)
                .HasMaxLength(50);
        }
    }
}