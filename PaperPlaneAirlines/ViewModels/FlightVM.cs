using PPA.Domains.Entities;

namespace PaperPlaneAirlines.ViewModels;

public class FlightVM
{
    public string Plane { get; set; }

    public int FlightRoute { get; set; }

    public DateTime Departure { get; set; }

    public DateTime Arrival { get; set; }

    public string FromCity { get; set; }

    public string ToCity { get; set; }
    
    public double FlightPrice { get; set; }
}