using PPA.Domains.Entities;

namespace PaperPlaneAirlines.ViewModels;

public class FlightSearchVM : BookingVM
{
    public List<CityVM>? SelectionOptionsCity { get; set; }
    public List<TravelClassVM>? SelectionOptionsTravelClass { get; set; }
    public List<BookingOptionVM>? BookingOptions { get; set; }
    public int SelectedTravelClassId { get; set; }
    public int SelectedFromCityId { get; set; }
    public int SelectedToCityId { get; set; }
}