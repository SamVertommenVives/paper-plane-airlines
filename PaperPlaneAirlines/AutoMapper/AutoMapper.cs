using AutoMapper;
using PaperPlaneAirlines.ViewModels;
using PPA.Domains.Entities;

namespace PaperPlaneAirlines.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Flight, FlightVM>()
            .ForMember(dest => dest.FromCity,
                opts => opts.MapFrom(
                    src => src.FromCityNavigation.Name
                ))
            .ForMember(dest => dest.ToCity,
                opts => opts.MapFrom(
                    src => src.ToCityNavigation.Name))
            .ForMember(dest => dest.Plane,
                opts => opts.MapFrom(
                    src => src.PlaneNavigation.Name
                )
            )
            .ForMember(dest => dest.FlightPrice,
                opts => opts.MapFrom(
                    src => Math.Round(Convert.ToDouble(src.FlightRouteNavigation.Distance * 0.08),2)
                ));

        CreateMap<City, CityVM>()
            .ForMember(dest => dest.AirportName,
                opts => opts.MapFrom(
                    src => src.AirportNavigation.IATA
                )
            );

        CreateMap<Class, ClassVM>();
    }
}