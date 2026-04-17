using System;

namespace Mulitcast
{
    public class MessageSystem
    {
        delegate void Message(string massage);

        public static void SendEmail(string massage)
        {
            Console.WriteLine($"Email sent: {massage}");
        }
        public static void SendSMS(string massage)
        {
            Console.WriteLine($"SMS sent: {massage}");
        }

        public static void Main (string[] args)
        {
            Message info = SendEmail;  //Mann kann Info so vorstellen das es eine Liste ist info => [Methode 1 , Methode 2, Methode 3, ...] also hier gleich nach dem delegate aufruf der 
                                        //Methode SendEmail zugewiesen wird, wird diese Methode in die Liste hinzugefügt.
            info += SendSMS; //Hier wird die Methode SendSMS der Liste hinzugefügt, also info => [SendEmail, SendSMS]   

            Console.WriteLine("Alle Benachrichtungen ");
            info("Hallo, hier ist eine Benachrichtigung!");     //Hier werden alle Methoden in der Liste aufgerufen, also hier SendEmail und SendSMS, da beide Methoden in der Liste sind.
                                                                //Das heißt es wird zuerst die Methode SendEmail aufgerufen und danach die Methode SendSMS, da die Reihenfolge der Methoden in der Liste so ist.

            // Mail entfernen 
            info -= SendEmail;                                  // Hier wird die Methode SendEmail aus der Liste entfernt, also info => [SendSMS] da SendEmail nicht mehr in der Liste ist, wird es auch nicht mehr aufgerufen wenn

            Console.WriteLine("\nNur SMS Benachrichtigung");
            info("Update: Es gibt eine neue Nachricht!");     //Hier wird nur die Methode SendSMS aufgerufen, da SendEmail aus der Liste entfernt wurde.

            Console.ReadLine();
        }
    }
}