using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

public class ReservationRepository : IReservationRepository
{
    private List<Reservation> _reservations = new List<Reservation>();
    private string _reservationsFilePath = "ReservationData.json";

    public ReservationRepository()
    {
        LoadReservations();
    }

    public void AddReservation(Reservation reservation)
    {
        _reservations.Add(reservation);
        SaveReservations();

        // Log the action
        LogAction("AddReservation", reservation);
    }

    public void DeleteReservation(Reservation reservation)
    {
        _reservations.Remove(reservation);
        SaveReservations();

        // Log the action
        LogAction("DeleteReservation", reservation);
    }

    public List<Reservation> GetAllReservations()
    {
        return _reservations;
}

    private void LoadReservations()
    {
        if (File.Exists(_reservationsFilePath))
        {
            string json = File.ReadAllText(_reservationsFilePath);
            _reservations = JsonSerializer.Deserialize<List<Reservation>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Reservation>();
        }
        else
        {
            _reservations = new List<Reservation>();
        }
    }

    private void SaveReservations()
    {
        string json = JsonSerializer.Serialize(_reservations, new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true });
        File.WriteAllText(_reservationsFilePath, json);
    }

    private void LogAction(string action, Reservation reservation)
    {
        LogRecord logRecord = new LogRecord(
            DateTime.Now,
            reservation.ReserverName,
            reservation.Room.RoomName
        );

        string logMessage = $"{action}: {reservation}";
        Console.WriteLine(logMessage);

        // Update LogData.json
        UpdateLogData(logRecord, logMessage);
    }

    private void UpdateLogData(LogRecord logRecord, string logMessage)
    {
        List<LogRecord>? logs = null;

        if (File.Exists("LogData.json"))
        {
            string json = File.ReadAllText("LogData.json");
            logs = JsonSerializer.Deserialize<List<LogRecord>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        if (logs != null)
        {
            logs.Add(logRecord);
            string newJson = JsonSerializer.Serialize(logs, new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true });
            File.WriteAllText("LogData.json", newJson);
        }
        else
        {
            File.WriteAllText("LogData.json", "[]");
        }
    }
}