using System.ComponentModel.DataAnnotations;
namespace PaperPlaneAirlines.ViewModels;

public class BookingVM
{
    public DateTime FromDate { get; set; } = DateTime.Now;
    public DateTime? ToDate { get; set; }
    public CityVM? FromCity { get; set; }
    public CityVM? ToCity { get; set; }
    public BookingOptionVM? OutboundFlight { get; set; }
    public BookingOptionVM? ReturnFlight { get; set; }
    public TravelClassVM? TravelClass { get; set; }
    public bool RoundTrip { get; set; } = false;
    
    public int NumberOfPassengers { get; set; } = 1;
    
    public UserDiscountVM? Discount { get; set; }
    
    public double TotalPrice { get; set; } = 0.0;
}