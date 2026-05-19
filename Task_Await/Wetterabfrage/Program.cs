using System;
using System.Security.Cryptography.X509Certificates;

namespace Wetterabfrage
{
    public class Program
    {

        public enum WeatherCondition
        {
            sonnig,
            wolkig,
            regnerisch,
            stürmisch,
            schneit,


        }
        public static bool running = true;

        public static async Task Main(string[] args)
        {
            Console.WriteLine(".........Starte Wetterabfrage....");
            Console.WriteLine("Drücke zum Starten die Enter-Taste...");
            Console.ReadKey();
            Console.WriteLine("Wetterabfrage läuft...");
            var loadingTask = Loading();



            var weatherTask = new List<Task<string>>
            {
                GetWeatherAsync("Berlin"),
                GetWeatherAsync("Hamburg"),
                GetWeatherAsync("München"),
                GetWeatherAsync("Köln"),
                GetWeatherAsync("Frankfurt")
            };
            string[] weatherResults = await Task.WhenAll(weatherTask);
            running = false;
            await loadingTask;

            foreach (var weatherResult in weatherResults)
            {
                Console.WriteLine(weatherResult);
            }







        }


        public static async Task<string> GetWeatherAsync(string city)
        {
            await Task.Delay(5000);
            Console.WriteLine();


            var temperatur = new Random().Next(-10, 35);
            var condition = Enum.GetValues(typeof(WeatherCondition));
            var weatherCondition = (WeatherCondition)condition.GetValue(new Random().Next(condition.Length));

            return $"In {city} ist es/oder {weatherCondition} es bei einer Temperatur von {temperatur}°C.";

        }
        public static async Task Loading()
        {
            while (running)
            {
                Console.Write("🌦️ 🌧️ ⛈️ 🌩️ ☀️ ☁️ 🌥️ 🌤️ ");
                await Task.Delay(500);
            }

        }
    }
}