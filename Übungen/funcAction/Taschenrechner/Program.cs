using System;


namespace Taschenrechner
{
    public delegate double RechenOperationen(double a, double b);

    public class Programm
    {
        public static void Main(string[] args)
        {
            RechenOperationen add = new RechenOperationen(addieren);
            RechenOperationen sub = new RechenOperationen(subtrahieren);
            RechenOperationen mul = new RechenOperationen(multiplizieren);
            RechenOperationen div = new RechenOperationen(dividieren);
            

            Berrechne(add, 5, 3);
            Berrechne(sub, 5, 3);
            Berrechne(mul, 5, 3);
            Berrechne(div, 5, 3);
        }
        static double addieren(double a,double b) => a + b;
        static double subtrahieren(double a, double b) => a - b;
        static double multiplizieren(double a, double b) => a * b;
        static double dividieren(double a, double b) => b == 0 ? 0 : a / b;

        public static void Berrechne(RechenOperationen operation,double a,double b )
        {
            double ergebnis = operation(a, b);
            Console.WriteLine($"Das Ergebnis der Operation ist: {ergebnis}");
        }

    }
}