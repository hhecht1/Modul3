using System;

namespace Userspecific_Data
{
    public class ThresholdReachedEventArgs : EventArgs
    {
        public int Threshold {get;set;}
        public int Value {get;set;}
        public DateTime TimeReached{get;set;}

    }

    public class Zähler
    {
        private int _total;
        private int _thershold;

        public Zähler(int threshold) => _thershold = threshold; 

        public event EventHandler<ThresholdReachedEventArgs> ThresholdReached;

        public void Add(int x)
        {
            _total += x;
            if(_total >= _thershold)
            {
                OnThresholdReached(new ThresholdReachedEventArgs{Threshold = _thershold, Value = _total, TimeReached = DateTime.Now});
            } 

        }
        protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
        {
            ThresholdReached?.Invoke(this,e);
        }

        class Programm
        {
            public static void Main()
            {
                var zähler = new Zähler(5);
                zähler.ThresholdReached += (sender,e) =>
                Console.WriteLine($"Ziel Erreicht! Schwellenwert: {e.Threshold} erreicht! Wert: {e.Value} Zeit: {e.TimeReached}");
                zähler.Add(6);
            }
        }
    }
}
