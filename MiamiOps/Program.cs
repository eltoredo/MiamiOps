using System;

namespace MiamiOps
{
    class Program
    {
        public static void Main(string[] args)
        {
<<<<<<< HEAD
            Console.WriteLine("(\")>");
            Console.ReadKey();
=======
            Game game = new Game(AppContext.BaseDirectory);
            game.Run();
>>>>>>> d265eb5c5a2682048a5d1dfb8a72f85c01ba21c6
        }
    }
}
