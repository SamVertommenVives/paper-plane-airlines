using Newtonsoft.Json;
using PPA.Services.Interfaces;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace PPA.Services;

public class HotelService : IHotelService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "93c3f480c0msh58f7d1402c61e98p130cc2jsn6cfa45de28b4";
    private readonly string _apiHost = "booking-com15.p.rapidapi.com";

    public HotelService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    private async Task<string?> GetDestinationIdAsync(string city)
    {
        var url = $"https://{_apiHost}/api/v1/hotels/searchDestination?query={Uri.EscapeDataString(city)}";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("X-RapidAPI-Key", _apiKey);
        request.Headers.Add("X-RapidAPI-Host", _apiHost);

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode) return null;

        var json = await response.Content.ReadAsStringAsync();
        dynamic data = JsonConvert.DeserializeObject(json)!;
        return data.data[0]?.dest_id;
    }

    public async Task<List<Hotel>> GetHotelsAsync(string city, DateTime fromDate, DateTime? toDate)
    {
        var destId = await GetDestinationIdAsync(city);
        if (string.IsNullOrEmpty(destId)) return new();

        var from = fromDate.ToString("yyyy-MM-dd");
        var to = toDate?.ToString("yyyy-MM-dd") ?? "";

        var url =
            $"https://{_apiHost}/api/v1/hotels/searchHotels?dest_id={destId}&search_type=CITY&arrival_date={from}&departure_date={to}&adults=2";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("X-RapidAPI-Key", _apiKey);
        request.Headers.Add("X-RapidAPI-Host", _apiHost);

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode) return new();

        var json = await response.Content.ReadAsStringAsync();
        dynamic result = JsonConvert.DeserializeObject(json)!;

        var hotels = new List<Hotel>();

        for (int i = 0; i < 4 ; i++)
        {
            string hotelId = result.data.hotels[i].hotel_id;

            // Get details
            var detailsUrl =
                $"https://{_apiHost}/api/v1/hotels/getHotelDetails?hotel_id={hotelId}&arrival_date={from}&departure_date={to}&adults=2";
            var detailsReq = new HttpRequestMessage(HttpMethod.Get, detailsUrl);
            detailsReq.Headers.Add("X-RapidAPI-Key", _apiKey);
            detailsReq.Headers.Add("X-RapidAPI-Host", _apiHost);
            var detailsResp = await _httpClient.SendAsync(detailsReq);
            var detailsJson = await detailsResp.Content.ReadAsStringAsync();
            dynamic details = JsonConvert.DeserializeObject(detailsJson)!;

            // Get photo
            var photoUrl = $"https://{_apiHost}/api/v1/hotels/getHotelPhotos?hotel_id={hotelId}";
            var photoReq = new HttpRequestMessage(HttpMethod.Get, photoUrl);
            photoReq.Headers.Add("X-RapidAPI-Key", _apiKey);
            photoReq.Headers.Add("X-RapidAPI-Host", _apiHost);
            var photoResp = await _httpClient.SendAsync(photoReq);
            var photoJson = await photoResp.Content.ReadAsStringAsync();
            dynamic photos = JsonConvert.DeserializeObject(photoJson)!;
            string imageUrl = photos.data?[0]?.url ?? "";

            hotels.Add(new Hotel
            {
                Name = details.data.hotel_name,
                Address = details.data.address,
                City = details.data.city,
                Url = details.data.url,
                ImageUrl = imageUrl
            });
        }

        return hotels;
    }
}