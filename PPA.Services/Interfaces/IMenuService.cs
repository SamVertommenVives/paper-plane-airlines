using PPA.Domains.Entities;

namespace PPA.Services.Interfaces;

public interface IMenuService : IService<Meal>
{
    Task<IEnumerable<Meal>?> GetAllLocalMenusForCityAsync(int cityId);
    Task<IEnumerable<Meal>?> GetAllStandardMenusAsync();
}