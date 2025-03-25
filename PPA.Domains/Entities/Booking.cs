using System;
using System.Collections.Generic;

namespace PPA.Domains.Entities;

public partial class Booking
{
    public int Id { get; set; }

    public int? User { get; set; }

    public int? Cancelation { get; set; }

    public int FlightBooking { get; set; }

    public virtual Cancelation? CancelationNavigation { get; set; }

    public virtual FlightBooking FlightBookingNavigation { get; set; } = null!;

    public virtual ICollection<FlightBooking> FlightBookings { get; set; } = new List<FlightBooking>();
}
