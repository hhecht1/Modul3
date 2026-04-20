using System;


public enum Drink
{
    // Alkoholisch
    Beer,
    Wine,
    Whiskey,

    // Alkoholfrei
    Water,
    Juice,
    Cola
}

public enum DrinkCategory
{
    Alcoholic,
    NonAlcoholic
}




public class DrinkEventArgs : EventArgs
{
    public Drink Drink { get; }
    public DrinkCategory Category { get; }

    public DrinkEventArgs(Drink drink, DrinkCategory category)
    {
        Drink = drink;
        Category = category;
    }
}



public class BarMitDrink
{
    // Generisches Event mit eigenen EventArgs
    public event EventHandler<DrinkEventArgs> EsWirdEineRundeAusgegeben;

    public void RundeAusgeben(Drink drink)
    {
        // Kategorie anhand des Getränks bestimmen
        DrinkCategory category = drink switch
        {
            Drink.Beer or Drink.Wine or Drink.Whiskey => DrinkCategory.Alcoholic,
            _ => DrinkCategory.NonAlcoholic
        };

        // Event auslösen
        EsWirdEineRundeAusgegeben?.Invoke(
            this,
            new DrinkEventArgs(drink, category)
        );
    }
}



public class Person
{
    public string Name { get; set; }

    public void Eintreten(BarMitDrink bar)
    {
        bar.EsWirdEineRundeAusgegeben += RundeEmpfangen;
    }

    public void Verlassen(BarMitDrink bar)
    {
        bar.EsWirdEineRundeAusgegeben -= RundeEmpfangen;
    }

    private void RundeEmpfangen(object sender, DrinkEventArgs e)
    {
        string art = e.Category == DrinkCategory.Alcoholic
            ? "alkoholisches"
            : "alkoholfreies";

        Console.WriteLine($"{Name} freut sich über ein {art} Getränk: {e.Drink}");
    }
}


class Program
{
    static void Main(string[] args)
    {
        var bar = new BarMitDrink();

        var besucher1 = new Person { Name = "Dalis" };
        var besucher2 = new Person { Name = "Lama" };
        

        besucher1.Eintreten(bar);
        besucher2.Eintreten(bar);

        bar.RundeAusgeben(Drink.Beer);
        bar.RundeAusgeben(Drink.Cola);

        Console.WriteLine();

        besucher2.Verlassen(bar);

        bar.RundeAusgeben(Drink.Whiskey);
        bar.RundeAusgeben(Drink.Juice);

        Console.ReadKey();
    }
}
