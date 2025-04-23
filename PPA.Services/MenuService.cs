using PPA.Domains.Entities;
using PPA.Repositories;
using PPA.Repositories.Interfaces;

namespace PPA.Services.Interfaces;

public class MenuService : IMenuService
{
    private readonly IMenuDAO _menuDAO;


    public MenuService(IMenuDAO menuDao)
    {
        _menuDAO = menuDao;
    }

    public async Task<IEnumerable<Meal>?> GetAllAsync()
    {
        return await _menuDAO.GetAllAsync();
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

    public async Task<Meal?> FindByIdAsync(int Id)
    {
        return await _menuDAO.FindByIdAsync(Id);
    }

    public async Task<IEnumerable<Meal>?> GetAllStandardMenusAsync()
    {
        try
        {
            return await _menuDAO.GetAllStandardMenusAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<Meal>?> GetAllLocalMenusForCityAsync(int cityId)
    {
        try
        {
            return await _menuDAO.GetAllLocalMenusForCityAsync(cityId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}