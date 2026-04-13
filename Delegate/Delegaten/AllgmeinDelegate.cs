using System.Runtime.InteropServices;

internal class AllgemeinDelegate
{
    // unser Delegat (Signatur == Methoden-Signatur !!!)
    delegate float CalculationHandler(float a, float b);
    //delegate float CalculationHandler(float a, int b);   // kein Überladen möglich

    // die auszuführende Methode wird hier in Form des Delegaten als Parameter übergeben
    static void ExecuteCalculation(CalculationHandler calc)
    {
        float a = 5;
        float b = 3;
        Console.WriteLine($"Berechnung wird ausgeführt. Werte   a:{a}   b:{b}");
        float result = calc(a, b);
        Console.WriteLine("Das Ergebnis ist: " + result);
    }

    // unsere finalen Methoden (Signatur == Signatur des Delegaten 'CalculationHandler')
    public static float AddMethode(float x, float y)
    {
        Console.WriteLine("Addition");
        return x + y;
    }

    static float SubstractMethode(float x, float y)
    {
        Console.WriteLine("Substraktion");
        return x - y;
    }

    static float DivisionMethode(float x, float y)
    {
       return (float)x / (float)y;
    }

    // static void Main(string[] args)
    // {
    //     Console.WriteLine("=== Delegatisierter Rechner ===");

    //     // Delegat wird deklariert
    //     CalculationHandler calc;

    //     Console.WriteLine("Was möchtest du tun? 1: Addieren, 2: Subtrahieren, 3: Dividieren");
    //     string input = Console.ReadLine();
    //     int number;

    //     if (int.TryParse(input, out number))
    //     {
    //         switch (number)
    //         {
    //             case 1:
    //                 calc = AddMethode;  // Methode wird der Delegat-Variable zugewiesen
    //                 break;
    //             case 2:
    //                 calc = SubstractMethode;
    //                 break;
    //             case 3:
    //                calc = DivisionMethode;
    //                break;
    //             default:
    //                 Console.WriteLine("Was erwartest du von mir? Dann wird halt addiert:");
    //                 calc = AddMethode;
    //                 break;
    //         }

    //         if (calc != null) // da calc null werden könnte -> überprüfen
    //         {
    //               ExecuteCalculation(calc);
    //               Console.WriteLine(calc(6,7));       // Berechnung wird ausgeführt
    //         }       
              
    //     }
    //     else
    //     {
    //         Console.WriteLine("Das war aber alles Andere als eine Zahl!");
    //     }

    //     Console.ReadKey();
    // }
}