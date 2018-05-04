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
        View _view;
        Map _map;
        Convert _convert = new Convert();

        public Game(string rootPath) : base(DEFAULT_WINDOW_WIDTH, DEFAULT_WINDOW_HEIGHT, WINDOW_TITLE, Color.Black)
        {
            _rootPath = rootPath;
        }

        public override void Draw(GameTime gameTime)
        {
            
            Window.Draw(_map);
            _roundUI.Draw(Window, _roundUI.MapWidth, _roundUI.MapHeight);
            Window.SetView(_view);

        }

        public override void Initialize()
        {
            _convert.ConvertXMLCollide(@"..\..\..\..\MiamiOps.Map\Map\tilemap.tmx");
            _convert.ConvertXMLSpawn(@"..\..\..\..\MiamiOps.Map\Map\tilemap.tmx");
            _round = new Round(100, enemiesSpeed: 0.0005f, playerSpeed: 0.005f);
            _map = new Map(@"..\..\..\..\MiamiOps.Map\Map\tilemap.tmx", @"..\..\..\..\MiamiOps.Map\Map\tileset2.png", _round);
            _roundUI = new RoundUI(_round, this, 1280, 720, _map);
            _playerInput = new InputHandler(_roundUI);
            _view = new View(Window.GetView());
           
        }

        public override void LoadContent()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            _playerInput.Handle();
            _round.Update();            
            _view.Center = new Vector2f(_roundUI.PlayerUI.PlayerPosition.X, _roundUI.PlayerUI.PlayerPosition.Y);
            
        }

        public InputHandler Input => _playerInput;
    }
}
