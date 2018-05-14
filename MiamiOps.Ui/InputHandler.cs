﻿using SFML.Window;
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
        Event _event;

        public InputHandler(RoundUI roundUIContext)
        {
            _roundUIContext = roundUIContext;
            _event = new Event();
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
                Vector _mouseVector = new Vector(_event.MouseMove.X, _event.MouseMove.Y);
                
                _roundUIContext.RoundContext.Player.Attack(_mouseVector);
            }
        }
    }
}
