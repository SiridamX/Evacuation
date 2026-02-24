using MediatR;
using Evacuation.Application.Features.Vehicles.Commands.Create.DTOs;

namespace Evacuation.Application.Features.Vehicles.Commands.Create;

public class CreateVehicleCommand : IRequest<List<CreateVehicleResponse>>
{
    public required List<CreateVehicleRequest> Vehicles { get; set; }
}