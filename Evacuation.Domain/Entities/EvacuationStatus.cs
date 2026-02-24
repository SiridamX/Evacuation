
using System.ComponentModel.DataAnnotations;

namespace Evacuation.Domain.Entities
{
    public class EvacuationStatus
    {
        [Key]
        public int Id { get; set; }
        public required string ZoneId { get; set; }
        public int TotalEvacuated { get; set; }
        public int RemainingPeople { get; set; }
        public string? LastVehicleUsed { get; set; }
    }
}
