using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PaperPlaneAirlines.ViewModels;
using PPA.Domains.Entities;
using PPA.Repositories.Interfaces;
using PPA.Services.Interfaces;

namespace PaperPlaneAirlines.Controllers;

public class FlightSearchController : Controller
{
    private readonly IFlightDAO _flightService;
    private readonly IService<City> _cityService;
    private readonly IMenuDAO _menuService;
    //private readonly IService<Discount> _discountService;
    private readonly IService<Class> _classService;
    private readonly IBookingOptionService _bookingOptionService;

    private readonly IMapper _mapper;


    public FlightSearchController(
        IFlightDAO flightService,
        IService<City> cityService,
        IMenuDAO menuService,
        //IService<Discount> discountService,
        IBookingOptionService bookingOptionService,
        IMapper mapper,
        IService<Class> classService)
    {
        _flightService = flightService;
        _cityService = cityService;
        _menuService = menuService;
        //_discountService = discountService;
        _bookingOptionService = bookingOptionService;
        _mapper = mapper;
        _classService = classService;
    }

    public async Task<IActionResult> Index()
    {
        var cities = await _cityService.GetAllAsync();
        var citiesList = _mapper.Map<List<CityVM>>(cities);
        
        var classes = await _classService.GetAllAsync();
        var classesList = _mapper.Map<List<TravelClassVM>>(classes);

        var bookingOptions = await GetPersonalisedBookingOptions();
        

        FlightSearchVM flightSearch = new FlightSearchVM
        {
            SelectionOptionsCity = citiesList,
            SelectionOptionsTravelClass = classesList,
            BookingOptions = bookingOptions
        };

        return View(flightSearch);
    }

    public async Task<List<BookingOptionVM>> GetPersonalisedBookingOptions()
    {
        //Todo: If user is logged in and has previous bookings: GetBookableFlightsForUser()
        var bookingOptionsList = await _bookingOptionService.GetFirstTenBookableFlights();
        List<BookingOptionVM> bookingOptions = _mapper.Map<List<BookingOptionVM>>(bookingOptionsList);
        return bookingOptions;
    }
}