using System;
using System.Collections.Generic;

namespace PPA.Domains.Entities;

public partial class Plane
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int EconomySeats { get; set; }

    public int BusinessSeats { get; set; }

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
