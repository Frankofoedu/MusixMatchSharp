using System;

namespace MusixMatchSharp
{
    class Program
    {
        static readonly string api = "XXXXXXXXXXXXXXXXXXXXX";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Musixmatch mx = new Musixmatch(api);

           var songLyrics = mx.Track_lyrics_get("16860631");

            Console.Write(songLyrics);

            Console.Read();
        }
    }
}
