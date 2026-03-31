namespace ErsteLibary;

public class Rechner
{
    public int Addiere(int a,int b) => a+b;
    public int Addieren2(int a, int b)
    {
        Console.WriteLine("Addieren2 wird aufgerufen");
        return a + b;
    }

}
public static class RechnerStat
{
    public static int Addiere(int a, int b) => a + b;

    public static int Addiere2(int a, int b)
    {
        Console.WriteLine("_____    Addiere statisch    ______");
        return a + b;
    }
}


