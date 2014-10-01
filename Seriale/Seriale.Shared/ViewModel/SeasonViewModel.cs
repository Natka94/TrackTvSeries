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
        private int _idOfSeason;
        public async Task Initialize(int idOfSeason, TvSeries currentTvSeries)
        {
            _idOfSeason = idOfSeason;
            CurrentTvSeries = currentTvSeries;
           await getInfoAsync();

        }

        private async Task getInfoAsync()
        {
            var urlBase = String.Format("https://api.themoviedb.org/3/tv/{0}/season/{1}?api_key=6ddd8a671123ed37164c64d1c8b33a0c", CurrentTvSeries.Id,_idOfSeason);


            var client = new HttpClient();
            var json = await client.GetStringAsync(urlBase);
            CurrentSeason=CurrentTvSeries.Seasons.First(x=>x.Id==_idOfSeason);
            CurrentSeason = JsonConvert.DeserializeObject<Season>(json);
        }

    }
}
