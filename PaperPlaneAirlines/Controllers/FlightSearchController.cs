using System.Collections;
using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaperPlaneAirlines.Models;
using PaperPlaneAirlines.ViewModels;
using PPA.Domains.Entities;
using PPA.Repositories.Interfaces;
using PPA.Services;
using PPA.Services.Interfaces;

namespace PaperPlaneAirlines.Controllers;

public class FlightSearchController : Controller
{
    private readonly IFlightService _flightService;
    private readonly IService<City> _cityService;
    private readonly IService<Class> _classService;

    private readonly IMapper _mapper;

    public FlightSearchController(IFlightService flightService, IService<City> cityService,
        IService<Class> classService, IMapper mapper)
    {
        _flightService = flightService;
        _cityService = cityService;
        _classService = classService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var citiesList = await _cityService.GetAllAsync();
            List<CityVM> cities = _mapper.Map<List<CityVM>>(citiesList);

            var classList = await _classService.GetAllAsync();
            List<ClassVM> classes = _mapper.Map<List<ClassVM>>(classList);
            
            var flightList = await _flightService.GetFirstTenBookableFlights();
            List<FlightVM> flights = _mapper.Map<List<FlightVM>>(flightList);

            List<BookingOptionVM> bookingOptions = 
                flights.Select(flight => new BookingOptionVM { OutboundFlight = flight, BasePrice = flight.FlightPrice }).ToList();

            FlightSearchVM vm = new FlightSearchVM
            {
                Cities = cities,
                TravelClasses = classes,
                BookingOptions = bookingOptions
            };

            return View(vm);
        }
        catch (Exception e)
        {
            Debug.WriteLine("errorlog " + e.Message);
        }

        return View();
    }
}