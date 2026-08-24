using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SpaRelaxApp.Models;
using SpaRelaxApp.Data;
using Microsoft.AspNetCore.Authorization; // För att kräva inloggning
using System.Security.Claims; // För att hämta användarens ID

namespace SpaRelaxApp.Pages.BookingPages;

[Authorize] // Endast inloggade kan skapa bokningar
public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Booking Booking { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        // 1. Hämta ID för den användare som är inloggad just nu
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // 2. Tilldela detta ID till bokningen manuellt
        Booking.UserId = userId;

        // Vi tar bort valideringsfelet för UserId eftersom vi sätter det manuellt här uppe (annars kan ModelState tro att data saknas)
        ModelState.Remove("Booking.UserId");

        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Bookings.Add(Booking);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}