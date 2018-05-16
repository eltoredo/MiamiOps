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

                Vector2f viewPos = _roundUIContext.GameCtx.MyView.GetPosition();
                Vector viewPosition = new Vector(viewPos.X, viewPos.Y);

                Vector2i mouseVector2i = Mouse.GetPosition(_roundUIContext.GameCtx.Window);
                Vector mouseVector = new Vector(mouseVector2i.X, mouseVector2i.Y);

                Vector mouseAim = new Vector(viewPosition.X + mouseVector.X, viewPosition.Y + mouseVector.Y);

                Vector finalMousePosition = new Vector(mouseAim.X * (1.0 / (_roundUIContext.MapWidth/2.0)) - 1.0, mouseAim.Y * (1.0 / (_roundUIContext.MapHeight/2.0)) - 1.0); // Vraie coordonnées de la souris en -1 / 1

                //_mouseVector = new Vector()

                _roundUIContext.RoundContext.Player.Attack(finalMousePosition);
            }
        }
    }
}
