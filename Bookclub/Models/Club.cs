namespace Bookclub.Models
{
    public class Club
    {
        public int Id { get; set; } 
        public string Name { get; set; } =  "";
        public string Description { get; set; } = "";
            public string JoinCode { get; set; } = "";
            public DateTime CreattedAt { get; set; } = DateTime.UtcNow;

        public string OwnerId { get; set; } = "";
        public ApplicationUser? Owner { get; set; }

        public ICollection<Membership> Memberships { get; set; } = new List<Membership>();
        public ICollection<ClubBook> ClubBooks { get; set; } = new List<ClubBook>();

    }
}
