using Microsoft.EntityFrameworkCore;
using PPA.Domains.Data;
using PPA.Domains.Entities;
using PPA.Repositories.Interfaces;

namespace PPA.Repositories;

public class FlightDAO : IFlightDAO
{
    private readonly PPADbContext _dbContext;

    public FlightDAO(PPADbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Flight>?> GetAllAsync()
    {
        try
        {
            return await _dbContext.Flights
                .Include(f => f.FlightRouteNavigation)
                .Include(f => f.PlaneNavigation)
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task AddAsync(Flight entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Flight entity)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Flight entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Flight?> FindByIdAsync(int id)
    {
        try
        {
            return await _dbContext.Flights.Where(f => f.Id == id)
                .Include(f => f.FlightRouteNavigation)
                .Include(f => f.PlaneNavigation)
                .FirstOrDefaultAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<Flight>?> FindFlightsByDestinationAndDepartureDate(
        City fromCity,
        City toCity,
        DateTime fromDate
    )
    {
        try
        {
            return await _dbContext.Flights.Where(f => f.Departure == fromDate && f.FromCity.Equals(fromCity) && f.ToCity.Equals(toCity))
                .Include(f => f.FlightRouteNavigation)
                .Include(f => f.PlaneNavigation)
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}