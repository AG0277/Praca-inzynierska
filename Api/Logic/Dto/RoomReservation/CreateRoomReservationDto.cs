namespace Logic.Dto.RoomReservationDto
{
    public class CreateRoomReservationDto
    {
        public int AppUserId { get; set; }
        public int RoomId { get; set; }
        public int NumberOfBeds { get; set; }
        public int TotalPrice { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
