namespace PaperPlaneAirlines.ViewModels;

public class Booking
{
    public FilterOptionsVM? FilterOptions { get; set; }
    public BookingOptionVM? OutboundFlightOption { get; set; }
    public BookingOptionVM? ReturnFlightOption { get; set; }
    public double? TotalPrice { get; set; }
}