using System;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Threading;
using System.Collections.Generic;

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
        View _viewATH;
        Camera _camera;
        Convert _convert = new Convert();
        HashSet<float[]> _collide;


        public Game(string rootPath) : base(DEFAULT_WINDOW_WIDTH, DEFAULT_WINDOW_HEIGHT, WINDOW_TITLE, Color.Black)
        {
            _rootPath = rootPath;
        }

        public override void Draw(GameTime gameTime)
        {
            Window.SetView(_view);
            Window.Draw(_map);
            _roundUI.Draw(Window, _roundUI.MapWidth, _roundUI.MapHeight);
            
        }

        public override void Initialize()
        {
            _collide = _convert.ConvertXMLCollide(@"..\..\..\..\MiamiOps.Map\Map\tilemap.tmx");
            _map = new Map(@"..\..\..\..\MiamiOps.Map\Map\tilemap.tmx", @"..\..\..\..\MiamiOps.Map\Map\tileset2.png");
            _round = new Round(100, enemieSpawn: new Vector(), enemiesSpeed: 0.0005f, playerSpeed: 0.005f,enemySpawn: _convert.ConvertXMLSpawn(@"..\..\..\..\MiamiOps.Map\Map\tilemap.tmx"),playerHauteur:0f,playerLargeur:0f, playerLife: 100);
            foreach (var item in _collide)
            {
                _round.AddObstacle(item[0], item[1], item[2], item[3]);
            }
            _view = new View(new FloatRect(0, 0, DEFAULT_WINDOW_WIDTH, DEFAULT_WINDOW_HEIGHT));
            _viewATH = new View(Window.GetView());
            _roundUI = new RoundUI(_round, this, 3168, 3168, _map, DEFAULT_WINDOW_WIDTH, DEFAULT_WINDOW_HEIGHT, _view, _viewATH);
            _playerInput = new InputHandler(_roundUI);
            _camera = new Camera();
           //_view.Zoom(4f); Permet de dézoomer
        }

        public override void LoadContent()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            Console.WriteLine("Player place: " + _round.Player.Place.X + " ; " + _round.Player.Place.Y);
            foreach (float[] wall in _round.Obstacles) Console.WriteLine("Wall position: " + wall[0] + " ; " + wall[1] + " ; " + wall[2] + " ; " + wall[3]);
            Thread.Sleep(500);

            _camera.CameraPlayerUpdate(_roundUI.PlayerUI.PlayerPosition.X, _roundUI.PlayerUI.PlayerPosition.Y, 3168, 3168, _view);
            _playerInput.Handle();
            _round.Update();
            _roundUI.Update();
        }

        public InputHandler Input => _playerInput;
        public View MyView => _view;
         
    }
}
