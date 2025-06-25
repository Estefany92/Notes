using Notes.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Notes.Models.WeatherModels.WeatherModels;

namespace Notes.ViewModels
{
     class WeatherViewModel : INotifyPropertyChanged
    {
        private WeatherData _weaherData;

        public WeatherData WeatherDataInfo
        {
            get => _weaherData;
            set
            {
                if (_weaherData != value) 
                { 
                    _weaherData = value;
                    OnPropertyChanged();
                }

            }
        }

        public WeatherViewModel()
        {
            _weaherData = new WeatherData();
            GetCurrentWeather();
        }

        public async Task GetCurrentWeather()
        {
            WeatherRepository weatherRepository = new WeatherRepository();
            WeatherDataInfo = await weatherRepository.GetWeatherCurrentLocationAsync();
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));





    }
}
