using PaperPlaneAirlines.Models;

namespace PaperPlaneAirlines.ViewModels;

public class FlightSelectionVM
{
    public FlightType FlightType { get; set; }

    public BookingVM? Booking { get; set; }
    public List<BookingOptionVM>? BookingOptions { get; set; }
    
    public int SelectedBookingOptionId { get; set; }
}