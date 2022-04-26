using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TaskWeather
{
    public class WeatherManager
    {
        public static async void GetHttp()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.weatherapi.com/");
                Console.WriteLine(client.BaseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage pespones = await client.GetAsync(@"v1/current.json?key=b480e7a490374b44be472511222103&q=LONDON&aqi=no");
                if (pespones.IsSuccessStatusCode)
                {
                    string str = await pespones.Content.ReadAsStringAsync();

                   // File.WriteAllText("serialize.txt", str);
                  //  string r = File.ReadAllText("serialize.txt");

                    WeatherData weatherData = JsonSerializer.Deserialize<WeatherData>(str);


                   


                    // string WrhitToJson = JsonSerializer.Serialize(str);
                    //File.WriteAllText("serialize.json", WrhitToJson);

                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }

            }
        }
        public static void NameCity(WeatherData weatherData)
        {

           
        }
    }
    
}

