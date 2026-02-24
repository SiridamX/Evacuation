using MediatR;
using Evacuation.Domain.Interfaces;

namespace Evacuation.Application.Features.Evacuation.Reset;

public class ResetEvacuationCommandHandler
    : IRequestHandler<ResetEvacuationCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public ResetEvacuationCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(ResetEvacuationCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Assignments.ClearAsync(cancellationToken);
        await _unitOfWork.Zones.ResetAllAsync(cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}