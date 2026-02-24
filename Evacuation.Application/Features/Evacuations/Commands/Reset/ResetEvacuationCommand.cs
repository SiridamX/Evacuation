using MediatR;
namespace Evacuation.Application.Features.Evacuation.Reset;
public record ResetEvacuationCommand : IRequest<Unit>;