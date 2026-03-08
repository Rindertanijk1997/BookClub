namespace Bookclub.Models
{
    public class ClubBook
    {
        public int Id { get; set; }
        public int ClubId { get; set; }
        public Club? Club { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public string Status { get; set; } = "Vill läsa";
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;


        public ICollection<Discussion> Discussions { get; set; } = new List<Discussion>();
    }
}
