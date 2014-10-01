using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seriale.Model
{
    public class Episode
    {
        public string air_date { get; set; }
        public int Episode_number { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public string still_path { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
    }
}
