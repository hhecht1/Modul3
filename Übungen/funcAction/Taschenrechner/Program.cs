using System;

namespace Taschenrechner
{
    class Taschenrechner
    {
        public static decimal Berechne(decimal a,decimal b,Func<decimal, decimal, decimal> operation)
        {
            return operation(a, b);
        }
    

    public static void Main()
        {
            Func<decimal,decimal,decimal> addieren =(x,y) => x+y;
            Func<decimal, decimal, decimal> subtrahieren = (x, y) => x - y;
            Func<decimal, decimal, decimal> multiplizieren = (x, y) => x * y;
            Func<decimal, decimal, decimal> dividieren = (x, y) => y != 0 ? x / y : throw new DivideByZeroException("Division durch Null ist nicht erlaubt.");


            Console.WriteLine("Bitte erste Zahl eingeben:");
            if (!decimal.TryParse(Console.ReadLine(), out decimal a))
            {
                Console.WriteLine("Ungültige Eingabe. Bitte eine gültige Zahl eingeben.");
                return;
            }   

            Console.WriteLine("Bitte zweite Zahl eingeben:");
            if (!decimal.TryParse(Console.ReadLine(), out decimal b))
            {
                Console.WriteLine("Ungültige Eingabe. Bitte eine gültige Zahl eingeben.");
                return;
            }

            Console.WriteLine("Bitte Operation eingeben (+, -, *, /):");
            string eingabe = Console.ReadLine();

            Func<decimal, decimal, decimal> operation;    

            switch (eingabe)
            {
                case "+":
                operation = addieren;
                break;
                case "-":
                operation = subtrahieren;
                break;
                case "*":
                operation = multiplizieren;
                break;
                case "/":
                operation = dividieren;
                break;
                default:
                throw new InvalidOperationException("Ungültige Operation.");
            }
           
            decimal ergebnis = Berechne(a, b, operation);
            Console.WriteLine("Ergebnis: " + ergebnis);
        }

        

    }
}
        
