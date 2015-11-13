using System;

namespace Projet7
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (TennisPong game = new TennisPong())
            {
                game.Run();
            }
        }
    }
#endif
}

