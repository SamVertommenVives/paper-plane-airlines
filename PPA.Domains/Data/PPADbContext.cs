using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PPA.Domains.Entities;

namespace PPA.Domains.Data;

public partial class PPADbContext : DbContext
{
    public PPADbContext()
    {
    }

    public PPADbContext(DbContextOptions<PPADbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Airport> Airports { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Cancelation> Cancelations { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<CityDiscount> CityDiscounts { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<FlightBooking> FlightBookings { get; set; }

    public virtual DbSet<FlightRoute> FlightRoutes { get; set; }

    public virtual DbSet<Meal> Meals { get; set; }

    public virtual DbSet<Plane> Planes { get; set; }

    public virtual DbSet<Refund> Refunds { get; set; }

    public virtual DbSet<UserDiscount> UserDiscounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:paperplaneairlines-server.database.windows.net,1433;Initial Catalog=Paper-plane-airlines;Persist Security Info=False;User ID=ppa-admin;Password=.aG87a@tC7hLrK#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Airport>(entity =>
        {
            entity.ToTable("Airport");

            entity.Property(e => e.AirportName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IATA).HasMaxLength(3);
            entity.Property(e => e.Lat).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Lon).HasColumnType("decimal(9, 6)");

            entity.HasOne(d => d.CityNavigation).WithMany(p => p.Airports)
                .HasForeignKey(d => d.City)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Airport_City");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_booking");

            entity.ToTable("Booking");

            entity.HasOne(d => d.CancelationNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Cancelation)
                .HasConstraintName("FK_Cancelation");

            entity.HasOne(d => d.FlightBookingNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.FlightBooking)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Booking_FlightBooking");
        });

        modelBuilder.Entity<Cancelation>(entity =>
        {
            entity.ToTable("Cancelation");

            entity.Property(e => e.CanceledAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime");
            entity.Property(e => e.Reason).HasColumnType("text");

            entity.HasOne(d => d.RefundNavigation).WithMany(p => p.Cancelations)
                .HasForeignKey(d => d.Refund)
                .HasConstraintName("FK_Cancelation_Refund");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("City");

            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CityDiscount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_RouteDiscount");

            entity.ToTable("CityDiscount");

            entity.HasOne(d => d.CityNavigation).WithMany(p => p.CityDiscounts)
                .HasForeignKey(d => d.City)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CityDiscount_City");

            entity.HasOne(d => d.DiscountNavigation).WithMany(p => p.CityDiscounts)
                .HasForeignKey(d => d.Discount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RouteDiscount_Discount");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FlightPricing");

            entity.ToTable("Class");

            entity.Property(e => e.BasePrice).HasColumnType("decimal(9, 2)");
            entity.Property(e => e.SeatClass)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.ToTable("Discount");

            entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(9, 2)");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.ToTable("Flight");

            entity.Property(e => e.Arrival).HasColumnType("datetime");
            entity.Property(e => e.Departure).HasColumnType("datetime");

            entity.HasOne(d => d.FlightRouteNavigation).WithMany(p => p.Flights)
                .HasForeignKey(d => d.FlightRoute)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Flight_FlightRoute");

            entity.HasOne(d => d.FromCityNavigation).WithMany(p => p.FlightFromCityNavigations)
                .HasForeignKey(d => d.FromCity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Flight_FromCity");

            entity.HasOne(d => d.PlaneNavigation).WithMany(p => p.Flights)
                .HasForeignKey(d => d.Plane)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Flight_Plane");

            entity.HasOne(d => d.ToCityNavigation).WithMany(p => p.FlightToCityNavigations)
                .HasForeignKey(d => d.ToCity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Flight_ToCity");
        });

        modelBuilder.Entity<FlightBooking>(entity =>
        {
            entity.ToTable("FlightBooking");

            entity.Property(e => e.SeatNumber).HasMaxLength(50);

            entity.HasOne(d => d.BookingNavigation).WithMany(p => p.FlightBookings)
                .HasForeignKey(d => d.Booking)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FlightBooking_Booking");

            entity.HasOne(d => d.ClassNavigation).WithMany(p => p.FlightBookings)
                .HasForeignKey(d => d.Class)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FlightBooking_Price");

            entity.HasOne(d => d.FlightNavigation).WithMany(p => p.FlightBookings)
                .HasForeignKey(d => d.Flight)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FlightBooking_Flight");

            entity.HasOne(d => d.FlightDiscountNavigation).WithMany(p => p.FlightBookings)
                .HasForeignKey(d => d.FlightDiscount)
                .HasConstraintName("FK_FlightBooking_CityDiscount");

            entity.HasOne(d => d.MealNavigation).WithMany(p => p.FlightBookings)
                .HasForeignKey(d => d.Meal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FlightBooking_Meal");

            entity.HasOne(d => d.UserDiscountNavigation).WithMany(p => p.FlightBookings)
                .HasForeignKey(d => d.UserDiscount)
                .HasConstraintName("FK_FlightBooking_UserDiscount");
        });

        modelBuilder.Entity<FlightRoute>(entity =>
        {
            entity.ToTable("FlightRoute", tb => tb.HasTrigger("trg_CalculateDistance"));

            entity.HasOne(d => d.Airport_1Navigation).WithMany(p => p.FlightRouteAirport_1Navigations)
                .HasForeignKey(d => d.Airport_1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Route_FromAirport");

            entity.HasOne(d => d.Airport_2Navigation).WithMany(p => p.FlightRouteAirport_2Navigations)
                .HasForeignKey(d => d.Airport_2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Route_ToAirport");
        });

        modelBuilder.Entity<Meal>(entity =>
        {
            entity.ToTable("Meal");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.LocalMealForNavigation).WithMany(p => p.Meals)
                .HasForeignKey(d => d.LocalMealFor)
                .HasConstraintName("FK_Meal_City");
        });

        modelBuilder.Entity<Plane>(entity =>
        {
            entity.ToTable("Plane");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Refund>(entity =>
        {
            entity.ToTable("Refund");

            entity.Property(e => e.RefundAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.RefundedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<UserDiscount>(entity =>
        {
            entity.ToTable("UserDiscount");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
