using Bookclub.Data;
using Bookclub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookclub.Pages.Clubs
{
    [Authorize]
    public class AddBookModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public AddBookModel(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [BindProperty] public int ClubId { get; set; }
        [BindProperty] public string Title { get; set; } = "";
        [BindProperty] public string Author { get; set; } = "";
        [BindProperty] public string Status { get; set; } = "Vill läsa";
        public string Error { get; set; } = "";

        public void OnGet(int clubId)
        {
            ClubId = clubId;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Title)) { Error = "Titel krävs."; return Page(); }

            var user = await _userManager.GetUserAsync(User);
            if (user is null) return Unauthorized();

            var book = new Book
            {
                Title = Title.Trim(),
                Author = Author.Trim()
            };

            _db.Books.Add(book);

            _db.ClubBooks.Add(new ClubBook
            {
                Club = _db.Clubs.First(c => c.Id == ClubId),
                Book = book,
                Status = Status,
                AddedAt = DateTime.UtcNow
            });

            await _db.SaveChangesAsync();
            return RedirectToPage("/Clubs/Details", new { id = ClubId });
        }
    }
}