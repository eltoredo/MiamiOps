using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace MiamiOps
{
    public class Player
    {
        Texture _playerTexture;
        Sprite _playerSprite;

        int _direction;
        int _animFrames;
        int _animStop;
        float _speed;

        public Player()
        {
            _playerTexture = new Texture("../../Content/playersprite.png");
            _playerSprite = new Sprite(_playerTexture);
            _playerSprite.Position = new Vector2f(640, 580);

            _speed = 5000f;

            _animFrames = 0;
            _direction = 192;
            _animStop = 64;
        }

        public void Move(float deltaTime)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Z))
            {
                _animStop = 64;
                _playerSprite.Position -= new Vector2f(0f, _speed * deltaTime);
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                _animStop = 64;
                _playerSprite.Position += new Vector2f(0f, _speed * deltaTime);
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
            {
                _direction = 96;
                _animStop = 64;
                _playerSprite.Position -= new Vector2f(_speed * deltaTime, 0f);
            }

            else if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                _direction = 192;
                _animStop = 64;
                _playerSprite.Position += new Vector2f(_speed * deltaTime, 0f);
            }
            else _animStop = 0;

            /*if (_playerSprite.Position.X >= 960) _backgroundSprite.TextureRect = new IntRect(new Vector2i((int)_playerSprite.Position.X % (int)_backgroundSprite.Texture.Size.X, 0), new Vector2i(1280, 720));
            else if (_playerSprite.Position.X < 320) _backgroundSprite.TextureRect = new IntRect(new Vector2i((int)_playerSprite.Position.X % (int)_backgroundSprite.Texture.Size.X, 0), new Vector2i(1280, 720));*/

            if (_playerSprite.Position.Y < 0) _playerSprite.Position = new Vector2f(_playerSprite.Position.X, 0);
            else if (_playerSprite.Position.Y > 580) _playerSprite.Position = new Vector2f(_playerSprite.Position.X, 580);
            else if (_playerSprite.Position.X < 0) _playerSprite.Position = new Vector2f(0, _playerSprite.Position.Y);
            else if (_playerSprite.Position.X > 1220) _playerSprite.Position = new Vector2f(1220, _playerSprite.Position.Y);
        }

        public void Draw(GameTime gameTime, RenderWindow window)
        {
            if (_animFrames == 4) _animFrames = 0;
            _playerSprite.TextureRect = new IntRect(_animFrames * _animStop, _direction, 64, 96);
            ++_animFrames;

            _playerSprite.Draw(window, RenderStates.Default);
        }
    }
}
