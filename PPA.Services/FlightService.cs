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

    public async Task AddAsync(Flight entity)
    {
        await _dao.AddAsync(entity);
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

    public async Task<IEnumerable<Flight>?> FindFlightsByDestinationAndDepartureDate(
        int fromCity,
        int toCity,
        DateTime fromDate)
    {
        return await _dao.FindFlightsByDestinationAndDepartureDate(fromCity, toCity, fromDate);
    }

    public async Task<IEnumerable<Flight>?> SearchFlights(FlightSearchCriteria searchCriteria)
    {
        return await _dao.SearchFlights(searchCriteria);
    }
}