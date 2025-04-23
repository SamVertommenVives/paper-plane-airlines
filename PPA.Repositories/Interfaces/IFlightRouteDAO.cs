using PPA.Domains.Entities;

namespace PPA.Repositories.Interfaces;

public interface IFlightRouteDAO
{
    public Task<IEnumerable<FlightRoute>?> GetFlightRoutesAsync();
}