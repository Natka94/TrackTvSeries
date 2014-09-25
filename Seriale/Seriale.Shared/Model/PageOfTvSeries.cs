using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Seriale.Model
{
    
        public class PageOfTvSeries : ObservableObject
        {
            public int Page { get; set; }
            [JsonProperty(PropertyName = "results")]
            public ObservableCollection<TVSeries> List { get; set; }
            [JsonProperty(PropertyName = "total_pages")]
            public int TotalPages { get; set; }
            [JsonProperty(PropertyName = "total_results")]
            public int TotalResults { get; set; }
      


            public event PropertyChangedEventHandler PropertyChanged;
            protected void NotifyPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        
    }
}
