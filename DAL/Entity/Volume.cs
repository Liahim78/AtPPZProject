using System;

namespace DAL.Entity
{
    public class Volume
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTime Time { get; set; }
        public Event Event { get; set; }
    }
}
