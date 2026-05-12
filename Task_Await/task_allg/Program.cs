using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

class Program
{
    public static async Task Main()
    {
        Console.WriteLine("Start");
        await Task.Run(async () =>
        {
            await Task.Delay(1000);
              Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("hintergrundfarbe geändert");
          
            Console.WriteLine("Aufgabe abgeschlossen");
        });
        Console.WriteLine("Ende");
         Console.BackgroundColor = ConsoleColor.Black;

        Console.WriteLine("*****************************************");

        var task1 = Task.Run(async () => { await Task.Delay(1000); Console.WriteLine("Aufgabe 1"); });
        var task2 = Task.Run(async () => { await Task.Delay(2000); Console.WriteLine("Aufgabe 2"); });
        var task3 = Task.Run(async () => { await Task.Delay(3000); Console.WriteLine("Aufgabe 3"); });

        await Task.WhenAll(task1,task2,task3);

        Console.WriteLine("Alle Aufgaben abgeschlossen");



        Console.WriteLine("*****************************************");

        Task<int> berechne = Task.Run(()=>
        {
            int sum =0;
            for (int i = 0; i < 100; i++)
            {
                sum += i;
            }
            return sum;
        });
        int ergebnis = await berechne;
        Console.WriteLine($"Ergebnis: {ergebnis}");
    
    }
}