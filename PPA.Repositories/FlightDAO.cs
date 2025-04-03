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

    public async Task<IEnumerable<Flight>?> GetFirstTenBookableFlights()
    {
        try
        {
            return await _dbContext.Flights.Where(
                    f => f.Departure > DateTime.Now.AddDays(3) && f.Departure < DateTime.Now.AddMonths(6))
                .Include(f => f.FlightRouteNavigation)
                .Include(f => f.PlaneNavigation)
                .Include(f => f.FromCityNavigation)
                .Include(f => f.ToCityNavigation)
                .OrderBy(f => f.Departure)
                .Take(10)
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<Flight>?> FindFlightsByDestinationAndDepartureDate(
        int fromCity,
        int toCity,
        DateTime fromDate
    )
    {
        try
        {
            return await _dbContext.Flights.Where(f =>
                    f.Departure >= fromDate && f.Departure <= fromDate.AddDays(1) && f.FromCity == fromCity &&
                    f.ToCity == toCity)
                .Include(f => f.FlightRouteNavigation)
                .Include(f => f.PlaneNavigation)
                .OrderBy(f => f.Departure)
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<Flight>?> SearchFlights(FlightSearchCriteria searchCriteria)
    {
        var query = _dbContext.Flights.AsQueryable();

        Console.WriteLine(searchCriteria.ToCity);

        if (!string.IsNullOrEmpty(searchCriteria.FromCity))
            query = query.Where(f => f.FromCityNavigation.Name == searchCriteria.FromCity);

        if (!string.IsNullOrEmpty(searchCriteria.ToCity)) 
            query = query.Where(f => f.ToCityNavigation.Name == searchCriteria.ToCity);
        
        
        query = query.Where(f => f.Departure >= searchCriteria.FromDate && f.Departure <= searchCriteria.FromDate.AddDays(1));
        
        //query = query.Where(f => f.PlaneNavigation.EconomySeats - f.SeatsBooked >= searchCriteria.NumberOfPassengers);


        return await query
            .Include(f => f.FlightRouteNavigation)
            .Include(f => f.PlaneNavigation)
            .Include(f => f.FromCityNavigation)
            .Include(f => f.ToCityNavigation)
            .OrderBy(f => f.Departure)
            .ToListAsync();
    }
}