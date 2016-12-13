using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtPPZProject.Models;

namespace DAL.Entity
{
    public class Like
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public Video Video { get; set; }
    }
}
