using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ReservationApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyApp.Namespace
{
    public class ReservationsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ReservationsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reservation NewReservation { get; set; }
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
        public List<Room> Rooms { get; set; } = new List<Room>();

        public async Task OnGetAsync()
        {
            Rooms = await _context.Rooms.ToListAsync();
            Reservations = await _context.Reservations
                .Include(r => r.Room)
                .ToListAsync();

            NewReservation = new Reservation
            {
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddHours(1),
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // if (!ModelState.IsValid)
            // {
            //     return RedirectToAction("GET");
            // }

            NewReservation.ReserverId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _context.Reservations.Add(NewReservation);
            await _context.SaveChangesAsync();

            return RedirectToAction("GET");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
