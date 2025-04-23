using Microsoft.EntityFrameworkCore;
using PPA.Domains.Data;
using PPA.Domains.Entities;

namespace PPA.Repositories.Interfaces;

public class CityDiscountDAO : IDAO<CityDiscount>
{
    private readonly PPADbContext _dbContext;


    public CityDiscountDAO(PPADbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<CityDiscount>?> GetAllAsync()
    {
        try
        {
            return await _dbContext.CityDiscounts
                .Include(cd => cd.CityNavigation)
                .Include(cd => cd.DiscountNavigation)
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<int> AddAsync(CityDiscount entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(CityDiscount entity)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(CityDiscount entity)
    {
        throw new NotImplementedException();
    }

    public async Task<CityDiscount?> FindByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}