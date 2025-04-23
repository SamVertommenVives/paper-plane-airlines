namespace PPA.Services.Interfaces;

public interface IHotelService
{
    Task<List<Hotel>> GetHotelsAsync(string city, DateTime fromDate, DateTime? toDate);
}

public class Hotel
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Url { get; set; }
    public string ImageUrl { get; set; }
}