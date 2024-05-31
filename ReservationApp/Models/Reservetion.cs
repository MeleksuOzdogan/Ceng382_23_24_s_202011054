public class Reservation
{
    public int ReservationId { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }

    public string ReserverId { get; set; }

    public int RoomId { get; set; }
    public Room Room { get; set; }
}