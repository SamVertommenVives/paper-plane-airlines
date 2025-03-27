using PPA.Domains.Entities;
using PPA.Repositories.Interfaces;
using PPA.Services.Interfaces;

namespace PPA.Services;

public class FlightService : IService<Flight>
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

    public async Task<IEnumerable<Flight>?> FindFlightsByDestinationAndDepartureDate(
        City fromCity,
        City toCity,
        DateTime fromDate)
    {
        return await _dao.FindFlightsByDestinationAndDepartureDate(fromCity, toCity, fromDate);
    }
}