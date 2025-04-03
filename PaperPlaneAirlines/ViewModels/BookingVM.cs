using PaperPlaneAirlines.Models;
using PPA.Repositories.Interfaces;

namespace PaperPlaneAirlines.ViewModels;

public class BookingVM
{
    public List<BookingOptionVM>? BookingOptions { get; set; }
    
    public FlightSearchCriteria? FlightSearchCriteria { get; set; }
    
    public FlightVM? OutboundFlight { get; set; }
    public FlightVM? ReturnFlight { get; set; }
}