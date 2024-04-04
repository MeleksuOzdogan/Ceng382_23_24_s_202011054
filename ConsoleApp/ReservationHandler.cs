using System;

public class ReservationHandler
{
    private readonly Reservation[,] _reservations;

    public ReservationHandler()
    {
        _reservations = new Reservation[7, 24];
    }

    public void AddReservation(Reservation reservation, string reserverName)
    {
        int dayOfWeek = (int)reservation.Date.DayOfWeek;
        int hourOfDay = reservation.Time.Hour;

        if (_reservations[dayOfWeek, hourOfDay] != null)
        {
            Console.WriteLine($"Seçilen zaman ({reservation.Time.ToString("HH:mm")}) için rezervasyon yapılamadı, odası dolu!");
            return;
        }

        Reservation newReservation = reservation with { ReserverName = reserverName };
        _reservations[dayOfWeek, hourOfDay] = newReservation;

        Console.WriteLine($"{reserverName} için {reservation.Date.ToShortDateString()} tarihinde {reservation.Time.ToString("HH:mm")} saatinde {newReservation.Room.RoomName} ({newReservation.Room.RoomId}) odasında rezervasyon başarıyla oluşturuldu!");
    }

    public void DeleteReservation(Reservation reservation, string reserverName)
    {
        int dayOfWeek = (int)reservation.Date.DayOfWeek;
        int hourOfDay = reservation.Time.Hour;

        if (_reservations[dayOfWeek, hourOfDay] == null || _reservations[dayOfWeek, hourOfDay]?.ReserverName != reserverName || _reservations[dayOfWeek, hourOfDay]?.Room != reservation.Room)
        {
            Console.WriteLine($"Seçilen rezervasyon ({reserverName}, {reservation.Date.ToShortDateString()}, {reservation.Time.ToString("HH:mm")}) bulunamadı!");
            return;
        }

        _reservations[dayOfWeek, hourOfDay] = null;
        Console.WriteLine($"{reserverName} için {reservation.Date.ToShortDateString()} tarihinde {reservation.Time.ToString("HH:mm")} saatinde rezervasyon başarıyla silindi!");
    }

    public void DisplayWeeklySchedule()
    {
        Console.WriteLine("Time   │   MON    │   TUE    │   WED    │   THU    │   FRI    │   SAT    │   SUN    │");
        Console.WriteLine("───────┼──────────┼──────────┼──────────┼──────────┼──────────┼──────────┼──────────┤");

        for (int hourOfDay = 0; hourOfDay < 24; hourOfDay++)
        {
            Console.Write($"{hourOfDay:00}:00  │");

            for (int dayOfWeek = 0; dayOfWeek < 7; dayOfWeek++)
            {
                if (_reservations[dayOfWeek, hourOfDay] != null)
                {
                    string? reserverName = _reservations[dayOfWeek, hourOfDay]?.ReserverName;
                    Console.Write($"  {reserverName,-6}  │");
                }
                else
                {
                    Console.Write("          │"); // Boş hücre için padding
                }
            }

            Console.WriteLine();
            if (hourOfDay < 23)
            {
                Console.WriteLine("───────┼──────────┼──────────┼──────────┼──────────┼──────────┼──────────┼──────────┤");
            }
        }
    }
}