using System;

namespace innlevering1_dataGFX
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Del1 game = new Del1())
            {
                game.Run();
            }
        }
    }
#endif
}

