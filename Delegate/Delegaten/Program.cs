internal class Program
{


   
public class MyDelegateRechner
{
    public delegate int CalculationHandler(int a, int b);

    public int Add(int x, int y)
    {
        Console.WriteLine("Addition");
        return x + y;
    }

    public int Subtract(int x, int y)
    {
        Console.WriteLine("Subtraktion");
        return x - y;
    }

    public int Divide(int x, int y)
    {
        Console.WriteLine("Division");
        return x / y;
    }

    // ✅ NEU: Bedingung ausgelagert
    public CalculationHandler? GetCalculationFromUser()
    {
        Console.WriteLine("Was möchtest du tun? 1: Addieren, 2: Subtrahieren, 3: Dividieren");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out int number))
        {
            Console.WriteLine("Keine gültige Zahl!");
            return null;
        }

        return number switch
        {
            1 => Add,
            2 => Subtract,
            3 => Divide,
            _ => Add
        };
    }

    public void ExecuteCalculation(CalculationHandler calc)
    {
        int a = 5;
        int b = 3;

        Console.WriteLine($"Berechnung wird ausgeführt. a={a}, b={b}");
        int result = calc(a, b);
        Console.WriteLine("Ergebnis: " + result);
    }
}

  
   
static void Main(string[] args)
    {
        var rechner = new MyDelegateRechner();

        var calc = rechner.GetCalculationFromUser();

        if (calc != null)
            rechner.ExecuteCalculation(calc);

        Console.ReadKey();

        Func<float, float, float, float> Multiply = (x, y, z) => x * y * z;
        float result = Multiply(4f, 5f, 2f);
        Console.WriteLine("Das Ergebnis der Multiplikation ist: " + result);
    }
}
