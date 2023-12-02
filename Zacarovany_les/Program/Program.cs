using System;

namespace Zacarovany_les
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using var game = new ZacarovanyLes();
            game.Run();
        }
    }
}
