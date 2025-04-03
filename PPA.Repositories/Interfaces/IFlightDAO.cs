using PPA.Domains.Entities;

namespace PPA.Repositories.Interfaces;

public interface IFlightDAO : IDAO<Flight>
{
    Task<IEnumerable<Flight>?> GetFirstTenBookableFlights();
    Task<IEnumerable<Flight>?> FindFlightsByDestinationAndDepartureDate(int fromCity, int toCity, DateTime fromDate);
    Task<IEnumerable<Flight>?> SearchFlights(FlightSearchCriteria searchCriteria);
}

public class FlightSearchCriteria
{
    public string? SelectedTravelClass { get; set; }
    public string? FromCity { get; set; }
    public string? ToCity { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public bool Roundtrip { get; set; } = false;
    public int NumberOfPassengers { get; set; } = 1;
}