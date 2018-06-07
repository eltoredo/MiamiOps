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
            OpenGame();
        }

        public static void OpenGame()
        {
            Menu menu = new Menu(1280, 720);
            RenderWindow window = new RenderWindow(new VideoMode(1280, 720), "Miami Ops");
            menu.PlaySoundMenu();

            bool verifKeyNotPressedPreviously = true;


            while (window.IsOpen)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    window.Close();
                }

                if (Keyboard.IsKeyPressed(Keyboard.Key.Z))
                {
                    menu.Move(Keyboard.Key.Z);
                    verifKeyNotPressedPreviously = false;
                }
                else if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                {
                    menu.Move(Keyboard.Key.S);
                    verifKeyNotPressedPreviously = false;
                }

                else if (Keyboard.IsKeyPressed(Keyboard.Key.Return)&& verifKeyNotPressedPreviously == false)
                {
                    if (menu.SelectedItemIndex == 0)
                    {
                        window.Close();
                        menu.StopSoundMenu();
                        Game game = new Game(AppContext.BaseDirectory);
                        game.Run();
                        window.Close();
                        if (game.Round.GameState == false)
                        {
                            window.Close();
                        }
                        EndGame();
                        break;
                    }
                    else if (menu.SelectedItemIndex == 2)
                    {
                        window.Close();
                    }


                }

                menu.Draw(window);
                window.Display();
                Thread.Sleep(85);
            }

        }
        public static void EndGame()
        {
            
            GameOver gameOver = new GameOver(1280, 720);
            RenderWindow window = new RenderWindow(new VideoMode(1280, 720), "End Game");
            gameOver.PlaySoundMenu();

            while (window.IsOpen)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    window.Close();
                }
                
                if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                {
                    gameOver.Move(Keyboard.Key.D);
                }
                else if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
                {
                    gameOver.Move(Keyboard.Key.Q);
                }

                else if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
                {
                   
                    if (gameOver.SelectedItemIndex == 0)
                    {
                        window.Close();
                        gameOver.StopSoundMenu();
                        Game game = new Game(AppContext.BaseDirectory);
                        game.Run();
                        window.Close();
                        EndGame();
                        break;
                    }
                    else if (gameOver.SelectedItemIndex == 1)
                    {
                        gameOver.StopSoundMenu();
                        window.Close();
                        OpenGame();
                        break;
                    }
                }

                gameOver.Draw(window);
                window.Display();
                Thread.Sleep(80);
            }

        }

    }
}
