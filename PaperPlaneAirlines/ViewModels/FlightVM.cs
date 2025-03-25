namespace PaperPlaneAirlines.ViewModels;

public class FlightVM
{
    public int Plane { get; set; }

    public int FlightRoute { get; set; }

    public DateTime Departure { get; set; }

    public DateTime Arrival { get; set; }

    public int FromCity { get; set; }

    public int ToCity { get; set; }
}