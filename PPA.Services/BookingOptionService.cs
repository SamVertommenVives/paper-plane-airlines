using System.Collections;
using PPA.Domains.Entities;
using PPA.Services.Interfaces;

namespace PPA.Services;

public class BookingOptionService : IBookingOptionService
{
    //TODO: This should probably be stored somewhere else
    internal double PRICE_PER_KM = 0.02;
    
    private readonly IFlightService _flightService;
    private readonly IFlightRouteService _flightRouteService;
    private readonly IService<City> _cityService;

    public BookingOptionService(IFlightService flightService, IFlightRouteService flightRouteService, IService<City> cityService)
    {
        _flightService = flightService;
        _flightRouteService = flightRouteService;
        _cityService = cityService;
    }

    public async Task<List<BookingOption>?> GetBookingOptionsAsync(int FromCityId, int ToCityId, DateTime FromDate, int NumberOfPassengers)
    {
        List<BookingOption>? bookingOptions = new List<BookingOption>();
        
        Console.WriteLine("fetching fromCity");
        City fromCity = await _cityService.FindByIdAsync(FromCityId) ?? throw new Exception("fromCity with id: " + FromCityId + " not found.");
        Console.WriteLine("fetching toCity");
        City toCity = await _cityService.FindByIdAsync(ToCityId)?? throw new Exception("toCity with id: " + ToCityId + " not found.");
        
        var routeOptions = await _flightRouteService.GetFlightRoutesAsync(fromCity.Airport, toCity.Airport);

        foreach (var routeOption in routeOptions)
        {
            DateTime departureDate = FromDate;
            List<Flight> flights = new List<Flight>();
            double price = 0;
            foreach (var flightRoute in routeOption)
            {
                //find next flight
                Flight flight = await _flightService.GetNextFlightForRoute(flightRoute.Id, departureDate, NumberOfPassengers);
                flights.Add(flight);
                price += CalculatePrice(flight);
                departureDate = flight.Arrival.AddHours(2);
            }
            
            //TODO: now something to find reductions 

            BookingOption bookingOption = new BookingOption
            {
                Flights = flights,
                Price = price
            };
            
            bookingOptions.Add(bookingOption);
        }
        
        return bookingOptions;
    }

    public async Task<List<BookingOption>?> GetFirstTenBookableFlights()
    {
        List<BookingOption>? bookingOptions = new List<BookingOption>();
        var flights = await _flightService.GetFirstTenBookableFlights();
        
        foreach (var flight in flights)
        {
            BookingOption bookingOption = new BookingOption
            {
                Flights = [flight],
                Price = CalculatePrice(flight)
            };
            bookingOptions.Add(bookingOption);
        }
        
        return bookingOptions;
    }

    private double CalculatePrice(Flight flight)
    {
        return flight.FlightRouteNavigation.Distance * PRICE_PER_KM;
    }
}

public class BookingOption
{
    public List<Flight>? Flights { get; set; }
    public Discount? Reduction { get; set; }
    public double? Price { get; set; }
}