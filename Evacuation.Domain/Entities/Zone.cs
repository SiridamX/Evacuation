using Evacuation.Domain.Common;

namespace Evacuation.Domain.Entities
{
    public class Zone : BaseEntity
    {
        public string ZoneId { get; set; } = default!;

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public int NumberOfPeople { get; set; }
        public int EvacuatedPeople { get; set; }

        public int UrgencyLevel { get; set; }
        public string? LastVehicleUsed { get;  set; }

        public int RemainingPeople => NumberOfPeople - EvacuatedPeople;

        //nav
        public ICollection<VehicleAssignment> Assignments { get;  set; } = [];

        private Zone() { }

        public Zone(
            string zoneId,
            double latitude,
            double longitude,
            int totalPeople,
            int urgencyLevel)
        {
            ZoneId = zoneId;
            Latitude = latitude;
            Longitude = longitude;
            NumberOfPeople = totalPeople;
            UrgencyLevel = urgencyLevel;
        }

        public void Evacuate(int count, string vehicleId)
        { 
            LastVehicleUsed = vehicleId;
            EvacuatedPeople += count;
            SetUpdated();
        }

        public void Reset()
        {
            EvacuatedPeople = 0;
            LastVehicleUsed = null;
            SetUpdated();
        }
    }

}
