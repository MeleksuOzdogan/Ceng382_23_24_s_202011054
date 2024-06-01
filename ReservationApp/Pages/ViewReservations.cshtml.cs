using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ReservationApp.Data;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyApp.Namespace
{
    public class ViewReservationsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ViewReservationsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Reservation> Reservations { get; set; }
        public List<Room> Rooms {get; set;} = new List<Room>();

        public async Task OnGetAsync()
        {
            Reservations = await _context.Reservations.ToListAsync();
            foreach(Reservation r in Reservations){
                r.Room = await _context.Rooms.FirstOrDefaultAsync(room => room.RoomId == r.RoomId);
            }
            
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

            Log log = new(){
                operation = "DELETE",
                userName = User.FindFirstValue(ClaimTypes.NameIdentifier),
                reservationID = id,
                operationDate = DateTime.Now
            };
            _context.Logs.Add(log);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
