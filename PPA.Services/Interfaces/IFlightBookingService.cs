using PPA.Domains.Entities;

namespace PPA.Services.Interfaces;

public interface IFlightBookingService : IService<FlightBooking>
{
    public Task<IEnumerable<FlightBooking>?> GetAllBookingsForFlightAndClassAsync(int flightId, int classId);
}