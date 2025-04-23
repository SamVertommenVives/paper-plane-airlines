using Microsoft.EntityFrameworkCore;
using PPA.Domains.Data;
using PPA.Domains.Entities;
using PPA.Repositories.Interfaces;

namespace PPA.Repositories;

public class MenuDAO : IMenuDAO
{
    private readonly PPADbContext _dbContext;

    public MenuDAO(PPADbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Meal>?> GetAllAsync()
    {
        try
        {
            return await _dbContext.Meals
                .Include(m => m.LocalMealForNavigation)
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<int> AddAsync(Meal entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Meal entity)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Meal entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Meal?> FindByIdAsync(int id)
    {
        try
        {
            return await _dbContext.Meals.Where(
                    m => m.Id == id)
                .Include(m => m.LocalMealForNavigation)
                .FirstOrDefaultAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<Meal>?> GetAllLocalMenusForCityAsync(int CityId)
    {
        try
        {
            return await _dbContext.Meals.Where(m => m.LocalMealFor == CityId).ToListAsync();
        }
        catch
        {
            Console.WriteLine();
            throw;
        }
    }

    public async Task<IEnumerable<Meal>?> GetAllStandardMenusAsync()
    {
        try
        {
            return await _dbContext.Meals.Where(m => m.Type == "Standard").ToListAsync();
        }
        catch
        {
            Console.WriteLine();
            throw;
        }
    }
}