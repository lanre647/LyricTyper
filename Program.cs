using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main()
    {
        // === TITLE ===
        string title = "Imagine - John Lennon";
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(title);
        Console.WriteLine(new string('-', title.Length));
        Console.ResetColor();
        Console.WriteLine();

        // === LYRICS ===
        // Only use short excerpts for demonstration purposes.
        // (If you use the full lyrics, ensure you have permission or
        //  keep it for personal learning.)
        List<string> lyrics = new List<string>
        {
            "Imagine there's no heaven",
            "It's easy if you try",
            "No hell below us",
            "Above us, only sky",
            "Imagine all the people",
            "Living for today... Ah..."
        };

        /* === TIMING DATA ===
         * These delays roughly correspond to how long each lyric line lasts in the song.
         * You can adjust them to match the actual track you’re listening to.
         * For example, if line 1 lasts about 3 seconds, use 3000.
         */
        List<int> lineDurations = new List<int> { 3000, 2500, 2000, 2000, 3000, 3500 };

        // === VISUAL SETTINGS ===
        ConsoleColor highlightColor = ConsoleColor.Red;   // active line
        ConsoleColor fadedColor = ConsoleColor.DarkGray;   // previous lines

        // === BASE SPEED SETTINGS ===
        int baseCharDelay = 50;  // baseline typing delay per character in ms
        Random random = new Random();

        // === FOR SMOOTH SCROLLING EFFECT ===
        List<string> previousLines = new List<string>();
        int bufferHeight = Console.WindowHeight - 2;

        for (int i = 0; i < lyrics.Count; i++)
        {
            string currentLine = lyrics[i];
            previousLines.Add(currentLine);
            if (previousLines.Count > bufferHeight)
                previousLines.RemoveAt(0); // remove oldest when scrolling

            Console.Clear();

            // === DISPLAY PREVIOUS LINES ===
            foreach (var line in previousLines.GetRange(0, previousLines.Count - 1))
            {
                Console.ForegroundColor = fadedColor;
                Console.WriteLine(line);
            }

            // === DISPLAY CURRENT LINE WITH FADE-IN ===
            Console.ForegroundColor = highlightColor;
            for (int j = 0; j < currentLine.Length; j++)
            {
                // Alternate between bright and dim to simulate a fade
                Console.ForegroundColor = (j % 2 == 0) ? highlightColor : ConsoleColor.Gray;
                Console.Write(currentLine[j]);

                // Slow down slightly at punctuation for realism
                int charDelay = baseCharDelay;
                if (".,!?".Contains(currentLine[j]))
                    charDelay *= 3;

                Thread.Sleep(charDelay);
            }
            Console.WriteLine();
            Console.ResetColor();

            /* === REALISTIC DELAY BETWEEN LINES ===
             * Instead of a random delay, we use our pre-set lineDurations.
             * If you want perfect sync, measure the duration of each lyric
             * in the song (in ms) and replace these numbers.
             */
            if (i < lineDurations.Count)
                Thread.Sleep(lineDurations[i]);
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n--- End of Lyrics ---");
        Console.ResetColor();
    }
}
