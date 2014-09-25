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

    public class Season
    {
        [JsonProperty("air_date")]
        public string AirDate { get; set; }
        public int Id { get; set; }
        public string poster_path { get; set; }
        [JsonProperty("season_number")]
        public int SeasonNumber { get; set; }
    }
    public class TVSeries : INotifyPropertyChanged
    {
        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }
        public int Id { get; set; }
        [JsonProperty("orginal_name")]
        public string OriginalName { get; set; }
        public string First_air_date { get; set; }
       
        private string _posterPath;
        [JsonProperty("poster_path")]
        public string PosterPath
        {
            get { return _posterPath; }
            set {
                if (_posterPath != "http://image.tmdb.org/t/p/w92" + value)
                {
                    _posterPath = "http://image.tmdb.org/t/p/w92" + value;
                        NotifyPropertyChanged("PosterPath");
                }
    
            
            
            }
        }
        
        
        public double Popularity { get; set; }
        public string Name { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
        public List<CreatedBy> created_by { get; set; }
        public List<int> episode_run_time { get; set; }
       
        public List<Genre> genres { get; set; }
        public string homepage { get; set; }
        
        public bool in_production { get; set; }
        public List<string> languages { get; set; }
        public string last_air_date { get; set; }
      
        public List<Network> networks { get; set; }
        [JsonProperty("number_of_episodes")]
        public int NumberOfEpisodes { get; set; }
        [JsonProperty("number_of_seasons")]
        public int NumberOfSeasons { get; set; }
        public string Title { get; set; }
        public List<string> origin_country { get; set; }
        public string overview { get; set; }
        public List<Season> seasons { get; set; }
        public string status { get; set; }
        public bool Adult { get; set; }
        public string original_title { get; set; }
        public string release_date { get; set; }
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
