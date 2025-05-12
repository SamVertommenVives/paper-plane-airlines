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

    public async Task<int> AddAsync(Flight entity)
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

    public async Task<IEnumerable<Flight?>?> GetFirstTenBookableFlights()
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

    public async Task<IEnumerable<Flight>?> SearchFlights(
        int fromCityId,
        int? toCityId,
        DateTime fromDate
    )
    {
        var query = _dbContext.Flights.AsQueryable();

        query = query.Where(f => f.FromCityNavigation.Id == fromCityId);

        if (toCityId == null)
            query = query.Where(f => f.ToCityNavigation.Id == toCityId);


        query = query.Where(f => f.Departure >= fromDate && f.Departure <= fromDate.AddDays(1));

        //query = query.Where(f => f.PlaneNavigation.EconomySeats - f.SeatsBooked >= searchCriteria.NumberOfPassengers);


        return await query
            .Include(f => f.FlightRouteNavigation)
            .Include(f => f.PlaneNavigation)
            .Include(f => f.FromCityNavigation)
            .Include(f => f.ToCityNavigation)
            .OrderBy(f => f.Departure)
            .ToListAsync();
    }

    public async Task<Flight?> GetNextFlightForRoute(int routeId, DateTime minDepartureDate, int numberOfPassengers)
    {
        var flights = await _dbContext.Flights
            .Where(f => f.FlightRoute == routeId && f.Departure >= minDepartureDate)
            .Include(f => f.FlightRouteNavigation)
            .Include(f => f.PlaneNavigation)
            .Include(f => f.FromCityNavigation)
            .Include(f => f.ToCityNavigation)
            .OrderBy(f => f.Departure)
            .ToListAsync();

        //
        return flights.FirstOrDefault();
    }
    
    public async Task<Flight?> GetNextEconomyFlightForRoute(int routeId, DateTime minDepartureDate, int numberOfPassengers)
    {
        var flights = await _dbContext.Flights
            .Where(f => f.FlightRoute == routeId && f.Departure >= minDepartureDate && f.PlaneNavigation.EconomySeats - f.SeatsBooked >= numberOfPassengers)
            .Include(f => f.FlightRouteNavigation)
            .Include(f => f.PlaneNavigation)
            .Include(f => f.FromCityNavigation)
            .Include(f => f.ToCityNavigation)
            .OrderBy(f => f.Departure)
            .ToListAsync();

        //
        return flights.FirstOrDefault();
    }
    
    public async Task<Flight?> GetNextBusinessFlightForRoute(int routeId, DateTime minDepartureDate, int numberOfPassengers)
    {
        var flights = await _dbContext.Flights
            .Where(f => f.FlightRoute == routeId && f.Departure >= minDepartureDate && f.PlaneNavigation.BusinessSeats - f.SeatsBooked >= numberOfPassengers)
            .Include(f => f.FlightRouteNavigation)
            .Include(f => f.PlaneNavigation)
            .Include(f => f.FromCityNavigation)
            .Include(f => f.ToCityNavigation)
            .OrderBy(f => f.Departure)
            .ToListAsync();
        
        return flights.FirstOrDefault();
    }
}