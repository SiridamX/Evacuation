using AutoMapper;
using Evacuation.Domain.Entities;
using Evacuation.Application.Features.Evacuation.Commands.GeneratePlan.DTOs;

public class EvacuationMappingProfile : Profile
{
    public EvacuationMappingProfile()
    {
        CreateMap<VehicleAssignment, EvacuationAssignmentResponse>()
        .ForMember(d => d.NumberOfPeople,
            opt => opt.MapFrom(s => s.AssignedPeople))
        .ForMember(d => d.ETA,
            opt => opt.MapFrom(s =>
                $"{s.EstimatedArrival} minutes"));
    }
}