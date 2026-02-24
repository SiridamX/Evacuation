using MediatR;
using Evacuation.Application.Features.Zones.Commands.Create.DTOs;

public class CreateZoneCommand 
    : IRequest<List<ZoneResponse>>
{
    public required List<CreateZoneRequest> Zones { get; set; }
}