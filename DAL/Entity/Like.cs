using DAL.Context;

namespace DAL.Entity
{
    public class Like
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public Video Video { get; set; }
    }
}
