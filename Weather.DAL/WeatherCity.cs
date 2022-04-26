using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Weather.Model;

namespace Weather.DAL
{
    public class WeatherCity
    {
        string fileTabelOneWeather = @"C:\Users\ZION\Desktop\TzioNet\TzionProjectWeather\Weather.DAL\bin\DataDictionryTabel\OneWeather.txt";

        string fileTabelAll = @"C:\Users\ZION\Desktop\TzioNet\TzionProjectWeather\Weather.DAL\bin\DataDictionryTabel\AlleWeather.txt";
       
        public async Task<WeatherR> GetCityData(string nameCity)
        {
            WeatherR weather;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.weatherapi.com/");

                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage pespones = await client.GetAsync($@"v1/current.json?key=12bac74e2e2743d6961184414221304&q={nameCity}& aqi=no");

                string lines = await pespones.Content.ReadAsStringAsync();

                weather = JsonSerializer.Deserialize<WeatherR>(lines);

                return weather;

            }

        }
        public async Task<String> AccessTheServerAndFetchData(string nameCity)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.weatherapi.com/");
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage pespones = await client.GetAsync($@"v1/current.json?key=12bac74e2e2743d6961184414221304&q={nameCity}& aqi=no");

                string lines = await pespones.Content.ReadAsStringAsync();

                return lines;
            }
        }


        //public async Task<Dictionary<string, WeatherR>> LoadWeatherTable(string nameCity)
        //{
        //    Dictionary<string, WeatherR> tbl = new Dictionary<string, WeatherR>();
        //    var arr = await GetCityData(nameCity);

        //    tbl.Add(arr.location.name, arr);

        //    return tbl;
        //}

        public async Task<WeatherR> LoadWeatherTable(string nameCity)
        {
            var cityWeather = await GetCityData(nameCity);
            SaveInFile(cityWeather);
            return cityWeather;
        }

        public void SaveInFile(WeatherR cityWeather)
        {
           
            string dataWeatherServer = JsonSerializer.Serialize(cityWeather);
            File.WriteAllText(fileTabelOneWeather, dataWeatherServer);
        }

        public WeatherR LoadOfFileOneWeather()
        {
            string oneWeather = File.ReadAllText(fileTabelOneWeather);
            var oneCity  = JsonSerializer.Deserialize<WeatherR>(oneWeather);
            return oneCity;
        }
    }
}

