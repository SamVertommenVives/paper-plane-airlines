using System;
using System.Collections.Generic;

namespace PPA.Domains.Entities;

public partial class Flight
{
    public int Id { get; set; }

    public int Plane { get; set; }

    public int FlightRoute { get; set; }

    public DateTime Departure { get; set; }

    public DateTime Arrival { get; set; }

    public int FromCity { get; set; }

    public int ToCity { get; set; }

    public int SeatsBooked { get; set; }

    public virtual ICollection<FlightBooking> FlightBookings { get; set; } = new List<FlightBooking>();

    public virtual FlightRoute FlightRouteNavigation { get; set; } = null!;

    public virtual City FromCityNavigation { get; set; } = null!;

    public virtual Plane PlaneNavigation { get; set; } = null!;

    public virtual City ToCityNavigation { get; set; } = null!;
}
