using Newtonsoft.Json;
using Seriale.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Seriale.Model;
using GalaSoft.MvvmLight.Command;

namespace Seriale.ViewModel
{
    public class DetailsViewModel : INotifyPropertyChanged
    {
        public TvSeries CurrentTvSeries { get; set; }

        readonly INavigationService _navigationService;
        public RelayCommand GoBackCommand { get; set; }
        public async Task Initialize(int id)
        {
           CurrentTvSeries.Id = id;
            await getInfoAsync();

        }
        public DetailsViewModel(INavigationService navigationService){
            _navigationService = navigationService;
            CurrentTvSeries = new TvSeries();
            GoBackCommand = new RelayCommand(GoBack);
            
        }

        public void GoBack()
        {
            _navigationService.GoBack();
        }
        private async Task getInfoAsync()
        {
            var urlBase = String.Format("https://api.themoviedb.org/3/tv/{0}?api_key=6ddd8a671123ed37164c64d1c8b33a0c", CurrentTvSeries.Id);
            
                
                var client = new HttpClient();
                var json = await client.GetStringAsync(urlBase);
                CurrentTvSeries = JsonConvert.DeserializeObject<TvSeries>(json);
          
            

     

        }
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
