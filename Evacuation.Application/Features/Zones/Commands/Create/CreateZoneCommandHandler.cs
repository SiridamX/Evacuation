using MediatR;
using Evacuation.Domain.Entities;
using Evacuation.Domain.Interfaces;
using Evacuation.Application.Features.Zones.Commands.Create.DTOs;
using AutoMapper;

public class CreateZoneCommandHandler
    : IRequestHandler<CreateZoneCommand, List<ZoneResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;


    public CreateZoneCommandHandler(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<ZoneResponse>> Handle(
        CreateZoneCommand request,
        CancellationToken cancellationToken)
    {
        var responses = new List<ZoneResponse>();

        foreach (var zoneRequest in request.Zones)
        {
            var zone = new Zone(
                zoneRequest.ZoneId,
                zoneRequest.LocationCoordinates.Latitude,
                zoneRequest.LocationCoordinates.Longitude,
                zoneRequest.NumberOfPeople,
                zoneRequest.UrgencyLevel
            );

            await _unitOfWork.Zones.AddAsync(zone, cancellationToken);

            responses.Add(_mapper.Map<ZoneResponse>(zone));
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return responses;
    }
}