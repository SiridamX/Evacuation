namespace Evacuation.Application.Features.Evacuation.Commands.GeneratePlan.DTOs;

public class EvacuationPlanResponse
{
    public List<EvacuationAssignmentResponse> Assignments { get; set; } = new();
}