using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.DAL;
using Weather.Model;
using System.IO;

namespace Weather.Entites
{
    public class WeatherManager
    {
        public bool RunAuto = true;
        Dictionary<string, WeatherR> TabelWteaher = new Dictionary<string, WeatherR>();

        public async Task<string> GetCity(string cityname)
        {
            WeatherR weatherR = new WeatherR();
            WeatherCity weather = new WeatherCity();

            weatherR = await weather.GetCityData(cityname);

            return $" City:{weatherR.location.name}\n,Temperature:{weatherR.current.temp_c}*C";
        }

        

        public async Task<Dictionary<string, WeatherR>> DataTabelCityAndGrid(string nameCity)
        {
            WeatherCity weather = new WeatherCity();
            var arr = await weather.LoadWeatherTable(nameCity);

            TabelWteaher.Add(arr.location.name, arr);
            return TabelWteaher;
        }

        public async Task Auto(string cityName)
        {
            string path = @"C:\Users\ZION\Desktop\TzioNet\TzionProjectWeather\Weather.DAL\bin\DataServer\DataWeather.txt";
            WeatherCity weather = new WeatherCity();
            Task ret = await Task.Factory.StartNew(async () =>
            {
                while (RunAuto)
                {
                    System.Threading.Thread.Sleep(20000);
                    string lines = await weather.AccessTheServerAndFetchData(cityName);
                    File.WriteAllText(path, lines);
                }
                
            });
           // while (RunAuto)
           // {
           //     System.Threading.Thread.Sleep(30000);
           //     string lines = await weather.AccessTheServerAndFetchData(cityName);
           //     File.WriteAllText(path, lines);

           //     // CreateTask_Weather(city);
           // }

           //return ret;

        }
        public async Task SaveDataDictionry(string city)
        {
            WeatherCity weather = new WeatherCity();
           await weather.LoadWeatherTable(city);
        }
        public void RefreshTableByUser(string cityName, int timeByUser)
        {
            Task.Run(async () =>
            {
                while (RunAuto)
                {
                    WeatherCity Request = new WeatherCity();
                    WeatherR oneCityWeather;
                    oneCityWeather = await Request.GetCityData(cityName);
                    Request.SaveInFile(oneCityWeather);

                    System.Threading.Thread.Sleep(timeByUser * 1000);
                }
            });
        }
        public void StopRefresh()
        {
            RunAuto = false;
        }

        public string GetCityOfFile()
        {
            WeatherR weatherR = new WeatherR();
            WeatherCity weather = new WeatherCity();

            weatherR = weather.LoadOfFileOneWeather();

            return $" City:{weatherR.location.name}\n,Temperature:{weatherR.current.temp_c}*C";

        }

    }
}
