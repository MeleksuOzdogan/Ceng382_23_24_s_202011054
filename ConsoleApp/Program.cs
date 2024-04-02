using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public class RoomData
{
    [JsonPropertyName("Room")]
    public Room[]? Rooms { get; set; }
}

public class Room
{
    [JsonPropertyName("roomId")]
    public string? RoomId { get; set; }

    [JsonPropertyName("roomName")]
    public string? RoomName { get; set; }

    [JsonPropertyName("capacity")]
    public int Capacity { get; set; }
}

public class Reservation
{
    public DateTime Date { get; set; }
    public DateTime Time { get; set; }
    public string ReserverName { get; set; }
    public Room Room { get; set; }

    public Reservation(string reserverName, DateTime date, DateTime time, Room room)
    {
        ReserverName = reserverName;
        Date = date.Date;
        Time = time;
        Room = room;
    }
}

public class ReservationHandler
{
    private readonly Reservation?[,] _reservations;

    public ReservationHandler()
    {
        _reservations = new Reservation?[7, 24];
    }

    public void AddReservation(string reserverName, DateTime date, DateTime time, string roomId)
    {
        int dayOfWeek = (int)date.DayOfWeek;
        int hourOfDay = time.Hour;

        if (_reservations[dayOfWeek, hourOfDay] != null)
        {
            Console.WriteLine($"Seçilen zaman ({time.ToString("HH:mm")}) için rezervasyon yapılamadı, odası dolu!");
            return;
        }

        Room selectedRoom = new Room() { RoomId = roomId }; // Geçici çözüm, oda bilgilerini JSON'dan alma
        Reservation newReservation = new Reservation(reserverName, date, time, selectedRoom);
        _reservations[dayOfWeek, hourOfDay] = newReservation;

        Console.WriteLine($"{reserverName} için {date.ToShortDateString()} tarihinde {time.ToString("HH:mm")} saatinde {selectedRoom.RoomName} ({selectedRoom.RoomId}) odasında rezervasyon başarıyla oluşturuldu!");
    }

    public void DeleteReservation(string reserverName, DateTime date, DateTime time)
    {
        int dayOfWeek = (int)date.DayOfWeek;
        int hourOfDay = time.Hour;

        if (_reservations[dayOfWeek, hourOfDay] == null || _reservations[dayOfWeek, hourOfDay]?.ReserverName != reserverName)
        {
            Console.WriteLine($"Seçilen rezervasyon ({reserverName}, {date.ToShortDateString()}, {time.ToString("HH:mm")}) bulunamadı!");
            return;
        }

        _reservations[dayOfWeek, hourOfDay] = null;
        Console.WriteLine($"{reserverName} için {date.ToShortDateString()} tarihinde {time.ToString("HH:mm")} saatinde rezervasyon başarıyla silindi!");
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

class Program
{
    static void Main(string[] args)
    {
        string jsonFilePath = "Data.json";

        try
        {
            string jsonString = File.ReadAllText(jsonFilePath);

            var options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString |
                JsonNumberHandling.WriteAsString
            };

            var roomData = JsonSerializer.Deserialize<RoomData>(jsonString, options);

            if (roomData?.Rooms == null)
            {
                Console.WriteLine("Room data is null or empty.");
                return;
            }

            ReservationHandler handler = new ReservationHandler();

            // Rezervasyon ekleme
            handler.AddReservation("Ayşe", DateTime.Parse("2024-03-27 10:00"), DateTime.Parse("2024-03-27 10:00"), "001");
            handler.AddReservation("Mehmet", DateTime.Parse("2024-03-27 14:00"), DateTime.Parse("2024-03-27 14:00"), "002");

            // Haftalık programı gösterme
            handler.DisplayWeeklySchedule();

            // Rezervasyon silme
            handler.DeleteReservation("Ayşe", DateTime.Parse("2024-03-27 10:00"), DateTime.Parse("2024-03-27 10:00"));

            // Haftalık programın güncel durumunu gösterme
            handler.DisplayWeeklySchedule();
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"File not found: {jsonFilePath}");
        }
        catch (JsonException)
        {
            Console.WriteLine("Error deserializing JSON.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}