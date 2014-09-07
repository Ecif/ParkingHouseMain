
namespace Entities.Main.ParkingHouse
{
    /// <summary>
    /// Parking space entity. Holds client reference.
    /// </summary>
    public class ParkingSpace
    {
        public int Id { get; set; }
        public int? ClientId { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public bool IsPremium { get; set; }
    }
}
