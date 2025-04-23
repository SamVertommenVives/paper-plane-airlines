namespace PaperPlaneAirlines.ViewModels;

public class FlightReductionVM
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public double? Percentage { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}