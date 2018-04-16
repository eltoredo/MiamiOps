using System;
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

        static Texture _backgroundTexture = new Texture("../../../../Images/background.png");
        static Sprite _backgroundSprite;

        public Game(string rootPath) : base(DEFAULT_WINDOW_WIDTH, DEFAULT_WINDOW_HEIGHT, WINDOW_TITLE, Color.Black)
        {
            _rootPath = rootPath;
            
            //_backgroundTexture.Repeated = true;
            _backgroundSprite = new Sprite(_backgroundTexture);
        }

        public override void Draw(GameTime gameTime)
        {
            _backgroundSprite.Draw(Window, RenderStates.Default);
            _roundUI.Draw(Window, _roundUI.MapWidth, _roundUI.MapHeight);
        }

        public override void Initialize()
        {
            _round = new Round(20, enemiesSpeed: 0.0005f);
            _roundUI = new RoundUI(_round, 1280, 720);
            _playerInput = new InputHandler(_roundUI);
        }

        public override void LoadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            //Thread.Sleep(100);
            _playerInput.Handle();
            _round.Update();
        }
    }
}
