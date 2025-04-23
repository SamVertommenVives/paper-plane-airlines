using Microsoft.CodeAnalysis.CSharp;
using PaperPlaneAirlines.Models;
using PPA.Domains.Entities;

namespace PaperPlaneAirlines.ViewModels;

public class BookingOptionVM
{
    public int BookingOptionId { get; set; }
    public FlightType? FlightType { get; set; }
    public List<FlightVM> Flights { get; set; }
    public List<MenuVM>? LocalMenuOptions { get; set; }
    public MenuVM? Menu { get; set; }
    public FlightReductionVM? Reduction { get; set; }
    public double? Price { get; set; }
}