// using System;
// using System.Diagnostics;
// namespace synchron
// {

// public class Programm
// {
// static void Main(string[] args)
// {
//     Stopwatch stopSync = Stopwatch.StartNew();

//     Calculate();   // sync

//     stopSync.Stop();

//     Console.WriteLine("sync: " + stopSync.ElapsedMilliseconds);
//     Console.WriteLine("sync: " + stopSync.Elapsed);

//     Console.ReadKey();
// }

// static void Calculate()
// {
//     Calculate1();
//     Calculate2();
//     Calculate3();
// }

// static int Calculate1()
// {
//     for (int i = 0; i < 5; i++)
//     {
//         Thread.Sleep(1000);
//         Console.WriteLine("C1 -> " + i);
//     }

//     return 100;
// }

// static int Calculate2()
// {
//     for (int i = 0; i < 5; i++)
//     {
//         Thread.Sleep(900);
//         Console.WriteLine("\tC2 -> " + i);
//     }

//     return 200;
// }

// static int Calculate3()
// {
//     for (int i = 0; i < 5; i++)
//     {
//         Thread.Sleep(1100);
//         Console.WriteLine("\t\tC3 -> " + i);
//     }

//     return 300;
// }
// }
    
// }