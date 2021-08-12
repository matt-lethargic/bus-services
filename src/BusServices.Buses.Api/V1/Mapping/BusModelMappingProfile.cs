using AutoMapper;
using BusServices.Buses.Api.V1.Models;
using BusServices.Buses.Application.DataContracts;

namespace BusServices.Buses.Api.V1.Mapping
{
    public class BusModelMappingProfile : Profile
    {
        public BusModelMappingProfile()
        {
            CreateMap<BusDataContract, BusModel>();
        }
    }
}
