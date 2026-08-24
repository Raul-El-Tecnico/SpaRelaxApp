using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SpaRelaxApp.Models;
using SpaRelaxApp.Data;

namespace SpaRelaxApp.Pages.BookingPages;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;
    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Booking Booking { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var booking = await _context.Bookings.FirstOrDefaultAsync(m => m.Id == id);
        if (booking is null)
        {
            return NotFound();
        }
        else
        {
            Booking = booking;
        }

        return Page();
    }
}
