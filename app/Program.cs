using System.Text.Json;
using System.Text.Json.Serialization;

namespace M3_Serialization
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Serialization");

            // https://www.youtube.com/watch?v=4pqHLyhnuUY&list=PLdo4fOcmZ0oVp5o3x_5IZiI7GNs0GklYH&index=1
            // Newtonsoft  https://youtu.be/pJtuuolUhCc?si=Np_OLZM5V9m2vnhH
            // doc  https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/ignore-properties

            var weatherForecast = new WeatherForecast()
            { 
                Date = DateTime.Parse("2026-04-16"),
                TempCelsius = 12,
                Summary = "Cold"
            };
            var weatherForecast2 = new WeatherForecast()
            {
                Date = DateTime.Parse("2026-04-1"),
                TempCelsius = 25,
                Summary = "Hot"
            };

            List<WeatherForecast> wfList = new List<WeatherForecast>();
            wfList.Add(weatherForecast);
            wfList.Add(weatherForecast2);

            // Serialize
            var jsonString = JsonSerializer.Serialize(weatherForecast);

            Console.WriteLine(jsonString);
            
            var filename = "WeatherForecast_WAT.json";
            File.WriteAllText(filename, jsonString);

            // Serialize Stream
            var streamName = "WeatherForecast_Stream.json";
            using FileStream createStream = File.Create(streamName);
            await JsonSerializer.SerializeAsync(createStream, weatherForecast);
            await createStream.DisposeAsync();

            // Deserialize
            var jsonStringDes = """{"Date": "2026-04-17T00:00:00-07:00", "TempCelsius":25,"Summary":"Very Hot"}""";

            weatherForecast = JsonSerializer.Deserialize<WeatherForecast>(jsonStringDes);
            Console.WriteLine(weatherForecast);

            // Read string
            var jsonStringRead = File.ReadAllText(filename);
            Console.WriteLine(jsonStringRead);

            // Read Stream
            using FileStream openStream = File.OpenRead(filename);
            weatherForecast = await JsonSerializer.DeserializeAsync<WeatherForecast>(openStream);
            await openStream.DisposeAsync();

            Console.WriteLine(weatherForecast);

            Console.WriteLine();
            Console.WriteLine("==============");
            Console.WriteLine();

            
            // List Serialize string
            var jsonList = JsonSerializer.Serialize(wfList);
            var stringNameList = "weatherStringList.json";
            File.WriteAllText(stringNameList, jsonList);

            // List Serialize Stream
            var streamNameList = "weatherStreamList.json";
            using FileStream createStreamList = File.Create(streamNameList);
            await JsonSerializer.SerializeAsync(createStreamList, weatherForecast);
            await createStreamList.DisposeAsync();

            Thread.Sleep(1000);

            // List Deserialize Stream
            Console.WriteLine("................ List Deserialize Stream");
            using FileStream openStreamList = File.OpenRead(streamNameList);
            weatherForecast = await JsonSerializer.DeserializeAsync<WeatherForecast>(openStreamList);
            Console.WriteLine(weatherForecast);

            // Test WriteIndented
            List<string> liste = new() { "Eintrag1", "Eintrag2" };
            string json = JsonSerializer.Serialize(liste, new JsonSerializerOptions { WriteIndented = false });
            File.WriteAllText("liste.json", json);

            // 
            var jsonList2 = File.ReadAllText(stringNameList);
            List<WeatherForecast> geladeneListe = JsonSerializer.Deserialize<List<WeatherForecast>>(jsonList2);

            foreach (var item in geladeneListe)
            {
                Console.WriteLine("Liste:   " + item);
            }

            Console.WriteLine();
            Console.WriteLine("=== FileStream ===");

            //List<string> liste = new() { "Eintrag1", "Eintrag2" };

            // Schreiben mit File.Create
            using (FileStream fs = File.Create("fileStreamList.json"))
            {
                await JsonSerializer.SerializeAsync(fs, wfList, new JsonSerializerOptions { WriteIndented = true });
            }

            Thread.Sleep(2000);

            List<WeatherForecast> geladeneListeStream = new();

            using (FileStream fs = File.OpenRead("fileStreamList.json"))
            {
                geladeneListeStream = await JsonSerializer.DeserializeAsync<List<WeatherForecast>>(fs);
            }

            foreach (var item in geladeneListeStream)
            {
                Console.WriteLine(item);
            }

            // damaged json
            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString,
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
                //IgnoreReadOnlyProperties = true
                //DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            };


            string damagedJson = "damagedJSON.json";

            var damagedJsonString = File.ReadAllText(damagedJson);

            var damagedWeather = JsonSerializer.Deserialize<WeatherForecast>(damagedJsonString, options);
            Console.WriteLine("----------");

            using (FileStream fs = File.Create("fileStreamList2.json"))
            {
                await JsonSerializer.SerializeAsync(fs, wfList, options);
            }


            // missing members       Wind


        }
    }

    [JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
    //[JsonObjectCreationHandling(JsonObjectCreationHandling.)]
    class WeatherForecast
    {
        public DateTimeOffset Date { get; set; }
        public int TempCelsius { get; set; }
        //[JsonIgnore]
        public string? Summary { get; set; }

        public override string ToString()
        {
            return $"Date: {Date},  TempCelsius: {TempCelsius},  Summary: {Summary}";
        }
    }
}
