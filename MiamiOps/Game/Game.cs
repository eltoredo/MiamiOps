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

        static Texture _backgroundTexture = new Texture("../../Content/Bbackground.jpg");
        static Sprite _backgroundSprite;

        Player _player;

        public Game(string rootPath) : base(DEFAULT_WINDOW_WIDTH, DEFAULT_WINDOW_HEIGHT, WINDOW_TITLE, Color.Black)
        {
            _rootPath = rootPath;

            _backgroundTexture.Repeated = true;
            _backgroundSprite = new Sprite(_backgroundTexture);
        }

        public override void Draw(GameTime gameTime)
        {
            _backgroundSprite.Draw(Window, RenderStates.Default);
            _player.Draw(GameTime, Window);
        }

        public override void Initialize()
        {
            _player = new Player();
        }

        public override void LoadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            _player.Move(gameTime.DeltaTimeUnscaled);
        }
    }
}
