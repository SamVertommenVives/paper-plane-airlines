using PPA.Domains.Entities;
using PPA.Repositories.Interfaces;
using PPA.Services.Interfaces;

namespace PPA.Services;

public class FlightBookingService : IFlightBookingService
{
    IFlightBookingDAO _dao;
    public FlightBookingService(IFlightBookingDAO dao)
    { 
        _dao = dao;
    }
    
    public async Task<IEnumerable<FlightBooking>?> GetAllAsync()
    {
        return await _dao.GetAllAsync();
    }

    public async Task<int> AddAsync(FlightBooking entity)
    {
        await _dao.AddAsync(entity);
        return entity.Id;
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

    public async Task<IEnumerable<FlightBooking>?> GetAllBookingsForFlightAndClassAsync(int flightId, int classId)
    {
        return await _dao.GetAllBookingsForFlightAndClassAsync(flightId, classId);
    }
}