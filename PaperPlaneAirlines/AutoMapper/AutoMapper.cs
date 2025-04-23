using AutoMapper;
using PaperPlaneAirlines.ViewModels;
using PPA.Domains.Entities;
using PPA.Services;
using PPA.Services.Interfaces;

namespace PaperPlaneAirlines.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Flight, FlightVM>()
            .ForMember(dest => dest.FromCity,
                opts => opts.MapFrom(
                    src => src.FromCityNavigation
                ))
            .ForMember(dest => dest.ToCity,
                opts => opts.MapFrom(
                    src => src.ToCityNavigation))
            .ForMember(dest => dest.Plane,
                opts => opts.MapFrom(
                    src => src.PlaneNavigation.Name
                )
            );

        CreateMap<City, CityVM>()
            .ForMember(dest => dest.AirportName,
                opts => opts.MapFrom(
                    src => src.AirportNavigation.IATA
                ));

        CreateMap<Class, TravelClassVM>();

        CreateMap<Meal, MenuVM>();
        
        CreateMap<BookingOption, BookingOptionVM>();

        CreateMap<FlightBookingCRUD, FlightBooking>();

        CreateMap<BookingCRUD, Booking>()
            .ForMember(dest => dest.FlightBookings,
                opts => opts.MapFrom(
                    src => src.Flights
                ));

        CreateMap<Hotel, HotelVM>();

    }
}