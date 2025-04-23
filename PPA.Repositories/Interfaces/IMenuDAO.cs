using PPA.Domains.Entities;

namespace PPA.Repositories.Interfaces;

public interface IMenuDAO : IDAO<Meal>
{
    Task<IEnumerable<Meal>?> GetAllLocalMenusForCityAsync(int cityId);
    Task<IEnumerable<Meal>?> GetAllStandardMenusAsync();
}