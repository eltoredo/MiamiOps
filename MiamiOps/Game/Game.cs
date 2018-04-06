﻿using System;
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

        static Texture _backgroundTexture = new Texture("../../../resources/Bbackground.jpg");
        static Sprite _backgroundSprite;

        readonly Enemies[] _enemies;    // Array which contain all enemies

        Player _player;

        public Game(string rootPath, int nbEnemies = 10) : base(DEFAULT_WINDOW_WIDTH, DEFAULT_WINDOW_HEIGHT, WINDOW_TITLE, Color.Black)
        {
            _rootPath = rootPath;

            _backgroundTexture.Repeated = true;
            _backgroundSprite = new Sprite(_backgroundTexture);

            this._enemies = new Enemies[nbEnemies];    // Create empty array of Enemies
            // Create much enemis as place in the attay this._enemies
            for (int idx = 0; idx < nbEnemies; idx += 1)
            {
                this._enemies[idx] = new Enemies(this, idx, new Vector2f(0, 0));
            }

        }

        public override void Draw(GameTime gameTime)
        {
            _backgroundSprite.Draw(Window, RenderStates.Default);
            _player.Draw(GameTime, Window);
        }

        public override void Initialize()
        {
            _player = new Player(2, 3, 31, 32, 100, 500);
        }

        public override void LoadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            _player.Move(gameTime.DeltaTimeUnscaled);
        }

        public Enemies[] Enemies
        {
            get { return this._enemies;}
        }
    }
}
