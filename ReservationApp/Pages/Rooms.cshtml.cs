using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReservationApp.Data;

namespace MyApp.Namespace
{
    [Authorize]
    public class RoomsModel : PageModel
    {
        private readonly ApplicationDbContext DbContext;

        public RoomsModel(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        [BindProperty]
        public Room NewRoom { get; set; } = new Room(){ RoomId = "SomeUniqueValue" };

        public IList<Room> Rooms { get; set; } = new List<Room>();
        public string Message { get; set; }

        public void OnGet()
        {
            Rooms = DbContext.Rooms.ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            DbContext.Rooms.Add(NewRoom);
            DbContext.SaveChanges();
            return RedirectToPage("/ViewRooms");
        }

        public IActionResult OnPostDelete(string roomId)
        {
            var room = DbContext.Rooms.FirstOrDefault(r => r.RoomId == roomId);
            if (room == null)
            {
                return NotFound();
            }

            DbContext.Rooms.Remove(room);
            DbContext.SaveChanges();
            return RedirectToPage("/ViewRooms");
        }
    }
}
