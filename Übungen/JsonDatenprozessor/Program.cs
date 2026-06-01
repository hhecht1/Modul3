using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace JsonDatenprozessor
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        public int Score { get; set; }
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, IsActive: {IsActive}, Score: {Score}";
        }


    }
    class Program
    {
        private static readonly string JsonData = @"
    [
        {""Id"": 1, ""Name"": ""Alice"", ""IsActive"": true, ""Score"": 80},
        {""Id"": 2, ""Name"": ""Bob"", ""IsActive"": false, ""Score"": 90},
        {""Id"": 3, ""Name"": ""Charlie"", ""IsActive"": true, ""Score"": 40},
        {""Id"": 4, ""Name"": ""Diana"", ""IsActive"": true, ""Score"": 65}
    ]";
        static async Task Main(string[] args)
        {
            System.Console.WriteLine("Loading JSON data...");
            List<User> users = await GetJsonDataAsync();
            Console.WriteLine($"Total users: {users.Count}");
            System.Console.WriteLine();

            System.Console.WriteLine("Defining filter and processor...");
            System.Console.WriteLine();

            System.Console.WriteLine("Filter: Active users with score > 50");
            System.Console.WriteLine();
            Func<User, bool> activceHighScoreFilter = user => user.IsActive && user.Score > 50;


            System.Console.WriteLine();
            System.Console.WriteLine("Processor: Boost score by 10 points");

            Action<User> boostScoreAction = user =>
            {
                user.Score += 10;
                System.Console.WriteLine($"Processing {user.Name} -> Neuer Score: {user.Score}");
            };
            System.Console.WriteLine();

            System.Console.WriteLine("Processing users...");
            System.Console.WriteLine();

            List<User> processedUsers = await ProcessUserAsync(users, activceHighScoreFilter, boostScoreAction, 500);
            foreach (var user in processedUsers)
            {
                Console.WriteLine(user);
            }


        }
        public static async Task<List<User>> GetJsonDataAsync()
        {
            using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(JsonData));
            var users = await JsonSerializer.DeserializeAsync<List<User>>(stream);
            return users ?? new List<User>();
        }
        public static async Task<List<User>> ProcessUserAsync(
            List<User> users, Func<User, bool> filter, Action<User> processor,
            int delayMS
        )
        {
            List<User> result = new();
            foreach (var user in users)
            {
                if (filter(user))
                {
                    await Task.Delay(delayMS);
                    processor(user);
                    result.Add(user);
                }
                else
                {
                    System.Console.WriteLine($"User {user.Name} does not meet the filter criteria.");
                }
                ;

            }
            return result;
        }
    }



}