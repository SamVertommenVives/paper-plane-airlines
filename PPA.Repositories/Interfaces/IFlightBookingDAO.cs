using PPA.Domains.Entities;

namespace PPA.Repositories.Interfaces;

public interface IFlightBookingDAO : IDAO<FlightBooking>
{
    public Task<IEnumerable<FlightBooking>?> GetAllBookingsForFlightAndClassAsync(int flightId, int classId);
}