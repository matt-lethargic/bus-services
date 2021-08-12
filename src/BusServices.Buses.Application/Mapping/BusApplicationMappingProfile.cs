using AutoMapper;
using BusServices.Buses.Application.DataContracts;
using BusServices.Buses.Domain;

namespace BusServices.Buses.Application.Mapping
{
    public class BusApplicationMappingProfile : Profile
    {
        public BusApplicationMappingProfile()
        {
            CreateMap<Bus, BusDataContract>();
        }
    }
}
