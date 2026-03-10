using Bookclub.Data;
using Bookclub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bookclub.Pages.Clubs
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public DetailsModel(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public Club? Club { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null) return Unauthorized();

            var isMember = await _db.Memberships
                .AnyAsync(m => m.UserId == user.Id && m.ClubId == id);

            if (!isMember) return Forbid();

            Club = await _db.Clubs
                .Include(c => c.ClubBooks)
                    .ThenInclude(cb => cb.Book)
                .FirstOrDefaultAsync(c => c.Id == id);

            return Page();
        }
    }
}