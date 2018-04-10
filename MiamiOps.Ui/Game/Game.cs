using System;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

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
            
            _backgroundTexture.Repeated = true;
            _backgroundSprite = new Sprite(_backgroundTexture);
        }

        public override void Draw(GameTime gameTime)
        {
            _backgroundSprite.Draw(Window, RenderStates.Default);
            _roundUI.Draw(Window, _roundUI.MapWidth, _roundUI.MapHeight);
        }

        public override void Initialize()
        {
            //Round UI avec paramètres du player UI
            _round = new Round(100, new Vector(0, 0), new Vector(0, 0));
            _roundUI = new RoundUI(_round, 1280, 720);
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
    }
}
