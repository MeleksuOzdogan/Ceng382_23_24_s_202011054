using System.Security.Claims;
using ReservationApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    public class RoomsModel : PageModel
    {
        public ApplicationDbContext DbContext { get; set; } = default!;
        [BindProperty]
        public Room NewRoom { get; set; } = default!;
        public List<Room> Rooms { get; set; } = new List<Room>();
        public void OnGet()
        {
            Rooms = (from item in DbContext.Rooms 
                            select item).ToList();
        }

        public IActionResult OnPost()
        {
            if (NewRoom == null)
            {
                return Page();
            }
            
            //var roomID = Room.FindFirstValue(ClaimTypes.NameIdentifier);
            
            //NewRoom.RoomId = RoomId;
            DbContext.Rooms.Add(NewRoom);
            DbContext.SaveChanges();
            return RedirectToAction("Get");
        }
    }
}
