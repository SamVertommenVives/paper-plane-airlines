using PPA.Domains.Entities;
using PPA.Repositories.Interfaces;
using PPA.Services.Interfaces;

namespace PPA.Services;

public class ClassService : IService<Class>
{
    private readonly IDAO<Class> _dao;


    public ClassService(IDAO<Class> dao)
    {
        _dao = dao;
    }

    public async Task<IEnumerable<Class>?> GetAllAsync()
    {
        return await _dao.GetAllAsync();
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

    public async Task<Class?> FindByIdAsync(int Id)
    {
        return await _dao.FindByIdAsync(Id);
    }
}