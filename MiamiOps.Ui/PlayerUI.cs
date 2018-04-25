using System;

using SFML.Graphics;
using SFML.System;

namespace MiamiOps
{
    public class PlayerUI
    {
        RoundUI _roundUIContext;
        Player _player;

        Texture _playerTexture;
        Sprite _playerSprite;
        int _nbSprite;    // The number of column in a sprite
        int _spriteWidth;
        int _spriteHeight;

        int _animFrames;    // Number of animation frames (0 to 3 so a total of 4)
        int _nbDirection;
        int _direction;
        int _animStop;

        public PlayerUI(RoundUI roundUIContext, int levelTexture, int nbSprite, int spriteWidth, int spriteHeight, Vector playerPlace, uint mapWidth, uint mapHeight)
        {
            _roundUIContext = roundUIContext;
            _player = _roundUIContext.RoundContext.Player;

            this._playerTexture = new Texture("../../../../Images/sprite_panda_lv" + levelTexture.ToString() + ".png");
            this._playerSprite = new Sprite(_playerTexture);

            this._nbSprite = nbSprite;

            this._spriteWidth = spriteWidth;
            this._spriteHeight = spriteHeight;
            
            this._playerSprite.Position = new Vector2f((float)playerPlace.X * (mapWidth / 2), (float)playerPlace.Y * (mapHeight / 2));

            _animFrames = 0;    // Basically, the player is not moving
            _direction = 2;
            _animStop = 0;
        }

        private Vector2f UpdatePlace(uint mapWidth, uint mapHeight)
        {
            _nbDirection = Conversion(_roundUIContext.RoundContext.Player.Direction);
            return new Vector2f(((float)_player.Place.X + 1) * (mapWidth / 2), ((float)_player.Place.Y + 1) * (mapHeight / 2));
        }

        public void Draw(RenderWindow window, uint mapWidth, uint mapHeight)
        {
            _direction = _spriteHeight * _nbDirection;
            _animStop = _spriteWidth;

            this._playerSprite.Position = UpdatePlace(mapWidth, mapHeight);

            if (_animFrames == _nbSprite) _animFrames = 0;
            _playerSprite.TextureRect = new IntRect(_animFrames * _animStop, _direction, _spriteWidth, _spriteHeight);
            ++_animFrames;

            _playerSprite.Draw(window, RenderStates.Default);
        }

        private int Conversion(Vector vector)
        {
            if (vector.X >= vector.Y && vector.X > 0) return 2;
            else if (vector.X >= vector.Y && vector.X < 0) return 1;
            else if (vector.X <= vector.Y && vector.Y > 0) return 0;
            else if (vector.X <= vector.Y && vector.Y < 0) return 3;
            else return 1;
        }

        public Vector2f PlayerPosition
        {
            get { return _playerSprite.Position; }
        }

        public int SpriteWidth => _spriteWidth;
        public int SpriteHeight => _spriteHeight;
    }
}