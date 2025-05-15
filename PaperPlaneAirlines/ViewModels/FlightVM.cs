namespace PaperPlaneAirlines.ViewModels;

public class FlightVM
{
    public int Id { get; set; }
    public string? Plane { get; set; }

    public int FlightRoute { get; set; }
    public DateTime Departure { get; set; }
    public DateTime Arrival { get; set; }
    public required CityVM FromCity { get; set; }
    public required CityVM ToCity { get; set; }

    public List<string> SeatNumber { get; set; } = [];
}