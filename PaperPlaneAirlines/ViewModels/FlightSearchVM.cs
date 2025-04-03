using System.Runtime.InteropServices.JavaScript;
using PaperPlaneAirlines.Models;
using PPA.Domains.Entities;
using PPA.Repositories.Interfaces;

namespace PaperPlaneAirlines.ViewModels;

public class FlightSearchVM
{
    public ClassVM? SelectedTravelClass { get; set; }
    public string? FromCity { get; set; }
    public string? ToCity { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public bool? Roundtrip { get; set; } = false;
    public int? NumberOfPassengers { get; set; } = 1;
    public List<CityVM>? Cities { get; set; }
    
    public List<ClassVM>? TravelClasses { get; set; }
    public List<BookingOptionVM>? BookingOptions { get; set; }
    
    public FlightSearchCriteria? FlightSearchCriteria { get; set; }
}

public class CityVM
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Country { get; set; }
    public string? AirportName { get; set; }
}

public class ClassVM
{
    public string? SeatClass { get; set; }
    public double? BasePrice { get; set; }
}