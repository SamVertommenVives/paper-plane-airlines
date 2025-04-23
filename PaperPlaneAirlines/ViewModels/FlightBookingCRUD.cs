namespace PaperPlaneAirlines.ViewModels;

public class FlightBookingCRUD
{
    public int Booking { get; set; }

    public int Flight { get; set; }

    public int? Meal { get; set; }

    public string SeatNumber { get; set; } = null!;

    public int Class { get; set; }

    public int? FlightDiscount { get; set; }
    
}