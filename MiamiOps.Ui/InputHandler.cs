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

            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                music.Play();

                Vector2i _mouseVector2i = Mouse.GetPosition(_roundUIContext.GameCtx.Window);
                Vector _mouseVector = new Vector(_mouseVector2i.X, _mouseVector2i.Y);

                Vector _realMousePosition = new Vector(_roundUIContext.RoundContext.Player.Place.X - (_roundUIContext.GameCtx.MyView.Size.X / 2), _roundUIContext.RoundContext.Player.Place.Y - (_roundUIContext.GameCtx.MyView.Size.Y / 2));

                //_mouseVector = new Vector()

                _roundUIContext.RoundContext.Player.Attack(_mouseVector);
            }
        }
    }
}
