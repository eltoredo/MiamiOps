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
        Map _ctxMap;
        FloatRect _hitBoxEnnemi;

        int _animFrames;    // Number of animation frames (0 to 3 so a total of 4)
        int _direction;    // Direction in which the player is looking
        int _animStop;
        Color colorCharacters = new Color(255, 255, 255, 255);

       
        public EnemiesUI(RoundUI roundUIContext, Texture texture, int nbSprite, int spriteWidth, int spriteHeight, Vector enemyPlace, uint mapWidth, uint mapHeight, Map ctxMap)
        {
            _roundUIContext = roundUIContext;

            this._enemyTexture = texture;
            this._enemySprite = new Sprite(texture);

            this._nbSprite = nbSprite;
            

            this._spriteWidth = spriteWidth;
            this._spriteHeight = spriteHeight;

            this._enemySprite.Position = new Vector2f((float)enemyPlace.X * (mapWidth / 2), (float)enemyPlace.Y * (mapHeight / 2));

            _animStop = 0;
            _animFrames = 0;    // Basically, the player is not moving
            _direction = 0;
            _ctxMap = ctxMap;
            _hitBoxEnnemi = _enemySprite.GetGlobalBounds();
        }

        private Vector2f UpdatePlace(Vector enemyPlace, uint mapWidth, uint mapHeight)
        {
            Vector2f newEnnemyPlace = new Vector2f(((float) enemyPlace.X + 1) * (mapWidth / 2), (((float) enemyPlace.Y -  1) * (mapHeight / 2)) * (-1));
            return newEnnemyPlace;
        }

        public void Draw(RenderWindow window, uint mapWidth, uint mapHeight, Vector position)
        {
            this._enemySprite.Position = UpdatePlace(position, mapWidth, mapHeight);
            _hitBoxEnnemi = _enemySprite.GetGlobalBounds();

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
