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
    public class BookDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookDetailsModel(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public ClubBook? ClubBook { get; set; }
        [BindProperty] public int ClubBookId { get; set; }
        [BindProperty] public string NewComment { get; set; } = "";

        public async Task OnGetAsync(int id)
        {
            ClubBookId = id;
            ClubBook = await _db.ClubBooks
                .Include(cb => cb.Book)
                .Include(cb => cb.Discussions)
                    .ThenInclude(d => d.User)
                .FirstOrDefaultAsync(cb => cb.Id == id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null) return Unauthorized();

            if (!string.IsNullOrWhiteSpace(NewComment))
            {
                _db.Discussions.Add(new Discussion
                {
                    Text = NewComment.Trim(),
                    UserId = user.Id,
                    ClubBookId = ClubBookId,
                    CreatedAt = DateTime.UtcNow
                });

                await _db.SaveChangesAsync();
            }

            return RedirectToPage(new { id = ClubBookId });
        }
    }
}