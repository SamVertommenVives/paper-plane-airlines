using System;
using System.Collections.Generic;

namespace PPA.Domains.Entities;

public partial class FlightBooking
{
    public int Id { get; set; }

    public int Booking { get; set; }

    public int Flight { get; set; }

    public int Meal { get; set; }

    public string SeatNumber { get; set; } = null!;

    public int Class { get; set; }

    public int? UserDiscount { get; set; }

    public int? FlightDiscount { get; set; }

    public virtual Booking BookingNavigation { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Class ClassNavigation { get; set; } = null!;

    public virtual CityDiscount? FlightDiscountNavigation { get; set; }

    public virtual Flight FlightNavigation { get; set; } = null!;

    public virtual Meal MealNavigation { get; set; } = null!;

    public virtual UserDiscount? UserDiscountNavigation { get; set; }
}
