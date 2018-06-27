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

        GameHandler _gameHandlerCtx;
        RoundUI _roundUI;
        InputHandler _playerInput;
        View _view;
        View _viewATH;
        Camera _camera;
        Convert _convert = new Convert();
        Text pause = new Text();
        Menu menu = new Menu(DEFAULT_WINDOW_WIDTH, DEFAULT_WINDOW_HEIGHT);
        GameOver gameOver = new GameOver(DEFAULT_WINDOW_WIDTH, DEFAULT_WINDOW_HEIGHT);
        Music _mainMusic = new Music("../../../../Images/JojoOP1.ogg");

        public Game(string rootPath) : base(DEFAULT_WINDOW_WIDTH, DEFAULT_WINDOW_HEIGHT, WINDOW_TITLE, Color.Black)
        {
            _rootPath = rootPath;
            _gameHandlerCtx = new GameHandler(_convert);
        }

        public override void Draw(GameTime gameTime)
        {
                Window.SetView(_view);
                Window.Draw(_gameHandlerCtx.Map);
                _roundUI.Draw(Window, _roundUI.MapWidth, _roundUI.MapHeight);

            if(Pause)
            {
                pause.Dispose();
                
                pause = new Text();

                pause.DisplayedString = "Jeu en pause";
                pause.Font = new Font("../../../Menu/pricedown.ttf");
                pause.Position = new Vector2f(_view.Center.X - 100, _view.Center.Y - 200);
                pause.CharacterSize = 50;
                pause.Color = Color.Black;

                pause.Draw(Window, RenderStates.Default);
            }
            
        }

        public override void Initialize()
        {
            if (gameOver.ReturnOrNot == false)
            {
                gameOver = new GameOver(DEFAULT_WINDOW_WIDTH, DEFAULT_WINDOW_HEIGHT);
                menu.OpenGame(Window);
            }

            _view = new View(new FloatRect(0, 0, DEFAULT_WINDOW_WIDTH, DEFAULT_WINDOW_HEIGHT));
            _viewATH = new View(Window.GetView());
            _roundUI = new RoundUI(_gameHandlerCtx, this, 3168, 3168, _gameHandlerCtx.Map, DEFAULT_WINDOW_WIDTH, DEFAULT_WINDOW_HEIGHT, _view, _viewATH);
            _playerInput = new InputHandler(_roundUI);
            _camera = new Camera();
            //_view.Zoom(4f);
            _mainMusic.Play();
            _mainMusic.Loop = true;
        }

        public override void LoadContent()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if (_gameHandlerCtx.RoundObject.GameState == true)
            {
                Window.Clear();
                gameOver.EndGame(Window, this,menu, _gameHandlerCtx);
                Window.Clear();
            }

            _gameHandlerCtx.RoundObject.Update();
            if (_gameHandlerCtx.HasLeft == true)
            {
                _roundUI.EffectMusic.Stop();
                _roundUI = new RoundUI(_gameHandlerCtx, this, 3168, 3168, _gameHandlerCtx.Map, DEFAULT_WINDOW_WIDTH, DEFAULT_WINDOW_HEIGHT, _view, _viewATH);
                _gameHandlerCtx.HasLeft = false;
            }

            _camera.CameraPlayerUpdate(_roundUI.PlayerUI.PlayerPosition.X, _roundUI.PlayerUI.PlayerPosition.Y, 3168, 3168, _view);
            _playerInput.Handle();
            _roundUI.Update();
            //Console.WriteLine(_round.Player.Place.X);
            //Console.WriteLine(_round.Player.Place.Y);
        }

        public InputHandler Input => _playerInput;
        public View MyView => _view;
        public Round Round => _gameHandlerCtx.RoundObject;
        public Convert ConvertMap => _convert;
        public Music MusicMain {
           get { return _mainMusic; }
           set { _mainMusic = value; }
        }
         
    }
}
