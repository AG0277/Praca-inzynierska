namespace Domain.Models
{
    public class RoomReservation
    {
        public int ReservationId { get; set; }
        public int AppUserId { get; set; }
        public int RoomId { get; set; }
        public int NumberOfBeds { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int AmountPaid { get; set; }
        public int TotalPrice { get; set; }
        public required AppUser AppUser { get; set; }
        public required Room Room { get; set; }
    }
}
