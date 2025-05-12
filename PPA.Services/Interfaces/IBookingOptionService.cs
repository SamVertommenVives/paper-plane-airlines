namespace PPA.Services.Interfaces;

public interface IBookingOptionService
{
    public Task<List<BookingOption>?> GetBookingOptionsAsync(int FromCityId,
        int ToCityId,
        DateTime FromDate,
        int NumberOfPassengers,
        int travelClassId);

    public Task<List<BookingOption>?> GetFirstTenBookableFlights();
}