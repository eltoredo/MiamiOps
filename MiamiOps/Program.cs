using System;

namespace MiamiOps
{
    class Program
    {
        public static void Main(string[] args)
        {
            Game game = new Game(AppContext.BaseDirectory);
            game.Run();
        }
    }
}
