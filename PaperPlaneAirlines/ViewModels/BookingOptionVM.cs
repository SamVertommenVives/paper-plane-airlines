using PPA.Domains.Entities;

namespace PaperPlaneAirlines.ViewModels;

public class BookingOptionVM
{
    public List<Flight>? Flights { get; set; }
    public MenuVM? Menu { get; set; }
    public ReductionVM? Reduction { get; set; }
    public double? Price { get; set; }
}

public class MenuVM
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public class ReductionVM
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public double? Percentage { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    
}