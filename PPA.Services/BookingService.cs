using PPA.Domains.Entities;
using PPA.Repositories.Interfaces;
using PPA.Services.Interfaces;

namespace PPA.Services;

public class BookingService : IService<Booking>
{
    private readonly IDAO<Booking> _dao;

    public BookingService(IDAO<Booking> dao)
    {
        this._dao = dao;
    }
    public async Task<IEnumerable<Booking>?> GetAllAsync()
    {
        return await _dao.GetAllAsync();
    }

    public async Task AddAsync(Booking entity)
    {
        await _dao.AddAsync(entity);
    }

    public async Task DeleteAsync(Booking entity)
    {
        await _dao.DeleteAsync(entity);
    }

    public async Task UpdateAsync(Booking entity)
    {
        await _dao.UpdateAsync(entity);
    }

    public async Task<Booking?> FindByIdAsync(int id)
    {
        return await _dao.FindByIdAsync(id);
    }
}