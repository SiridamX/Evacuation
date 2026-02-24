using AutoMapper;
using Evacuation.Application.Features.Vehicles.Commands.Create;
using Evacuation.Application.Features.Vehicles.Commands.Create.DTOs;
using Evacuation.Domain.Entities;

namespace Evacuation.Application.Mappings;

public class VehicleProfile : Profile
{
    public VehicleProfile()
    {
        CreateMap<CreateVehicleRequest, Vehicle>();
        CreateMap<Vehicle, CreateVehicleResponse>();
    }
}