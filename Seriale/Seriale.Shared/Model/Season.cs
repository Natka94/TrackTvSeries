using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Seriale.Model
{
    public class Season : INotifyPropertyChanged
    {
        [JsonProperty("air_date")]
        public string AirDate { get; set; }
        public int Id { get; set; }
        private string _posterPath;
        
        [JsonProperty("poster_path")]
        public string PosterPath
        {
            get { return _posterPath; }
            set
            {
                if (_posterPath == "http://image.tmdb.org/t/p/w92" + value) return;
                _posterPath = "http://image.tmdb.org/t/p/w92" + value;
                NotifyPropertyChanged("PosterPath");
            }
        }
        [JsonProperty("season_number")]
        public int SeasonNumber { get; set; }

        private bool _episodesVisible;

        public bool EpisodesVisible
        {
            get { return _episodesVisible; }
            set
            {
                _episodesVisible = value;
                NotifyPropertyChanged("EpisodesVisible");
                
            }
        }

        public ObservableCollection<Episode> Episodes { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
     

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
