namespace PaperPlaneAirlines.ViewModels;

public class BookingCRUD 
{
    public new List<FlightBookingCRUD> Flights { get; set; }
    public string User { get; set; } = null!;

    public int? Cancelation { get; set; }

    public int? UserDiscount { get; set; }
}