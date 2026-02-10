using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourManagement.Domain.Entities;

namespace TourManagement.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for Booking entity
/// </summary>
public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Booking");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).HasColumnName("BookingId");

        builder.Property(b => b.UserId)
            .IsRequired()
            .HasColumnName("UserId");

        builder.Property(b => b.TourId)
            .IsRequired()
            .HasColumnName("TourId");

        builder.Property(b => b.BookingDate)
            .IsRequired()
            .HasColumnName("BookingDate");

        builder.Property(b => b.NumberOfPeople)
            .IsRequired()
            .HasColumnName("NumberOfPeople");

        builder.Property(b => b.TotalPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasColumnName("TotalPrice");

        builder.Property(b => b.Status)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("Status")
            .HasDefaultValue("Pending");

        builder.Property(b => b.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(b => b.ModifiedDate);

        builder.Property(b => b.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(b => b.CreatedBy)
            .IsRequired()
            .HasMaxLength(100)
            .HasDefaultValue("System");

        builder.Property(b => b.ModifiedBy)
            .HasMaxLength(100);

        builder.HasOne(b => b.User)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Tour)
            .WithMany(t => t.Bookings)
            .HasForeignKey(b => b.TourId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
