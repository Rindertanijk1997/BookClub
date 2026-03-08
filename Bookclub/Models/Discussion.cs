namespace Bookclub.Models
{
    public class Discussion
    {
        public int Id { get; set; }
        public int ClubBookId { get; set; }
        public ClubBook? ClubBook { get; set; }
        public string UserId { get; set; } = "";
        public ApplicationUser? User { get; set; }
        public string Text { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
