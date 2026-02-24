using MediatR;
using Evacuation.Domain.Interfaces;
using Evacuation.Application.Common.Interfaces;
using Evacuation.Domain.Entities;
using Evacuation.Application.Features.Evacuation.Queries.GetStatus.DTOs;
namespace Evacuation.Application.Features.Evacuation.Commands.Update.DTOs;

public class UpdateEvacuationCommandHandler
    : IRequestHandler<UpdateEvacuationCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRedisStore _redisStore;

    public UpdateEvacuationCommandHandler(IUnitOfWork unitOfWork, IRedisStore redisStore)
    {
        _unitOfWork = unitOfWork;
        _redisStore = redisStore;
    }

    public async Task<Unit> Handle(UpdateEvacuationCommand request, CancellationToken cancellationToken)
    {
        var zone = await _unitOfWork.Zones
            .GetByIdAsync(request.ZoneId, cancellationToken);

        if (zone == null)
            throw new Exception("Zone not found");

        zone.Evacuate(request.PeopleMoved, request.VehicleId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _redisStore.SetStatusAsync(new EvacuationStatusResponse
        {
            ZoneId = zone.ZoneId,
            TotalEvacuated = zone.NumberOfPeople,
            RemainingPeople = zone.RemainingPeople,
            LastVehicleUsed = zone.LastVehicleUsed
        });

        return Unit.Value;
    }
}