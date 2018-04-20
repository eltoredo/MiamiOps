using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiamiOps
{
    public class InputHandler
    {
        RoundUI _roundUIContext;

        int _direction;
        int _animStop;

        public InputHandler(RoundUI roundUIContext)
        {
            _roundUIContext = roundUIContext;
        }

        public void Handle()
        {
            _animStop = 0;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Z))
            {
                _direction = _roundUIContext.PlayerUI.SpriteHeight * 3;
                _animStop = _roundUIContext.PlayerUI.SpriteWidth;
                _roundUIContext.RoundContext.Player.Move(new Vector(0, -1));
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                _direction = _roundUIContext.PlayerUI.SpriteHeight * 0;
                _animStop = _roundUIContext.PlayerUI.SpriteWidth;
                _roundUIContext.RoundContext.Player.Move(new Vector(0, 1));
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
            {
                _direction = _roundUIContext.PlayerUI.SpriteHeight * 1;
                _animStop = _roundUIContext.PlayerUI.SpriteWidth;
                _roundUIContext.RoundContext.Player.Move(new Vector(-1, 0));
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                _direction = _roundUIContext.PlayerUI.SpriteHeight * 2;
                _animStop = _roundUIContext.PlayerUI.SpriteWidth;
                _roundUIContext.RoundContext.Player.Move(new Vector(1, 0));
            }
        }

        public int Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        public int AnimStop => _animStop;
    }
}
