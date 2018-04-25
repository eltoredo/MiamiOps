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

        public InputHandler(RoundUI roundUIContext)
        {
            _roundUIContext = roundUIContext;
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
        }
    }
}
