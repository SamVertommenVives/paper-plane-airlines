using PPA.Domains.Entities;
using PPA.Repositories.Interfaces;
using PPA.Services.Interfaces;

namespace PPA.Services;

public class FlightRouteService : IFlightRouteService
{
    private readonly IFlightRouteDAO _flightRouteDao;


    public FlightRouteService(IFlightRouteDAO flightRouteDao)
    {
        _flightRouteDao = flightRouteDao;
    }

    public async Task<IEnumerable<List<FlightRoute>?>> GetFlightRoutesAsync(int startAirportId,
        int targetAirportId)
    {
        var visited = new HashSet<int>();
        var routes = await _flightRouteDao.GetFlightRoutesAsync();
        var routesList = new List<FlightRoute>(routes);
        
        var routeOptions = FindRoutesRecursive(routesList, startAirportId, targetAirportId, visited);

        return routeOptions;
    }
    
    private IEnumerable<List<FlightRoute>> FindRoutesRecursive(
        List<FlightRoute> routesInDb,
        int currentAirportId,
        int targetAirportId,
        HashSet<int> visited)
    {
        if (visited.Contains(currentAirportId))
            yield break;

        visited.Add(currentAirportId);

        foreach (var route in routesInDb.Where(r => r.Airport_1 == currentAirportId))
        {
            if (route.Airport_2 == targetAirportId)
            {
                // Found direct route
                yield return new List<FlightRoute> { route };
            }
            else
            {
                // Recursive search for further connections
                foreach (var subPath in FindRoutesRecursive(
                             routesInDb,
                             route.Airport_2,
                             targetAirportId,
                             new HashSet<int>(visited) // clone visited for branch isolation
                         ))
                {
                    subPath.Insert(0, route); // Prepend current leg to the path
                    yield return subPath;
                }
            }
        }
    }
}