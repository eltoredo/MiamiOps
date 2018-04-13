using System;

using SFML.Graphics;
using SFML.System;

namespace MiamiOps
{
    public class EnemiesUI
    {
        RoundUI _roundUIContext;

        Texture _enemyTexture;
        Sprite _enemySprite;
        int _nbSprite;    // The number of columns in a sprite
        int _spriteWidth;
        int _spriteHeight;

        int _animFrames;    // Number of animation frames (0 to 3 so a total of 4)
        int _direction;    // Direction in which the player is looking
        int _animStop;

        public EnemiesUI(RoundUI roundUIContext, int nbSprite, int spriteWidth, int spriteHeight, Vector enemyPlace, uint mapWidth, uint mapHeight)
        {
            _roundUIContext = roundUIContext;

            this._enemyTexture = new Texture("../../../../Images/monstersprite.png");
            this._enemySprite = new Sprite(_enemyTexture);

            this._nbSprite = nbSprite;

            this._spriteWidth = spriteWidth;
            this._spriteHeight = spriteHeight;

            this._enemySprite.Position = new Vector2f((float)enemyPlace.X * (mapWidth / 2), (float)enemyPlace.Y * (mapHeight / 2));

            _animFrames = 0;    // Basically, the player is not moving
            _direction = 0;
        }

        private Vector2f UpdatePlace(Vector enemyPlace, uint mapWidth, uint mapHeight)
        {
            return new Vector2f(((float)enemyPlace.X + 1) * (mapWidth / 2), ((float)enemyPlace.Y + 1) * (mapHeight / 2));
        }

        public void Draw(RenderWindow window, uint mapWidth, uint mapHeight, Vector position)
        {
            this._enemySprite.Position = UpdatePlace(position, mapWidth, mapHeight);

            if (_animFrames == _nbSprite) _animFrames = 0;
            _enemySprite.TextureRect = new IntRect(_animFrames * _animStop, _direction, _spriteWidth, _spriteHeight);
            ++_animFrames;

            _enemySprite.Draw(window, RenderStates.Default);
        }

        public Vector2f EnemyPosition
        {
            get { return _enemySprite.Position; }
        }
    }
}
