using PPA.Domains.Entities;
using PPA.Repositories.Interfaces;
using PPA.Services.Interfaces;

namespace PPA.Services;

public class DiscountService : IService<CityDiscount>, IService<UserDiscount>
{
    private readonly IDAO<CityDiscount> _cityDiscountDAO;
    private readonly IDAO<UserDiscount> _userDiscountDAO;


    public DiscountService(IDAO<CityDiscount> cityDiscountDao, IDAO<UserDiscount> userDiscountDao)
    {
        _cityDiscountDAO = cityDiscountDao;
        _userDiscountDAO = userDiscountDao;
    }

    async Task<IEnumerable<CityDiscount>?> IService<CityDiscount>.GetAllAsync()
    {
        try
        {
            return await _cityDiscountDAO.GetAllAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<int> AddAsync(UserDiscount entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(UserDiscount entity)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(UserDiscount entity)
    {
        throw new NotImplementedException();
    }
    
    //TODO: change to Find by UserId
    async Task<UserDiscount?> IService<UserDiscount>.FindByIdAsync(int Id)
    {
        return await _userDiscountDAO.FindByIdAsync(Id);
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

    async Task<IEnumerable<UserDiscount>?> IService<UserDiscount>.GetAllAsync()
    {
        try
        {
            return await _userDiscountDAO.GetAllAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    
    async Task<CityDiscount?> IService<CityDiscount>.FindByIdAsync(int Id) 
    {
        return await _cityDiscountDAO.FindByIdAsync(Id);
    }
}