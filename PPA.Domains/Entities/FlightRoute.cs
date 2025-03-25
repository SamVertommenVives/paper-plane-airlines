using System;
using System.Collections.Generic;

namespace PPA.Domains.Entities;

public partial class FlightRoute
{
    public int Id { get; set; }

    public int Airport_1 { get; set; }

    public int Airport_2 { get; set; }

    public int Distance { get; set; }

    public int FlightTime { get; set; }

    public virtual Airport Airport_1Navigation { get; set; } = null!;

    public virtual Airport Airport_2Navigation { get; set; } = null!;

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
