public class Reservation
{
    public string ReservationId { get; set; }
    public DateTime DateTime { get; set; }

    public string ReserverId { get; set; }

    public string RoomId { get; set; }
    public Room Room { get; set; }


    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
}