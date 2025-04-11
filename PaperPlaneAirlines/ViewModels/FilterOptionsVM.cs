namespace PaperPlaneAirlines.ViewModels;

public class FilterOptionsVM
{
    public bool RoundTrip { get; set; } = false;
    public CityVM? FromCity { get; set; }
    public CityVM? ToCity { get; set; }
    public DateOnly FromDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly? ToDate { get; set; }
}

public class CityVM
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Country { get; set; }
    public int? AirportName { get; set; }
}