﻿using Blurlib.Render;
using Blurlib.Util;
using System;

namespace Eriona
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new ErionaGame(1200, 600, "Eriona Dev"))
                game.Run();
            Console.WriteLine("Appuyez sur Entrée pour continuer . . .");
        }
    }
}
