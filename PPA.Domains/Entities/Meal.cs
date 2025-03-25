using System;
using System.Collections.Generic;

namespace PPA.Domains.Entities;

public partial class Meal
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? Type { get; set; }

    public int? LocalMealFor { get; set; }

    public virtual ICollection<FlightBooking> FlightBookings { get; set; } = new List<FlightBooking>();

    public virtual City? LocalMealForNavigation { get; set; }
}
