using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Threading;

namespace MiamiOps
{
    public class InputHandler
    {
        RoundUI _roundUIContext;

        Texture _bulletTexture;
        Music _bulletSound;

        int i = 20;
        int _timerNextWeapon = 4;
        int _timerPreviousWeapon = 4;


        public InputHandler(RoundUI roundUIContext)
        {
            _roundUIContext = roundUIContext;

            _bulletTexture = new Texture("../../../../Images/fireball.png");
            _bulletSound = new Music("../../../Menu/bullet_sound.ogg");
        }

        public void Handle()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Z))
            {
                _roundUIContext.RoundContext.Player.Move(new Vector(0, 1));
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                _roundUIContext.RoundContext.Player.Move(new Vector(0, -1));
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
            {
                _roundUIContext.RoundContext.Player.Move(new Vector(-1, 0));
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                _roundUIContext.RoundContext.Player.Move(new Vector(1, 0));
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.P))
            {
                if (_timerNextWeapon == 5)
                {
                    _roundUIContext.RoundContext.Player.ChangeWeapon(1);
                    _timerNextWeapon = 0;
                }
                _timerNextWeapon++;
               
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.O))
            {
                if (_timerPreviousWeapon == 4)
                {
                    _roundUIContext.RoundContext.Player.ChangeWeapon(-1);
                    _timerPreviousWeapon = 0;
                }
                _timerPreviousWeapon++;

            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                Vector2f viewPos = _roundUIContext.GameCtx.MyView.GetPosition();
                Vector viewPosition = new Vector(viewPos.X, viewPos.Y);

                Vector2i mouseVector2i = Mouse.GetPosition(_roundUIContext.GameCtx.Window);
                Vector mouseVector = new Vector(mouseVector2i.X, mouseVector2i.Y);

                Vector mouseAim = new Vector(viewPosition.X + mouseVector.X, viewPosition.Y + mouseVector.Y);

                Vector finalMousePosition = new Vector(mouseAim.X * (1.0 / (_roundUIContext.MapWidth/2.0)) - 1.0, mouseAim.Y * (1.0 / (_roundUIContext.MapHeight/2.0)) - 1.0); // Vraie coordonnées de la souris en -1 / 1

                if (i == 20)
                {
                    _bulletSound.Play();
                    _roundUIContext.RoundContext.Player.Attack(finalMousePosition);
                    i = 0;
                }
                i++;
            }
        }
    }
}
