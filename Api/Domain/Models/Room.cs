namespace Domain.Models
{
    public class Room : BaseObject
    {
        public int NumberOfBeds { get; set; }
        public int NumberOfReservedBeds { get; set; }
        public int RoomNumber { get; set; }
        public int PricePerBed { get; set; }
        public string ImageBase64 { get; set; } = string.Empty;
        public IEnumerable<RoomReservation> RoomReservations { get; set; } = new List<RoomReservation>();

    }
}
