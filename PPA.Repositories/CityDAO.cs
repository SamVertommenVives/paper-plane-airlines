using Microsoft.EntityFrameworkCore;
using PPA.Domains.Data;
using PPA.Domains.Entities;
using PPA.Repositories.Interfaces;

namespace PPA.Repositories;

public class CityDAO : IDAO<City>
{
    private readonly PPADbContext _dbContext;

    public CityDAO(PPADbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<City>?> GetAllAsync()
    {
        try
        {
            return await _dbContext.Cities
                .Include(c => c.AirportNavigation)
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task AddAsync(City entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(City entity)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(City entity)
    {
        throw new NotImplementedException();
    }

    public async Task<City?> FindByIdAsync(int id)
    {
        try
        {
            return await _dbContext.Cities.Where(c => c.Id == id)
                .Include(c => c.AirportNavigation)
                .FirstOrDefaultAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}