using Microsoft.EntityFrameworkCore;
using PPA.Domains.Data;
using PPA.Domains.Entities;
using PPA.Repositories.Interfaces;

namespace PPA.Repositories;

public class ClassDAO : IDAO<Class>
{
    private readonly PPADbContext _dbContext;


    public ClassDAO(PPADbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IEnumerable<Class>?> GetAllAsync()
    {
        try
        {
            return await _dbContext.Classes.ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<int> AddAsync(Class entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Class entity)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Class entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Class?> FindByIdAsync(int id)
    {
        try
        {
            return await _dbContext.Classes.Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}