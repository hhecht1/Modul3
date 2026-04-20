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
            decimal a = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Bitte zweite Zahl eingeben:");
            decimal b =decimal.Parse(Console.ReadLine());

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
        
