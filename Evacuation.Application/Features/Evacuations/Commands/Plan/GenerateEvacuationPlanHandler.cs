using AutoMapper;
using Evacuation.Application.Exceptions;
using Evacuation.Application.Features.Evacuation.Commands.GeneratePlan;
using Evacuation.Application.Features.Evacuation.Commands.GeneratePlan.DTOs;
using Evacuation.Application.Helper;
using Evacuation.Domain.Entities;
using Evacuation.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class GenerateEvacuationPlanCommandHandler
    : IRequestHandler<GenerateEvacuationPlanCommand,
        List<EvacuationAssignmentResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    private readonly ILogger<GenerateEvacuationPlanCommandHandler> _logger;

    public GenerateEvacuationPlanCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GenerateEvacuationPlanCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<EvacuationAssignmentResponse>> Handle(
        GenerateEvacuationPlanCommand request,
        CancellationToken cancellationToken)
    {
        var zones = await GetZonesAsync(cancellationToken);

        var vehicles = await GetVehiclesAsync(cancellationToken);
        _logger.LogInformation("Vehicle count: {Count}", vehicles.Count());


        var assignments = new List<VehicleAssignment>();
        var availableVehicles = vehicles.ToList();

        foreach (var zone in zones)
        {
            var vehicleNear = SelectBestVehicle(zone, availableVehicles);
            _logger.LogInformation("vehicleNear count: {Count}", vehicleNear);

            if (vehicleNear == null)
                throw new CustomException(
                    "No available vehicles within a reasonable distance.", 409);

            // if (!vehicleNear.IsAvailable)
            //     throw new CustomException("Vehicle already assigned.", 409);


            vehicleNear.MarkUnavailable();

            var assignment = CreateAssignment(zone, vehicleNear);

            assignments.Add(assignment);
            await _unitOfWork.Assignments.AddAsync(assignment, cancellationToken);

            zone.Evacuate(assignment.AssignedPeople, vehicleNear.VehicleId);

            availableVehicles.Remove(vehicleNear);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<List<EvacuationAssignmentResponse>>(assignments);
    }

    private async Task<List<Zone>> GetZonesAsync(CancellationToken ct)
    {
        return (await _unitOfWork.Zones.GetAllAsync(ct))
            .Where(z => z.RemainingPeople > 0)
            .OrderByDescending(z => z.UrgencyLevel)
            .ToList();
    }

    private async Task<List<Vehicle>> GetVehiclesAsync(CancellationToken ct)
    {
        return (await _unitOfWork.Vehicles.GetAllAsync(ct)).ToList();
    }

    private static Vehicle? SelectBestVehicle(Zone zone, List<Vehicle> vehicles)
    {
        // double MaxDistanceKm = 10;
        return vehicles
            .Select(v => new
            {
                Vehicle = v,
                DistanceKm = DistanceCalculator.HaversineDistance(
                    v.Latitude, v.Longitude,
                    zone.Latitude, zone.Longitude)
            })
            // .Where(x => x.DistanceKm <= MaxDistanceKm)
            .OrderBy(x => x.DistanceKm)
            .ThenByDescending(x => x.Vehicle.Capacity)
            .Select(x => x.Vehicle)
            .FirstOrDefault();
    }

    private VehicleAssignment CreateAssignment(Zone zone, Vehicle vehicle)
    {
        if (vehicle.Speed <= 0)
            throw new InvalidOperationException("Vehicle speed must be more than zero");

        var distanceKm = DistanceCalculator.HaversineDistance(
            vehicle.Latitude, vehicle.Longitude,
            zone.Latitude, zone.Longitude);

        var etaHours = distanceKm / vehicle.Speed;

        var etaMinutes = (int)Math.Ceiling(etaHours * 60);
        if (zone.RemainingPeople <= 0)
            throw new CustomException("Zone already fully evacuated.", 400);
        var assignedPeople = Math.Min(vehicle.Capacity, zone.RemainingPeople);
        _logger.LogInformation("Assigned Vehicle {VehicleId} to Zone {ZoneId}. ETA: {Eta} minutes. People: {People}",
         vehicle.VehicleId, zone.ZoneId, etaMinutes, assignedPeople);
        return new VehicleAssignment
        {
            VehicleId = vehicle.VehicleId,
            ZoneId = zone.ZoneId,
            AssignedPeople = assignedPeople,
            EstimatedArrival = etaMinutes
        };
    }
}