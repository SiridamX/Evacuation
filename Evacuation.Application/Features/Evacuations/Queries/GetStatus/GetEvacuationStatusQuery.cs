using MediatR;
using Evacuation.Application.Features.Evacuation.Queries.GetStatus.DTOs;

namespace Evacuation.Application.Features.Evacuation.Queries.GetStatus;

public class GetEvacuationStatusQuery : IRequest<List<EvacuationStatusResponse>>{}