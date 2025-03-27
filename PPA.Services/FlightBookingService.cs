using PPA.Domains.Entities;
using PPA.Repositories.Interfaces;
using PPA.Services.Interfaces;

namespace PPA.Services;

public class FlightBookingService : IService<FlightBooking>
{
    IDAO<FlightBooking> _dao;
    FlightBookingService(IDAO<FlightBooking> dao)
    { 
        _dao = dao;
    }
    
    public async Task<IEnumerable<FlightBooking>?> GetAllAsync()
    {
        return await _dao.GetAllAsync();
    }

    public async Task AddAsync(FlightBooking entity)
    {
        await _dao.AddAsync(entity);
    }

    public async Task DeleteAsync(FlightBooking entity)
    {
        await _dao.DeleteAsync(entity);
    }

    public async Task UpdateAsync(FlightBooking entity)
    {
        await _dao.UpdateAsync(entity);
    }

    public async Task<FlightBooking?> FindByIdAsync(int id)
    {
        return await _dao.FindByIdAsync(id);
    }
}