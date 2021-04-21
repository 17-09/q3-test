using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.ToTable("RX_RoomType");

            builder.HasKey(j => j.Id);

            builder.Property(j => j.Name)
                .HasMaxLength(28);

            builder.Property(j => j.Description)
                .HasMaxLength(255);
        }
    }
}