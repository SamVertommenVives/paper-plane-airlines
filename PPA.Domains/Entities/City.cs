using System;
using System.Collections.Generic;

namespace PPA.Domains.Entities;

public partial class City
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Country { get; set; } = null!;

    public int? Airport { get; set; }

    public virtual Airport? AirportNavigation { get; set; }

    public virtual ICollection<CityDiscount> CityDiscounts { get; set; } = new List<CityDiscount>();

    public virtual ICollection<Flight> FlightFromCityNavigations { get; set; } = new List<Flight>();

    public virtual ICollection<Flight> FlightToCityNavigations { get; set; } = new List<Flight>();

    public virtual ICollection<Meal> Meals { get; set; } = new List<Meal>();
}
