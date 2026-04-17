using System;

namespace Transformator
{
    public class StringTransformator
    {
        public delegate string StringModifikator(string text);

        // TExtverarbeitung mit Delegaten
        public static void VerarbeiteText(string[] texte,StringModifikator mod)
        {
            foreach(string text in texte)
            {
                string ergebnis =mod(text);
                Console.WriteLine(ergebnis);
            }
        }
        public static string ToUpper(string text)
        {
            return text.ToUpper();
        }
        public static string ToLower(string text)

        {
            return text.ToLower();
        }
        public static string Reverse(string text)
        {
            char[] charArray = text.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }    
        public static string Sterne(string text)
        {
            return new string('*', text.Length);
        }

        public static void Main(string[ ] args)
        {
            string[] wörter = { "Hallo", "Welt", "Delegaten", "C#" };
            VerarbeiteText(wörter, ToUpper);
            Console.WriteLine();
            VerarbeiteText(wörter, ToLower);
            Console.WriteLine();
            VerarbeiteText(wörter, Reverse);
            Console.WriteLine();
            VerarbeiteText(wörter, Sterne);
            Console.WriteLine();

            Console.ReadLine();

        }
    }
}