using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1.Models
{
    public class reviewDTO
    {
        public int id {  get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int rating { get; set; }
        public string rvt { get; set; }
        public DateTime rvd { get; set; }
    }
}
