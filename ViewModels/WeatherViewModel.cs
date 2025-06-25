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
                    OnPropertyChanged(nameof(RainIcon));
                    OnPropertyChanged(nameof(TemperaturaIcon));
                    OnPropertyChanged(nameof(HumedadSize));
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



        //Logica para que lo Icons cambien de acuerdo a los datos en la pagina del tab 
        public string RainIcon
        {
            get
            {
                if (WeatherDataInfo?.current?.rain > 0)
                    return "lluvia.jpg";
                else
                    return "nubesinlluvia.jpg";
            }
        }

        public string TemperaturaIcon
        {
            get
            {
                var t = WeatherDataInfo?.current?.temperature_2m ?? 0;
                if (t < 20)
                    return "frio.jpg";
                else if (t <= 30)
                    return "viento.jpg";
                else
                    return "calor.jpg";
            }
        }

        public double HumedadSize
        {
            get
            {
                var h = WeatherDataInfo?.current?.relative_humidity_2m ?? 0;
                return 30 + h * 0.4; // Crece con humedad
            }
        }






    }
}
