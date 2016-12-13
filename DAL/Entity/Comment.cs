using DAL.Context;

namespace DAL.Entity
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public ApplicationUser User { get; set; }
        public Video Video { get; set; }
    }
}
