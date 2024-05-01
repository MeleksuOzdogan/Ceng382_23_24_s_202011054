using System;
using System.IO;
using System.Text.Json;

public class FileLogger : ILogger
{
    private readonly string _logFilePath;

    public FileLogger(string logFilePath)
    {
        _logFilePath = logFilePath;
    }

    public void Log(LogRecord log)
    {
        try
        {
            // Create a JSON string from the LogRecord
            string jsonLog = JsonSerializer.Serialize(log);

            // Append the log record to the file
            using (StreamWriter writer = File.AppendText(_logFilePath))
            {
                writer.WriteLine(jsonLog);
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during logging
            Console.WriteLine($"Error logging to file: {ex.Message}");
        }
    }
}