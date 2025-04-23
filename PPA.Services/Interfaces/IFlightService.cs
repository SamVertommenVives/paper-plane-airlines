using PPA.Domains.Entities;

namespace PPA.Services.Interfaces;

public interface IFlightService : IService<Flight>
{
    Task<IEnumerable<Flight>?> GetFirstTenBookableFlights();
    Task<IEnumerable<Flight>?> SearchFlights(int fromCityId, int? toCityId, DateTime fromDate);

    Task<Flight?> GetNextFlightForRoute(int routeId, DateTime minDepartureDate, int numberOfPassengers);
}