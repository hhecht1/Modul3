// using System;
// using System.Threading.Tasks;
// using System.Diagnostics;
// using System.Collections.Generic;

// class Program
// {
//     static bool running = true;
//     static Stopwatch stopwatch = new Stopwatch();
//     static List<TimeSpan> laps = new List<TimeSpan>();

//     static async Task Main(string[] args)
//     {
//         Console.WriteLine("Stopuhr:");
//         Console.WriteLine("S = Start");
//         Console.WriteLine("L = Zwischenzeit");
//         Console.WriteLine("Enter = Stop");

//         if (Console.ReadKey(true).Key == ConsoleKey.S)
//         {
//             var task = RunStopwatchAsync();

//             while (running)
//             {
//                 if (Console.KeyAvailable)
//                 {
//                     var key = Console.ReadKey(true).Key;

//                     if (key == ConsoleKey.Enter)
//                         running = false;

//                     if (key == ConsoleKey.L)
//                         AddLap();
//                 }

//                 await Task.Delay(50);
//             }

//             await task;
//         }

//         Console.WriteLine("Beendet.");
//     }

//     static async Task RunStopwatchAsync()
//     {
//         stopwatch.Start();

//         while (running)
//         {
//             Console.Clear();
//             Console.WriteLine($"Zeit: {stopwatch.Elapsed:hh\\:mm\\:ss\\.fff}");

//             Console.WriteLine("\nZwischenzeiten:");
//             for (int i = 0; i < laps.Count; i++)
//             {
//                 Console.WriteLine($"Lap {i + 1}: {laps[i]:hh\\:mm\\:ss\\.fff}");
//             }

//             await Task.Delay(100);
//         }

//         stopwatch.Stop();
//         Console.WriteLine($"\nEndzeit: {stopwatch.Elapsed:hh\\:mm\\:ss\\.fff}");
//     }

//     static void AddLap()
//     {
//         laps.Add(stopwatch.Elapsed);
//     }
// }