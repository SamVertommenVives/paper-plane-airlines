using PPA.Domains.Entities;

namespace PPA.Services.Interfaces;

public interface IFlightRouteService
{
    public Task<IEnumerable<List<FlightRoute>?>> GetFlightRoutesAsync(int startAirportId, int targetAirportId);
}