using Microsoft.EntityFrameworkCore;
using PPA.Domains.Data;
using PPA.Domains.Entities;
using PPA.Repositories.Interfaces;

namespace PPA.Repositories;

public class ClassDAO : IDAO<Class>
{
    private readonly PPADbContext _dbContext;

    public ClassDAO(PPADbContext context)
    {
        _dbContext = context;
    }
    public async Task<IEnumerable<Class>?> GetAllAsync()
    {
        return await _dbContext.Classes.ToListAsync();
    }

    public async Task AddAsync(Class entity)
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
        throw new NotImplementedException();
    }
}