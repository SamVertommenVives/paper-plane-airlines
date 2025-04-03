using PPA.Domains.Entities;
using PPA.Repositories.Interfaces;

namespace PPA.Services.Interfaces;

public interface IFlightService : IService<Flight>
{
    Task<IEnumerable<Flight>?> GetFirstTenBookableFlights();
    Task<IEnumerable<Flight>?> FindFlightsByDestinationAndDepartureDate(int fromCity, int toCity, DateTime fromDate);
    Task<IEnumerable<Flight>?> SearchFlights(FlightSearchCriteria searchCriteria);
}