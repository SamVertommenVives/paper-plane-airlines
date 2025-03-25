using System;
using System.Collections.Generic;

namespace PPA.Domains.Entities;

public partial class CityDiscount
{
    public int Id { get; set; }

    public int Discount { get; set; }

    public int City { get; set; }

    public virtual City CityNavigation { get; set; } = null!;

    public virtual Discount DiscountNavigation { get; set; } = null!;

    public virtual ICollection<FlightBooking> FlightBookings { get; set; } = new List<FlightBooking>();
}
