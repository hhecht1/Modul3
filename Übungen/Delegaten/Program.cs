using System;
namespace Delegaten
{
    public class Taschenrechner
    {
        delegate double Rechner(double a,double b);
        
            public static void Main(string[] args)
        {
            Berechne(5.0, 3.0, Addiere);
            Berechne(5.0, 3.0, Subtrahiere);
            Berechne(5.0, 3.0, Multipliziere);
            Berechne(5.0, 3.0, Dividiere);
            Console.ReadLine();
        }

        public static double Addiere(double a, double b)
        {
            return a + b;
        }
        public static double Subtrahiere(double a, double b)
        {
            return a - b;
        }
        public static double Multipliziere(double a, double b)
        {
            return a * b;
        }
        public static double Dividiere(double a, double b)
        {
            if(b == 0)
            {
                throw new DivideByZeroException("Division durch Null ist nicht erlaubt.");
            }
            return a / b;
        }
        public static void Berechne(double a, double b, Rechner operation)
        {
            double ergebnis = operation(a, b);
            Console.WriteLine($"Das Ergebnis der Operation ist: {ergebnis}");
        }
        
    }
}