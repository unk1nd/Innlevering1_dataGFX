using System;

namespace del2
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Del2 game = new Del2())
            {
                game.Run();
            }
        }
    }
#endif
}

