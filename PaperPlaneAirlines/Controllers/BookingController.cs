using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaperPlaneAirlines.ViewModels;
using PPA.Repositories.Interfaces;
using PPA.Services.Interfaces;

namespace PaperPlaneAirlines.Controllers;

public class BookingController : Controller
{
    private readonly IFlightService _flightService;

    private readonly IMapper _mapper;

    public BookingController(IFlightService flightService, IMapper mapper)
    {
        _flightService = flightService;
        _mapper = mapper;
    }

    public IActionResult Index(BookingVM booking)
    {
        return View(booking);
    }

    public async Task<IActionResult> CreateBooking(FlightSearchCriteria searchCriteria)
    {
        HttpContext.Session.SetString("FlightSearchCriteria", JsonConvert.SerializeObject(searchCriteria));

        List<BookingOptionVM> OutboundFlights = await SearchFlights(searchCriteria);

        BookingVM booking = new BookingVM
        {
            BookingOptions = OutboundFlights,
            FlightSearchCriteria = searchCriteria
        };

        return View("Index", booking);
    }

    public async Task<IActionResult> SelectReturnFlight(FlightVM outboundFlight)
    {
        BookingVM booking = new BookingVM();
        booking.OutboundFlight = outboundFlight;

        var criteriaJSON = HttpContext.Session.GetString("FlightSearchCriteria");
        var searchCriteria = JsonConvert.DeserializeObject<FlightSearchCriteria>(criteriaJSON);

        if (searchCriteria.Roundtrip)
        {
            var fromCity = searchCriteria.FromCity;
            var toCity = searchCriteria.ToCity;

            searchCriteria.FromCity = toCity;
            searchCriteria.ToCity = fromCity;

            List<BookingOptionVM> ReturnFlights = await SearchFlights(searchCriteria);


            booking.BookingOptions = ReturnFlights;
            booking.FlightSearchCriteria = searchCriteria;
            return View("Index", booking);
        }
        
        booking.FlightSearchCriteria = searchCriteria;
        return View("Index", booking);
    }
    
    public async Task<List<BookingOptionVM>> SearchFlights(FlightSearchCriteria searchCriteria)
    {
        var flightList = await _flightService.SearchFlights(searchCriteria);
        List<FlightVM> flights = _mapper.Map<List<FlightVM>>(flightList);

        List<BookingOptionVM> bookingOptions =
            flights.Select(flight => new BookingOptionVM
            {
                OutboundFlight = flight,
                BasePrice = flight.FlightPrice
            }).ToList();

        return bookingOptions;
    }
}




