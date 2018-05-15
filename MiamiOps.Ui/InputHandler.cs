using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace MiamiOps
{
    public class InputHandler
    {
        RoundUI _roundUIContext;
        Shoot _shoot;

        Event _event;

        Texture _bulletTexture;

        Music music = new Music("../../../Menu/fireball.ogg");

        public InputHandler(RoundUI roundUIContext)
        {
            _roundUIContext = roundUIContext;

            _bulletTexture = new Texture("../../../../Images/fireball.png");
        }

        public void Handle()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Z))
            {
                _roundUIContext.RoundContext.Player.Move(new Vector(0, -1));
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                _roundUIContext.RoundContext.Player.Move(new Vector(0, 1));
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
            {
                _roundUIContext.RoundContext.Player.Move(new Vector(-1, 0));
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                _roundUIContext.RoundContext.Player.Move(new Vector(1, 0));
            }

            /*if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                music.Play();

                Vector2i _mouseVector = Mouse.GetPosition(_roundUIContext.GameCtx.Window);
                Vector _mouseVector2 = new Vector(_mouseVector.X, _mouseVector.Y);

                Vector _realPosition = new Vector(_roundUIContext.GameCtx.Window)

                _roundUIContext.RoundContext.Player.Attack(_mouseVector2);
            }*/
        }
    }
}
