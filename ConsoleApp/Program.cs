using System;

public class Program
{
    public static void Main()
    {
        // Instantiate the ReservationHandler
        var reservationHandler = new ReservationHandler();

        // Instantiate IReservationRepository
        var reservationRepository = new ReservationRepository();

        // Instantiate LogHandler
        var logHandler = new LogHandler(new FileLogger());

        // Instantiate ILogger
        var logger = new Logger(logHandler);

        // Instantiate ReservationService
        var reservationService = new ReservationService(reservationRepository, logger);

        // Wiring everything together
        var program = new Program(reservationService, reservationHandler, logger);

        // Using the classes and methods
        program.RunSample();
    }

    private readonly ReservationService _reservationService;
    private readonly ReservationHandler _reservationHandler;
    private readonly ILogger _logger;

    public Program(ReservationService reservationService, ReservationHandler reservationHandler, ILogger logger)
    {
        _reservationService = reservationService;
        _reservationHandler = reservationHandler;
        _logger = logger;
    }

    public void RunSample()
    {
        // Add Reservation
        _reservationService.AddReservation(new Reservation("John Doe", "2024-04-01", "10:30", "A-101", 3), "John Doe");

        // Display Weekly Schedule
        _reservationHandler.DisplayWeeklySchedule();

        // Delete Reservation
        _reservationService.DeleteReservation(new Reservation("John Doe", "2024-04-01", "10:30", "A-101", 3), "John Doe");

        // Display Weekly Schedule after deletion
        _reservationHandler.DisplayWeeklySchedule();
    }
}