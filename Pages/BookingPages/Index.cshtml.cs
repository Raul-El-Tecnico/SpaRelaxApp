using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SpaRelaxApp.Models;
using SpaRelaxApp.Data;
using Microsoft.AspNetCore.Authorization; // För att kräva inloggning
using System.Security.Claims; // För att kunna hämta den inloggades ID

namespace SpaRelaxApp.Pages.BookingPages;

[Authorize] // Endast inloggade användare kommer åt denna sida
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Booking> Booking { get; set; } = default!;

    public async Task OnGetAsync()
    {
        // Hämtar ID på den användare som är inloggad just nu
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // Hämtar endast de bokningar som tillhör den inloggade användaren
        Booking = await _context.Bookings
            .Where(b => b.UserId == userId)
            .ToListAsync();
    }
}
