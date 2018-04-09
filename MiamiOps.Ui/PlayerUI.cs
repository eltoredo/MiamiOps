using System;using SFML.Graphics;using SFML.Window;using SFML.System;
namespace MiamiOps{    public class PlayerUI    {        Texture _playerTexture;        Sprite _playerSprite;        int _spriteWidth;        int _spriteHeight;
        int _direction; // Direction in which the player is looking        int _animFrames; // Number of animation frames (0 to 3 so a total of 4)        int _animStop; // The width of the player multiplied by the number of frames to get the actual animated movement        int _nbSprite; // The number of column in a sprite
        float _life;        float _speed;
        public Vector2f PlayerPosition        {            get { return _playerSprite.Position; }        }
        public PlayerUI(int levelTexture, int nbSprite, int spriteWidth, int spriteHeight, float life, float speed)        {            _playerTexture = new Texture("../../../../Images/sprite_panda_lv" + levelTexture.ToString() + ".png");            _playerSprite = new Sprite(_playerTexture);            _playerSprite.Position = new Vector2f(640, 580);            _nbSprite = nbSprite;
            _spriteWidth = spriteWidth;            _spriteHeight = spriteHeight;
            _speed = speed;            _life = life;
            _animFrames = 0; // Basically, the player is not moving            _direction = spriteHeight * 2; // Basically, the player looks to the right        }
        public void Move(float deltaTime)        {
            _animStop = 0;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Z))
            {
                _direction = _spriteHeight * 3;
                _animStop = _spriteWidth;
                _playerSprite.Position -= new Vector2f(0f, _speed * deltaTime);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                _direction = _spriteHeight * 0;
                _animStop = _spriteWidth;
                _playerSprite.Position += new Vector2f(0f, _speed * deltaTime);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
            {
                _direction = _spriteHeight * 1;
                _animStop = _spriteWidth;
                _playerSprite.Position -= new Vector2f(_speed * deltaTime, 0f);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                _direction = _spriteHeight * 2;
                _animStop = _spriteWidth;
                _playerSprite.Position += new Vector2f(_speed * deltaTime, 0f);
            }

            /*if (_playerSprite.Position.X >= 960) _backgroundSprite.TextureRect = new IntRect(new Vector2i((int)_playerSprite.Position.X % (int)_backgroundSprite.Texture.Size.X, 0), new Vector2i(1280, 720));            else if (_playerSprite.Position.X < 320) _backgroundSprite.TextureRect = new IntRect(new Vector2i((int)_playerSprite.Position.X % (int)_backgroundSprite.Texture.Size.X, 0), new Vector2i(1280, 720));*/

            // Boundaries of the map to not let the player go out of the screen for the moment
            if (_playerSprite.Position.Y < 0) _playerSprite.Position = new Vector2f(_playerSprite.Position.X, 0);            else if (_playerSprite.Position.Y > 580) _playerSprite.Position = new Vector2f(_playerSprite.Position.X, 580);            else if (_playerSprite.Position.X < 0) _playerSprite.Position = new Vector2f(0, _playerSprite.Position.Y);            else if (_playerSprite.Position.X > 1220) _playerSprite.Position = new Vector2f(1220, _playerSprite.Position.Y);        }
        public void Draw(GameTime gameTime, RenderWindow window)
        {            // It creates a rectangle around the player based on his left boundaries (whichm movement he's actually doing, the direction he's looking based on his height, his width and height before drawing him            if (_animFrames == _nbSprite) _animFrames = 0;            _playerSprite.TextureRect = new IntRect(_animFrames * _animStop, _direction, _spriteWidth, _spriteHeight);            ++_animFrames;
            _playerSprite.Draw(window, RenderStates.Default);        }    }}