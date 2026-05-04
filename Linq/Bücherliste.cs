using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml.Serialization;
namespace Bücherliste
{
    public class Bücher
    {
        public string? Titel { get; set; }
        public string? Autor { get; set; }
        public double Preis { get; set; }
        public int Erscheinungsjahr { get; set; }
        public int Seiten { get; set; }
        public string? Beschreibung { get; set; }
    }

    internal class Programm
    {
        static void Main(string[] args)
        {
            string path = "bücherliste.json";

            string json = File.ReadAllText(path);

            List<Bücher>? bücherliste =
                JsonSerializer.Deserialize<List<Bücher>>(json);
                
            if (bücherliste == null)
            {
            Console.WriteLine("Keine Bücher geladen.");
            return;
            }
            
           foreach (var buch in bücherliste)
            {
                Console.WriteLine(
                    $"{buch.Titel} => wurde geschrieben von Autor: {buch.Autor} ({buch.Erscheinungsjahr})"
                );
            }
            Console.WriteLine("\n\n\n");
            var result = bücherliste.GroupBy(x => (x.Erscheinungsjahr /100)*100 ,y  => y).OrderByDescending(x => x.Key);

            foreach(var item in result)
            {
                
                Console.WriteLine(item.Key);
                foreach(var iteminner in item)
                {

                    Console.WriteLine("\t" + iteminner.Erscheinungsjahr + " : " + iteminner.Titel +  " => " + iteminner.Autor);
                } 
            }
            Console.WriteLine("\n\n\n");
            Console.WriteLine();
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("#########  Filterfunktion #########");

            Console.WriteLine("Bitte geben Sie ein Stichwort ein, um die Bücher zu filtern:");
            string stichwort = Console.ReadLine();
            var result2 =bücherliste.Where(x => x.Beschreibung.Contains(stichwort));
            Console.WriteLine("\n");
            Console.WriteLine($"\nBücher, die das Stichwort '{stichwort}' in ihrer Beschreibung enthalten:\n");
            foreach(var item in result2)            {
                Console.WriteLine(item.Titel + " " + item.Autor + "\n" + " Beschreibung: " + " => " + item.Beschreibung);
            }
            if(result2.Count() == 0)
            {
                Console.WriteLine("Keine Bücher gefunden, die das Stichwort enthalten.");
            }

        }
    }
}