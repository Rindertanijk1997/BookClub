namespace Bookclub.Models
{
    public class Membership
    {
        public int Id { get; set; }
        public string UserId { get; set; } = "";
        public ApplicationUser? User { get; set; }
        public int ClubId { get; set; }
        public Club? Club { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}
