using System;

namespace events
{
    public class Zähler
    {
        private int _total;
        private int _threshold;

        public Zähler(int threshold) => _threshold = threshold;

        public event EventHandler ThresholdReached;
        public void Add(int x)
        {
            _total += x => _total >= _threshold? ThresholdReached?.Invoke(this,EventArgs.Empty):null;
        }
    }
    public class Program
    {
        public static void Main()
        {
            var zähler = new Zähler(5);
            zähler.ThresholdReached += (sender,e) => Console.WriteLine("Threshold reached!");
            zähler.Add(3);
            zähler.Add(2); // Abhier wäre die Ausgabe "Threshold reached!" zu sehen, da der Schwellenwert erreicht wurde.
        }
    }
}