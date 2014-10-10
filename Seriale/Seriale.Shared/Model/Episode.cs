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
        public DateTime AirDate { get; set; }
        [JsonProperty("episode_number")]
        public int EpisodeNumber { get; set; }
       
        public int SeasonNumber { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        private string _stillPath;
        [JsonProperty("still_path")] 
        public string StillPath
        {
            get { return _stillPath; }
            set
            {
                if (_stillPath == "http://image.tmdb.org/t/p/w92" + value) return;
                _stillPath = "http://image.tmdb.org/t/p/w92" + value;

            }
        }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
        public object production_code { get; set; }
        
        
    }
}
