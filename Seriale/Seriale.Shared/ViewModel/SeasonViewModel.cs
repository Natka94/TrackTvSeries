using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using Seriale.Model;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace Seriale.ViewModel
{
    public class SeasonViewModel : ObservableObject
    {
        public Season CurrentSeason { get; set; }// może fasada?
        public TvSeries CurrentTvSeries { get; set; }
        private int _numberOfSeason;
        public async Task Initialize(int numberOfSeason, TvSeries currentTvSeries)
        {
            _numberOfSeason = numberOfSeason;
            CurrentTvSeries = currentTvSeries;
           await getInfoAsync();

        }

        private async Task getInfoAsync()
        {
            var urlBase = String.Format("https://api.themoviedb.org/3/tv/{0}/season/{1}?api_key=6ddd8a671123ed37164c64d1c8b33a0c", CurrentTvSeries.Id,_numberOfSeason);


            var client = new HttpClient();
            var json = await client.GetStringAsync(urlBase);
            CurrentSeason=CurrentTvSeries.Seasons.FirstOrDefault(x=>x.SeasonNumber==_numberOfSeason);
            CurrentSeason = JsonConvert.DeserializeObject<Season>(json);
        }

    }
}
