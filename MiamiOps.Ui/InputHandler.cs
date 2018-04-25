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

        int _nbDirection;
        int _direction;
        int _animStop;

        public InputHandler(RoundUI roundUIContext)
        {
            _roundUIContext = roundUIContext;
            _nbDirection = Conversion(_roundUIContext.RoundContext.Player.Direction);
        }

        public void Handle()
        {
            _animStop = 0;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Z))
            {
                _direction = _roundUIContext.PlayerUI.SpriteHeight * _nbDirection;
                _animStop = _roundUIContext.PlayerUI.SpriteWidth;
                _roundUIContext.RoundContext.Player.Move(new Vector(0, -1));
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                _direction = _roundUIContext.PlayerUI.SpriteHeight * _nbDirection;
                _animStop = _roundUIContext.PlayerUI.SpriteWidth;
                _roundUIContext.RoundContext.Player.Move(new Vector(0, 1));
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
            {
                _direction = _roundUIContext.PlayerUI.SpriteHeight * _nbDirection;
                _animStop = _roundUIContext.PlayerUI.SpriteWidth;
                _roundUIContext.RoundContext.Player.Move(new Vector(-1, 0));
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                _direction = _roundUIContext.PlayerUI.SpriteHeight * _nbDirection;
                _animStop = _roundUIContext.PlayerUI.SpriteWidth;
                _roundUIContext.RoundContext.Player.Move(new Vector(1, 0));
            }
        }

        private int Conversion(Vector vector)
        {
            if (vector.X > vector.Y && vector.X > 0) return 2;
            else if (vector.X > vector.Y && vector.X < 0) return 1;
            else if (vector.X < vector.Y && vector.Y > 0) return 0;
            else if (vector.X < vector.Y && vector.Y < 0) return 3;
            else return 2;
        }

        public int Direction
        {
            get { return _direction; }
        }

        public int AnimStop => _animStop;
    }
}
