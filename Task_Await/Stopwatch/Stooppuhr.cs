using System;
using System.Diagnostics;

namespace Stoppuhr
{
    public class Program
    {
        public static bool running = true;

        public static Stopwatch stopwatch = new Stopwatch();
        public static List<TimeSpan> laps = new List<TimeSpan>();

        public static async Task Main(string[]args)
        {
            Console.WriteLine("StopUhr: ");
            Console.WriteLine("S = Start");
            Console.WriteLine("L = Zwischenzeit");
            Console.WriteLine("Enter = Stop");

            var usereingabe=Console.ReadKey();
            
            if(usereingabe.Key == ConsoleKey.S)
            {
                var task = RunStopwatchAsync();

                while (running)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(true).Key;

                        if (key == ConsoleKey.Enter)
                            running = false;

                        if (key == ConsoleKey.L)
                            AddLap();
                    }
                    task.Delay(50);

                }

                await task;
            }

            Console.WriteLine("Beendet. Die gemessene Zeit beträgt: " + stopwatch.Elapsed);
        }

        public static async Task RunStopwatchAsync()
        {
            stopwatch.Start();

            while (running)
            {
                Console.Clear();
                Console.WriteLine($"Zeit: {stopwatch.Elapsed:hh\\:mm\\:ss\\.fff}");

                Console.WriteLine("\nZwischenzeiten:");
                for (int i = 0; i < laps.Count; i++)
                {
                    Console.WriteLine($"Lap {i + 1}: {laps[i]:hh\\:mm\\:ss\\.fff}");
                }

                await Task.Delay(100);
            }
        }

        public static void AddLap()
        {
            laps.Add(stopwatch.Elapsed);
        }
    }
}