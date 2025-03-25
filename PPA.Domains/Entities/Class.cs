using System;
using System.Collections.Generic;

namespace PPA.Domains.Entities;

public partial class Class
{
    public int Id { get; set; }

    public string SeatClass { get; set; } = null!;

    public decimal BasePrice { get; set; }

    public virtual ICollection<FlightBooking> FlightBookings { get; set; } = new List<FlightBooking>();
}
