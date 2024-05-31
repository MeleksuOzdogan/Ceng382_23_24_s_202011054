using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReservationApp.Data;

namespace MyApp.Namespace
{
    public class ViewRoomsModel : PageModel
    {
        private readonly ApplicationDbContext DbContext;
        public ViewRoomsModel(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public IList<Room> Rooms { get; set; } = new List<Room>();
        public void OnGet()
        {
            Rooms = DbContext.Rooms.ToList();
        }
        public IActionResult OnPostDelete(int id)
        {
            var room = DbContext.Rooms.FirstOrDefault(r => r.RoomId == id);
            if (room == null)
            {
                return NotFound();
            }

            DbContext.Rooms.Remove(room);
            DbContext.SaveChanges();

            ViewData["Message"] = "Room deleted successfully.";
            return RedirectToPage("/ViewRooms");
        }

    }
}
