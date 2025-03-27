using PPA.Domains.Entities;

namespace PPA.Repositories.Interfaces;

public interface IFlightDAO : IDAO<Flight>
{
    Task<IEnumerable<Flight>?> FindFlightsByDestinationAndDepartureDate(City fromCity, City toCity, DateTime fromDate);
}