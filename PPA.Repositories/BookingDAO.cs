using Microsoft.EntityFrameworkCore;
using PPA.Domains.Data;
using PPA.Domains.Entities;
using PPA.Repositories.Interfaces;

namespace PPA.Repositories;

public class BookingDAO : IDAO<Booking>
{
    private readonly PPADbContext _dbContext;

    public BookingDAO(PPADbContext dbContext)
    {
        this._dbContext = dbContext;
    }


    public async Task<IEnumerable<Booking>?> GetAllAsync()
    {
        try
        {
            return await _dbContext.Bookings
                .Include(b => b.FlightBookings)
                .Include(b => b.UserDiscountNavigation)
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<int> AddAsync(Booking entity)
    {
        _dbContext.Entry(entity).State = EntityState.Added;
        try
        {
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task DeleteAsync(Booking entity)
    {
        _dbContext.Entry(entity).State = EntityState.Deleted;
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

    public async Task UpdateAsync(Booking entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
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

    public async Task<Booking?> FindByIdAsync(int id)
    {
        try
        {
            return await _dbContext.Bookings.Where(b => b.Id == id)
                .Include(b => b.FlightBookings)
                .Include(b => b.UserDiscountNavigation)
                .FirstOrDefaultAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}