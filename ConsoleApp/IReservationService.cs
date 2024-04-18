using System.Collections.Generic;

public interface IReservationService
{
    void AddReservation(Reservation reservation, string reserverName);
    void DeleteReservation(Reservation reservation);
    void DisplayWeeklySchedule();
    List<Reservation> GetAllReservations();
    List<Room> GetRooms();
}