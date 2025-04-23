namespace PPA.Services.Interfaces;

public interface IBookingOptionService
{
    public Task<List<BookingOption>?> GetBookingOptionsAsync(int FromCityId,
        int ToCityId,
        DateTime FromDate,
        int NumberOfPassengers);

    public Task<List<BookingOption>?> GetFirstTenBookableFlights();
}