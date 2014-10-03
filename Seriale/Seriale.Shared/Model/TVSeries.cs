using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;

namespace Seriale.Model
{
    public class CreatedBy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }
    }

    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Network
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    
    public class TvSeries : ObservableObject
    {
        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }
        public int Id { get; set; }
        [JsonProperty("orginal_name")]
        public string OriginalName { get; set; }
         [JsonProperty("first_air_date")]
        public string FirstAirDate { get; set; }
       
        private string _posterPath;
        [JsonProperty("poster_path")]
        public string PosterPath
        {
            get { return _posterPath; }
            set {
                if (_posterPath == "http://image.tmdb.org/t/p/w92" + value) return;
                _posterPath = "http://image.tmdb.org/t/p/w92" + value;
                
            }
        }
        
        
        public double Popularity { get; set; }
        public string Name { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
        public List<CreatedBy> created_by { get; set; }
         [JsonProperty("episode_run_time")]
        public ObservableCollection<int> EpisodeRunTime { get; set; }
       
        public List<Genre> Genres { get; set; }
        public string Homepage { get; set; }
        
        public bool in_production { get; set; }
        public List<string> Languages { get; set; }
        public string last_air_date { get; set; }
      
        public List<Network> networks { get; set; }
        [JsonProperty("number_of_episodes")]
        public int NumberOfEpisodes { get; set; }
        [JsonProperty("number_of_seasons")]
        public int NumberOfSeasons { get; set; }
        public string Title { get; set; }
        public List<string> origin_country { get; set; }
        public string Overview { get; set; }
        public ObservableCollection<Season> Seasons { get; set; }
        public string Status { get; set; }
        public bool Adult { get; set; }
        public string original_title { get; set; }
        public string release_date { get; set; }
       
    }
}
