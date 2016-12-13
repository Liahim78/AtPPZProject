using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtPPZProject.Models;

namespace DAL.Entity
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public string Info { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<Volume> Volumes { get; set; }
    }
}
