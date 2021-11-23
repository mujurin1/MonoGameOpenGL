using System;

namespace MonoGameOpenGL
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using(var game = new SampleGames.LibGame())
                game.Run();
        }
    }
}
