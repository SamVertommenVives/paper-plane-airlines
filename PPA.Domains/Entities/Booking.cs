using System;
using System.Collections.Generic;

namespace PPA.Domains.Entities;

public partial class Booking
{
    public int Id { get; set; }

    public string User { get; set; } = null!;

    public int? Cancelation { get; set; }

    public int? UserDiscount { get; set; }

    public virtual Cancelation? CancelationNavigation { get; set; }

    public virtual ICollection<FlightBooking> FlightBookings { get; set; } = new List<FlightBooking>();

    public virtual UserDiscount? UserDiscountNavigation { get; set; }

    public virtual AspNetUser UserNavigation { get; set; } = null!;
}
