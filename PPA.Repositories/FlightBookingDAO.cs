using Microsoft.EntityFrameworkCore;
using PPA.Domains.Data;
using PPA.Domains.Entities;
using PPA.Repositories.Interfaces;

namespace PPA.Repositories;

public class FlightBookingDAO : IDAO<FlightBooking>
{
    private readonly PPADbContext _dbContext;

    public FlightBookingDAO(PPADbContext dbContext)
    {
        this._dbContext = dbContext;
    }


    public async Task<IEnumerable<FlightBooking>?> GetAllAsync()
    {
        try
        {
            return await _dbContext.FlightBookings
                .Include(fb => fb.FlightNavigation)
                .Include(fb => fb.BookingNavigation)
                .Include(fb => fb.MealNavigation)
                .Include(fb => fb.ClassNavigation)
                .Include(fb => fb.FlightDiscountNavigation)
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task AddAsync(FlightBooking entity)
    {
        _dbContext.FlightBookings.Add(entity);
        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task DeleteAsync(FlightBooking entity)
    {
        _dbContext.FlightBookings.Remove(entity);
        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task UpdateAsync(FlightBooking entity)
    {
        _dbContext.FlightBookings.Update(entity);
        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<FlightBooking?> FindByIdAsync(int id)
    {
        try
        {
            return await _dbContext.FlightBookings.Where( fb => fb.Id == id )
                .Include(fb => fb.FlightNavigation)
                .Include(fb => fb.BookingNavigation)
                .Include(fb => fb.MealNavigation)
                .Include(fb => fb.ClassNavigation)
                .Include(fb => fb.FlightDiscountNavigation)
                .FirstOrDefaultAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}