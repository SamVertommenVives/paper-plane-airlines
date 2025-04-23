using PPA.Domains.Entities;
using PPA.Repositories.Interfaces;
using PPA.Services.Interfaces;

namespace PPA.Services;

public class FlightService : IFlightService
{
    private readonly IFlightDAO _dao;

    public FlightService(IFlightDAO dao)
    {
        _dao = dao;
    }


    public async Task<IEnumerable<Flight>?> GetAllAsync()
    {
        return await _dao.GetAllAsync();
    }

    public async Task<int> AddAsync(Flight entity)
    {
        await _dao.AddAsync(entity);
        return entity.Id;
    }

    public async Task DeleteAsync(Flight entity)
    {
        await _dao.DeleteAsync(entity);
    }

    public async Task UpdateAsync(Flight entity)
    {
        await _dao.UpdateAsync(entity);
    }

    public async Task<Flight?> FindByIdAsync(int Id)
    {
        return await _dao.FindByIdAsync(Id);
    }

    public async Task<IEnumerable<Flight>?> GetFirstTenBookableFlights()
    {
        return await _dao.GetFirstTenBookableFlights();
    }

    public async Task<IEnumerable<Flight>?> SearchFlights(int fromCityId, int? toCityId, DateTime fromDate)
    {
        return await _dao.SearchFlights(fromCityId, toCityId, fromDate);
    }

    public async Task<Flight?> GetNextFlightForRoute(int routeId, DateTime minDepartureDate, int numberOfPassengers)
    {
        return await _dao.GetNextFlightForRoute(routeId, minDepartureDate, numberOfPassengers);
    }
}