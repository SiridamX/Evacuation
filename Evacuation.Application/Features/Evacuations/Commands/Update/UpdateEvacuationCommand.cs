using MediatR;

public class UpdateEvacuationCommand
    : IRequest<Unit>
{
    public required string ZoneId { get; set; }
    public required string VehicleId { get; set; }
    public int PeopleMoved { get; set; }
}