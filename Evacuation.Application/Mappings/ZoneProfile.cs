using AutoMapper;
using Evacuation.Application.Features.Zones.Commands.Create.DTOs;
using Evacuation.Domain.Entities;

namespace Evacuation.Application.Mappings;

public class ZoneProfile : Profile
{
    public ZoneProfile()
    {
        CreateMap<Zone, ZoneResponse>();
    }
}