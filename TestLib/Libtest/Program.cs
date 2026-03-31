using ErsteLibary;

namespace TesteLib
{
    internal class Programm
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");


            // Instanz der Klasse Rechner erstellen
           var r = new Rechner();
           int ergebnis =r.Addiere2(5,7);
           Console.WriteLine("Ergebnis Addition: " + ergebnis);


           //Statischer Aufruf
           Console.WriteLine("Addition mit Aufruf " + RechnerStat.Addiere(3, 4));
              Console.WriteLine("Addition mit Aufruf " + RechnerStat.Addiere2(3, 4));
              
           
        }  

        
    }
    
}
