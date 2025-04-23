namespace PaperPlaneAirlines.ViewModels;

public class TravelClassVM
{
    public int Id { get; set; }
    public string SeatClass { get; set; } = null!;

    public decimal BasePrice { get; set; }
}