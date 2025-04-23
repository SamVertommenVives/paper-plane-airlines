using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaperPlaneAirlines.Models;
using PaperPlaneAirlines.ViewModels;
using PPA.Domains.Entities;
using PPA.Services.Interfaces;

namespace PaperPlaneAirlines.Controllers;

public class BookingController : Controller
{
    private readonly IBookingOptionService _bookingOptionService;
    private readonly IService<City> _cityService;
    private readonly IService<Class> _classService;
    private readonly IMenuService _menuService;
    private readonly IService<Booking> _bookingService;
    private readonly IService<FlightBooking> _flightBookingService;
    private readonly IMapper _mapper;
    private readonly IHotelService _hotelService;
    private readonly SendController _sendController;

    public BookingController(IBookingOptionService bookingOptionService, IMapper mapper, IService<City> cityService,
        IService<Class> classService, IMenuService menuService, IService<Booking> bookingService, IService<FlightBooking> flightBookingService, IHotelService hotelService, SendController sendController)
    {
        _bookingOptionService = bookingOptionService;
        _mapper = mapper;
        _cityService = cityService;
        _classService = classService;
        _menuService = menuService;
        _bookingService = bookingService;
        _flightBookingService = flightBookingService;
        _hotelService = hotelService;
        _sendController = sendController;
    }

    public async Task<IActionResult> BookingOverview()
    {
        var bookingJson = HttpContext.Session.GetString("BookingVM");
        BookingVM? booking = null;

        if (bookingJson != null)
        {
            booking =
                JsonConvert.DeserializeObject<BookingVM>(bookingJson ?? throw new NullReferenceException());
        }

        if (booking.OutboundFlight == null)
        {
            return View("SelectFlight", await SelectOutboundFlight());
        }

        if (booking.RoundTrip && booking.ReturnFlight == null)
        {
            return View("SelectFlight", await SelectReturnFlight());
        }

        return View("BookingOverview", booking);
    }

    [HttpPost]
    public async Task<IActionResult> BookingOverview(FlightSearchVM? flightSearchVM)
    {
        if (flightSearchVM != null)
        {
            await CreateSessionObjectBooking(flightSearchVM);
        }

        return await BookingOverview();
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult>
        ConfirmBooking()
    {
        
        var bookingJson = HttpContext.Session.GetString("BookingVM");
        var booking =
            JsonConvert.DeserializeObject<BookingVM>(bookingJson ?? throw new NullReferenceException());
        
        string? userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (booking.OutboundFlight != null)
        {
            try
            {
                await AddBookingToDb(userId, booking.OutboundFlight, booking.TravelClass.Id);
                Console.WriteLine("outbound bookingOption successfully added to the Db");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        
        if (booking.ReturnFlight != null)
        {
            try
            {
                await AddBookingToDb(userId, booking.ReturnFlight, booking.TravelClass.Id);
                Console.WriteLine("return bookingOption successfully added to the Db");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        
        var email = User?.FindFirstValue(ClaimTypes.Email);
        if (email != null)
        {
            try
            {

                Console.WriteLine("trying to send email to user");
                await _sendController.SendConfirmationEmail(email);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        return View("BookingConfirmed", booking);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetHotelsPartial(string toCity, DateTime fromDate, DateTime toDate)
    {
        var hotelsList = await _hotelService.GetHotelsAsync(toCity, fromDate, toDate);
        var hotels = _mapper.Map<List<HotelVM>>(hotelsList);

        return PartialView("_HotelDetailsPartial", hotels);
    }

    private async Task AddBookingToDb(string userId, BookingOptionVM bookingOption, int travelClassId)
    {
        BookingCRUD booking = new BookingCRUD
        { 
            User = userId,
        };
        
        try
        {
            var mappedBooking = _mapper.Map<Booking>(booking);
            var bookingId = await _bookingService.AddAsync(mappedBooking);
            Console.WriteLine("booking successfully added to the Db with id: " + bookingId);
            
            foreach (var flight in bookingOption.Flights)
            {
                FlightBookingCRUD flightBookingCrud = new FlightBookingCRUD
                {
                    Booking = bookingId,
                    Flight = flight.Id,
                    Meal = bookingOption.Menu?.Id,
                    SeatNumber = flight.Plane + new Random().Next(0, 100),
                    Class = travelClassId,
                    FlightDiscount = null
                };
                
                await _flightBookingService.AddAsync(_mapper.Map<FlightBooking>(flightBookingCrud));
                Console.WriteLine("flightBooking successfully added to the Db");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task CreateSessionObjectBooking(FlightSearchVM flightSearchVM)
    {
        if (flightSearchVM == null) throw new ArgumentNullException(nameof(flightSearchVM));

        var fromCity = await _cityService.FindByIdAsync(flightSearchVM.SelectedFromCityId);
        flightSearchVM.FromCity = _mapper.Map<CityVM>(fromCity);

        if (flightSearchVM.SelectedToCityId != null)
        {
            var toCity = await _cityService.FindByIdAsync(flightSearchVM.SelectedToCityId);
            flightSearchVM.ToCity = _mapper.Map<CityVM>(toCity);
        }

        var travelClass = await _classService.FindByIdAsync(flightSearchVM.SelectedTravelClassId);
        flightSearchVM.TravelClass = _mapper.Map<TravelClassVM>(travelClass);

        BookingVM booking = new BookingVM
        {
            FromDate = flightSearchVM.FromDate,
            ToDate = flightSearchVM.ToDate,
            FromCity = flightSearchVM.FromCity,
            ToCity = flightSearchVM.ToCity,
            RoundTrip = flightSearchVM.RoundTrip,
            TravelClass = flightSearchVM.TravelClass
        };

        HttpContext.Session.SetString("BookingVM", JsonConvert.SerializeObject(booking));
    }
    public async Task<FlightSelectionVM> GetFlightSelection(int fromCityId, int toCityId, FlightType flightType)
    {
        var bookingJson = HttpContext.Session.GetString("BookingVM");
        var booking =
            JsonConvert.DeserializeObject<BookingVM>(bookingJson ?? throw new NullReferenceException());

        var date = flightType == FlightType.Outbound ? booking.FromDate : booking.ToDate;
        
        var bookingOptionsList = await _bookingOptionService.GetBookingOptionsAsync(
            fromCityId,
            toCityId,
            (DateTime) date,
            booking.NumberOfPassengers
        );
        var bookingOptions = _mapper.Map<List<BookingOptionVM>>(bookingOptionsList);

        var counter = 1;
        foreach (var bookingOption in bookingOptions)
        {
            bookingOption.BookingOptionId = counter;
            counter++;
        }

        FlightSelectionVM flightSelection = new FlightSelectionVM
        {
            FlightType = flightType,
            Booking = booking,
            BookingOptions = bookingOptions
        };

        return flightSelection;
    }

    public async Task<FlightSelectionVM> SelectOutboundFlight()
    {
        var bookingJson = HttpContext.Session.GetString("BookingVM");
        var booking =
            JsonConvert.DeserializeObject<BookingVM>(bookingJson ?? throw new NullReferenceException());

        FlightSelectionVM flightSelection =
            await GetFlightSelection(booking.FromCity.Id, booking.ToCity.Id, FlightType.Outbound);

        HttpContext.Session.SetString("FlightOptions", JsonConvert.SerializeObject(flightSelection));

        return flightSelection;
    }
    public async Task<FlightSelectionVM> SelectReturnFlight()
    {
        var bookingJson = HttpContext.Session.GetString("BookingVM");
        var booking =
            JsonConvert.DeserializeObject<BookingVM>(bookingJson ?? throw new NullReferenceException());

        FlightSelectionVM flightSelection =
            await GetFlightSelection(booking.ToCity.Id, booking.FromCity.Id, FlightType.Return);

        HttpContext.Session.SetString("FlightOptions", JsonConvert.SerializeObject(flightSelection));

        return flightSelection;
    }

    public async Task<IActionResult> SetBookingOptionForSelectedFlight(int flightOptionId)
    {
        
        var bookingJson = HttpContext.Session.GetString("BookingVM");
        var booking = JsonConvert.DeserializeObject<BookingVM>(bookingJson ?? throw new NullReferenceException());

        var flightSelectionJson = HttpContext.Session.GetString("FlightOptions");
        var flightSelection =
            JsonConvert.DeserializeObject<FlightSelectionVM>(flightSelectionJson ?? throw new NullReferenceException());

        var selectedFlightOption = flightSelection.BookingOptions.Find(b => b.BookingOptionId == flightOptionId);

        if (flightSelection.FlightType.Equals(FlightType.Outbound))
        {
            selectedFlightOption.FlightType = FlightType.Outbound;
            booking.OutboundFlight = selectedFlightOption;
        }

        if (flightSelection.FlightType.Equals(FlightType.Return))
        {
            selectedFlightOption.FlightType = FlightType.Return;
            booking.ReturnFlight = selectedFlightOption;
        }

        HttpContext.Session.SetString("BookingVM", JsonConvert.SerializeObject(booking));
        HttpContext.Session.Remove("FlightOptions");

        return await BookingOverview();
    }

    public async Task<IActionResult> SelectMenu(FlightType flightType)
    {
        var bookingJson = HttpContext.Session.GetString("BookingVM");
        var booking =
            JsonConvert.DeserializeObject<BookingVM>(bookingJson ?? throw new NullReferenceException());

        var selectedFlight = flightType == FlightType.Outbound
            ? booking.OutboundFlight
            : booking.ReturnFlight;

        var destinationCityId = selectedFlight.Flights.Last().ToCity.Id;

        var standardMenuList = await _menuService.GetAllStandardMenusAsync();
        var localMeals = await _menuService.GetAllLocalMenusForCityAsync(destinationCityId);

        MenuSelectionVM selectionOptions = new MenuSelectionVM
        {
            BookingOption = selectedFlight,
            StandardMenus = _mapper.Map<List<MenuVM>>(standardMenuList),
            LocalMenus = _mapper.Map<List<MenuVM>>(localMeals)
        };

        HttpContext.Session.SetString("MenuSelectionVM", JsonConvert.SerializeObject(selectionOptions));

        return View("SelectMenu", selectionOptions);
    }

    public async Task<IActionResult> SetMenuForFlightOption(int selectedMenuId)
    {
        var bookingJson = HttpContext.Session.GetString("BookingVM");
        var booking =
            JsonConvert.DeserializeObject<BookingVM>(bookingJson ?? throw new NullReferenceException());

        var menuSelectionJson = HttpContext.Session.GetString("MenuSelectionVM");
        var selection =
            JsonConvert.DeserializeObject<MenuSelectionVM>(menuSelectionJson ?? throw new NullReferenceException());

        var selectedMenu = await _menuService.FindByIdAsync(selectedMenuId);
        
        var selectedFlight = selection.BookingOption.FlightType == FlightType.Outbound
            ? booking.OutboundFlight
            : booking.ReturnFlight;
        
        selectedFlight.Menu = _mapper.Map<MenuVM>(selectedMenu);

        HttpContext.Session.SetString("BookingVM", JsonConvert.SerializeObject(booking));

        return await BookingOverview();
    }
}