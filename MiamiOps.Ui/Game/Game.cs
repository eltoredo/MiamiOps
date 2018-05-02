﻿using System;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Threading;

namespace MiamiOps
{
    public class Game : GameLoop
    {
        readonly string _rootPath;

        public const uint DEFAULT_WINDOW_WIDTH = 1280;
        public const uint DEFAULT_WINDOW_HEIGHT = 720;

        public const string WINDOW_TITLE = "MiamiOps";

        Round _round;
        RoundUI _roundUI;
        InputHandler _playerInput;

        Map _map;

        public Game(string rootPath) : base(DEFAULT_WINDOW_WIDTH, DEFAULT_WINDOW_HEIGHT, WINDOW_TITLE, Color.Black)
        {
            _rootPath = rootPath;
        }

        public override void Draw(GameTime gameTime)
        {
            Window.Draw(_map);
            _roundUI.Draw(Window, _roundUI.MapWidth, _roundUI.MapHeight);
        }

        public override void Initialize()
        {
            _round = new Round(100, enemiesSpeed: 0.005f, playerSpeed: 0.05f);
            _map = new Map(@"..\..\..\Map\tilemap.tmx", @"..\..\..\Map\tileset2.png",_round);
            _roundUI = new RoundUI(_round, this, 1280, 720, _map);
            _playerInput = new InputHandler(_roundUI);
        }

        public override void LoadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            _playerInput.Handle();
            _round.Update();
            
        
        }

        public InputHandler Input => _playerInput;
    }
}
