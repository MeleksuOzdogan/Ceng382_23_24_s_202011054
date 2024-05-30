public class Reservation
{
    public string ReservationId { get; set; }
    public DateTime DateTime { get; set; }
    public string ReserverName { get; set; }

    public string RoomId { get; set; }
    public Room Room { get; set; }
}