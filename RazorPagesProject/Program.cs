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
    public string? roomId { get; set; }

    [JsonPropertyName("roomName")]
    public string? roomName { get; set; }

    [JsonPropertyName("capacity")]
    public string? capacity { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        string jsonFilePath = "Data.json";
        string jsonString = File.ReadAllText(jsonFilePath);

        var options = new JsonSerializerOptions()
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString |
            JsonNumberHandling.WriteAsString
        };

        var roomData = JsonSerializer.Deserialize<RoomData>(jsonString, options);

        if (roomData?.Rooms != null)
        {
            foreach (var room in roomData.Rooms)
            {
                Console.WriteLine($"Room ID: {room.roomId}, Name: {room.roomName}");
            }
        }
    }
}
