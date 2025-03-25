using System;
using System.Collections.Generic;

namespace PPA.Domains.Entities;

public partial class Airport
{
    public int Id { get; set; }

    public string AirportName { get; set; } = null!;

    public string IATA { get; set; } = null!;

    public int City { get; set; }

    public decimal Lat { get; set; }

    public decimal Lon { get; set; }

    public virtual City CityNavigation { get; set; } = null!;

    public virtual ICollection<FlightRoute> FlightRouteAirport_1Navigations { get; set; } = new List<FlightRoute>();

    public virtual ICollection<FlightRoute> FlightRouteAirport_2Navigations { get; set; } = new List<FlightRoute>();
}
