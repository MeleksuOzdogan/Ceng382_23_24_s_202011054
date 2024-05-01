using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

public class Program
{
    public static void Main(string[] args)
    {
        ReservationRepository reservationRepository = new ReservationRepository();

        // Perform add and delete operations here
        // ...

        // Output reservations to a different JSON file
        List<Reservation> reservations = reservationRepository.GetAllReservations();
        string reservationsJson = JsonSerializer.Serialize(reservations, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("ReservationsOutput.json", reservationsJson);

        // Output logs to a different JSON file
        string logsJson = File.ReadAllText("LogData.json");
        File.WriteAllText("LogsOutput.json", logsJson);
    }
}