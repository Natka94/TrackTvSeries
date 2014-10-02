using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Seriale.Model
{
    public class Episode: ObservableObject
    {
        [JsonProperty("air_date")]
        public string AirDate { get; set; }
        [JsonProperty("episode_number")]
        public int EpisodeNumber { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public string still_path { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
    }
}
