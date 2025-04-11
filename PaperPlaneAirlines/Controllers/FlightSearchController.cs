using Microsoft.AspNetCore.Mvc;
using PPA.Domains.Entities;
using PPA.Services.Interfaces;

namespace PaperPlaneAirlines.Controllers;

public class FlightSearchController : Controller
{
    private IService<Flight> flightService;
    private IService<City> cityService;
    
}