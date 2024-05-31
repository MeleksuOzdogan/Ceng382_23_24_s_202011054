using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ReservationApp.Data;
using System.Collections.Generic;
using System.Linq;
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

        public async Task OnGetAsync()
        {
            Reservations = await _context.Reservations
                .Include(r => r.Room)
                .ToListAsync();
        }
    }
}
