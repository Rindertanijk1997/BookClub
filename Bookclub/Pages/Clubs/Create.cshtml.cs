using Bookclub.Data;
using Bookclub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookclub.Pages.Clubs
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [BindProperty]
        public Club Club { get; set; } = new();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = await _userManager.GetUserAsync(User);
            if (user is null) return Unauthorized();

            Club.OwnerId = user.Id;
            Club.JoinCode = Guid.NewGuid().ToString("N")[..8].ToUpper();
            Club.CreatedAt = DateTime.UtcNow;

            _db.Clubs.Add(Club);

            _db.Memberships.Add(new Membership
            {
                UserId = user.Id,
                Club = Club,
                JoinedAt = DateTime.UtcNow
            });

            await _db.SaveChangesAsync();
            return RedirectToPage("/Clubs/Index");
        }
    }
}