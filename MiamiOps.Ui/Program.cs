using System;
using System.Threading;
using SFML.Graphics;
using SFML.Window;

namespace MiamiOps
{
    class Program
    {
        public static void Main(string[] args)
        {
            Menu menu = new Menu(1200, 720);
            RenderWindow window = new RenderWindow(new VideoMode(1200, 720), "Miami Ops");
            menu.PlaySoundMenu();

            while (window.IsOpen)
            {

                if (Keyboard.IsKeyPressed(Keyboard.Key.Z))
                {
                    menu.Move(Keyboard.Key.Z);
                }
                else if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                {
                    menu.Move(Keyboard.Key.S);

                }
                else if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
                {
                    if (menu.SelectedItemIndex == 0)
                    {
                        window.Close();
                        menu.StopSoundMenu();
                        Game game = new Game(AppContext.BaseDirectory);
                        game.Run();
                        break;
                    }
                    else if (menu.SelectedItemIndex == 2)
                    {
                        window.Close();
                    }
                }

                menu.Draw(window);
                window.Display();
                Thread.Sleep(60);
            }

        }

    }
}
