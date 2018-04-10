using System;

using SFML.Graphics;
using SFML.System;

namespace MiamiOps
{
    public class PlayerUI
    {
        Round _round;
        RoundUI _roundUIContext;

        Texture _playerTexture;
        Sprite _playerSprite;
        int _nbSprite;    // The number of column in a sprite
        int _spriteWidth;
        int _spriteHeight;

        int _animFrames;    // Number of animation frames (0 to 3 so a total of 4)
        int _direction;    // Direction in which the player is looking
        int _animStop;    // The width of the player multiplied by the number of frames to get the actual animated movement

        public PlayerUI(RoundUI roundUIContext, int levelTexture, int nbSprite, int spriteWidth, int spriteHeight, Vector playerPlace, uint mapWidth, uint mapHeight)
        {
            _roundUIContext = roundUIContext;

            this._playerTexture = new Texture("../../../../Images/sprite_panda_lv" + levelTexture.ToString() + ".png");
            this._playerSprite = new Sprite(_playerTexture);

            this._nbSprite = nbSprite;

            this._spriteWidth = spriteWidth;
            this._spriteHeight = spriteHeight;
            
            this._playerSprite.Position = new Vector2f((float)playerPlace.X * (mapWidth / 2), (float)playerPlace.Y * (mapHeight / 2));

            _animFrames = 0;    // Basically, the player is not moving
            _direction = spriteHeight * 2;    // Basically, the player looks to the right
        }

        private Vector2f UpdatePlace(Vector playerPlace, uint mapWidth, uint mapHeight)
        {
            return new Vector2f(((float)playerPlace.X + 1) * (mapWidth / 2), ((float)playerPlace.Y + 1) * (mapHeight / 2));
        }

        public void Draw(RenderWindow window, uint mapWidth, uint mapHeight)
        {
            this._playerSprite.Position = UpdatePlace(_roundUIContext.RoundContext.Player.Place, mapWidth, mapHeight);

            if (_animFrames == _nbSprite) _animFrames = 0;
            _playerSprite.TextureRect = new IntRect(_animFrames * _animStop, _direction, _spriteWidth, _spriteHeight);
            ++_animFrames;

            _playerSprite.Draw(window, RenderStates.Default);
        }

        public Vector2f PlayerPosition
        {
            get { return _playerSprite.Position; }
        }
    }
}