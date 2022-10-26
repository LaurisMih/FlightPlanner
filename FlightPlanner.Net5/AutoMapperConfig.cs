using AutoMapper;
using FlightPlanner.Net5.Models;
using FlightPlannerNet5;

namespace FlightPlanner.Net5
{
    public class AutoMapperConfig
    {
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AirportRequest, Airport>()
                    .ForMember(a => a.Id, options => options.Ignore())
                    .ForMember(a => a.AirportName, opt =>
                        opt.MapFrom(a => a.Airport));
                cfg.CreateMap<Airport, AirportRequest>().ForMember(a => a.Airport, opt =>
                    opt.MapFrom(a => a.AirportName));
                cfg.CreateMap<FlightRequest, Flight>();
                cfg.CreateMap<Flight, FlightRequest>();
            });
          
            return config.CreateMapper();
        }
    }
}
