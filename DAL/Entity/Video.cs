using System;
using System.Collections.Generic;
using DAL.Context;

namespace DAL.Entity
{
    public class Video
    {
        public int Id { get; set; }
        public string UrlVideoContent { get; set; }
        public bool IsPublic { get; set; }
        public bool IsNew { get; set; }
        public string Discription { get; set; }
        public DateTime DateTime { get; set; }
        public int? MainId { get; set; }

        public virtual ApplicationUser MyUser { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
