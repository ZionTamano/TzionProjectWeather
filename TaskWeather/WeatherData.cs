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
     public  class WeatherData
     {
        public location location { get; set; }
        public current current { get; set; }


    }
}
