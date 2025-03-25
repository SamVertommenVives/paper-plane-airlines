using AutoMapper;
using PaperPlaneAirlines.ViewModels;
using PPA.Domains.Entities;

namespace PaperPlaneAirlines.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Flight, FlightVM>();
        //CreateMap<TSource, TDestination>;
    }
}