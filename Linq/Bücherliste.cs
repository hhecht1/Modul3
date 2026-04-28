using System;
using System.Collections.Generic;
using System.IO;
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
        }
    }
}