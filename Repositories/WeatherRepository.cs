using Microsoft.Maui.Devices.Sensors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Notes.Models.WeatherModels.WeatherModels;

namespace Notes.Repositories
{
    class WeatherRepository
    {
        //ubicacion actual
    
        public async Task<WeatherData> GetWeatherCurrentLocationAsync()
        {
            GeolocationRepository geolocationRepository = new GeolocationRepository();
            var location = await geolocationRepository.GetCurrentLocation();
            return await GetWeatherDataAsync(location.Latitude, location.Longitude);
        }
       



        public async Task<WeatherData> GetWeatherDataAsync(double latitude, double longitude)
        {
            //consumir el API
            HttpClient httpClient = new HttpClient();
            string url = "https://api.open-meteo.com/v1/forecast?latitude="+latitude+"&longitude="+longitude+"&current=temperature_2m,relative_humidity_2m,rain";

            var response = await httpClient.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            WeatherData data = JsonConvert.DeserializeObject<WeatherData>(result);
            return data;
        }
    }
}   
