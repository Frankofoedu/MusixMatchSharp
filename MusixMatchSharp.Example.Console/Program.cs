using System;

namespace MusixMatchSharp.Example.ConsoleApp
{
    class Program
    {
        static string apikey = "XXXXXXXXXXXXXX";
        static void Main(string[] args)
        {
            Musixmatch mx = new Musixmatch(apikey);

            //Get the lyrics for Somebody That I Used to Know (Gotye feat. Kimbra)
           var lyr = mx.Track_lyrics_get("15953433");

            Console.WriteLine(lyr);
        }
    }
}
