using MediatR;
using Evacuation.Application.Features.Vehicles.Commands.Create.DTOs;
using Evacuation.Domain.Entities;
using Evacuation.Domain.Interfaces;
using AutoMapper;

namespace Evacuation.Application.Features.Vehicles.Commands.Create;

public class CreateVehicleCommandHandler
    : IRequestHandler<CreateVehicleCommand, List<CreateVehicleResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateVehicleCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<CreateVehicleResponse>> Handle(
        CreateVehicleCommand request,
        CancellationToken cancellationToken)
    {
        var vehicles = new List<Vehicle>();

        foreach (var vehicleRequest in request.Vehicles)
        {
            var vehicle = new Vehicle(
                vehicleRequest.VehicleId,
                vehicleRequest.Capacity,
                vehicleRequest.Type,
                vehicleRequest.LocationCoordinates.Latitude,
                vehicleRequest.LocationCoordinates.Longitude,
                vehicleRequest.Speed
            );

            vehicles.Add(vehicle);

            await _unitOfWork.Vehicles.AddAsync(vehicle, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<List<CreateVehicleResponse>>(vehicles);
    }
}