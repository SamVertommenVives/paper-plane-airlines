using System;
using System.Collections.Generic;

namespace PPA.Domains.Entities;

public partial class UserDiscount
{
    public int Id { get; set; }

    public int User { get; set; }

    public int Discount { get; set; }

    public bool Used { get; set; }

    public virtual ICollection<FlightBooking> FlightBookings { get; set; } = new List<FlightBooking>();
}
