using PPA.Domains.Entities;
using PPA.Repositories.Interfaces;
using PPA.Services.Interfaces;

namespace PPA.Services;

public class CityService : IService<City>
{
    IDAO<City> _dao;

    CityService(IDAO<City> dao)
    {
        _dao = dao;
    }

    public async Task<IEnumerable<City>?> GetAllAsync()
    {
        return await _dao.GetAllAsync();
    }

    public async Task AddAsync(City entity)
    {
        await _dao.AddAsync(entity);
    }

    public async Task DeleteAsync(City entity)
    {
        await _dao.DeleteAsync(entity);
    }

    public async Task UpdateAsync(City entity)
    {
        await _dao.UpdateAsync(entity);
    }

    public async Task<City?> FindByIdAsync(int id)
    {
        return await _dao.FindByIdAsync(id);
    }
}