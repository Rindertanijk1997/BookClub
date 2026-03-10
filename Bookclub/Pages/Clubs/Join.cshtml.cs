using Bookclub.Data;
using Bookclub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookclub.Pages.Clubs
{
    [Authorize]
    public class JoinModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public JoinModel(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [BindProperty]
        public string JoinCode { get; set; } = "";
        public string Error { get; set; } = "";

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null) return Unauthorized();

            var club = _db.Clubs.FirstOrDefault(c => c.JoinCode == JoinCode.ToUpper().Trim());
            if (club is null) { Error = "Ingen klubb hittades med den koden."; return Page(); }

            var exists = _db.Memberships.Any(m => m.UserId == user.Id && m.ClubId == club.Id);
            if (exists) { Error = "Du är redan med i den här klubben."; return Page(); }

            _db.Memberships.Add(new Membership
            {
                UserId = user.Id,
                ClubId = club.Id,
                JoinedAt = DateTime.UtcNow
            });

            await _db.SaveChangesAsync();
            return RedirectToPage("/Clubs/Index");
        }
    }
}