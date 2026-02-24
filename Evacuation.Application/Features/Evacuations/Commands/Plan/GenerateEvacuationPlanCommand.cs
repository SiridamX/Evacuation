using MediatR;
using Evacuation.Application.Features.Evacuation.Commands.GeneratePlan.DTOs;

namespace Evacuation.Application.Features.Evacuation.Commands.GeneratePlan;

public record GenerateEvacuationPlanCommand() : IRequest<List<EvacuationAssignmentResponse>>;