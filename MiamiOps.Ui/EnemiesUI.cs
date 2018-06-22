﻿using System;

using SFML.Graphics;
using SFML.System;

namespace MiamiOps
{
    public class EnemiesUI
    {
        RoundUI _roundUIContext;

        Texture _enemyTexture;
        Sprite _enemySprite;
        Enemies _enemy;

        int _nbSprite;    // The number of columns in a sprite
        int _spriteWidth;
        int _spriteHeight;
        Map _ctxMap;
        FloatRect _hitBoxEnnemi;
        int _effectTime;

        int _animFrames;    // Number of animation frames (0 to 3 so a total of 4)
        int _direction;    // Direction in which the player is looking
        int _animStop;
        Color colorCharacters = new Color(255, 255, 255, 255);

       
        public EnemiesUI(RoundUI roundUIContext, Texture texture, int nbSprite, int spriteWidth, int spriteHeight, Enemies enemy, uint mapWidth, uint mapHeight, Map ctxMap)
        {
            _roundUIContext = roundUIContext;
            _enemy = enemy;

            this._enemyTexture = texture;
            this._enemySprite = new Sprite(texture);

            this._nbSprite = nbSprite;
            

            this._spriteWidth = spriteWidth;
            this._spriteHeight = spriteHeight;

            this._enemySprite.Position = new Vector2f((float)enemy.Place.X * (mapWidth / 2), (float)enemy.Place.Y * (mapHeight / 2));

            _animStop = 0;
            _animFrames = 0;    // Basically, the player is not moving
            _direction = 0;
            _ctxMap = ctxMap;
            _hitBoxEnnemi = _enemySprite.GetGlobalBounds();
        }

        private Vector2f UpdatePlace(Vector enemyPlace, uint mapWidth, uint mapHeight)
        {
            Vector2f newEnnemyPlace = new Vector2f(((float)enemyPlace.X + 1) * (mapWidth / 2), (((float)enemyPlace.Y - 1) * (mapHeight / 2))*-1);
            //if (_ctxMap.Collide(this._hitBoxEnnemi))
            //{
            //    _enemySprite.Color = Color.Red;
            //    return newEnnemyPlace;
            //}

            //_enemySprite.Color = colorCharacters;
           
            return newEnnemyPlace;
        }

        public void Draw(RenderWindow window, uint mapWidth, uint mapHeight, Enemies enemies)
        {
            this._enemy = enemies;
            this._enemySprite.Position = UpdatePlace(enemies.Place, mapWidth, mapHeight);
            _hitBoxEnnemi = _enemySprite.GetGlobalBounds();

            if (_animFrames == _nbSprite) _animFrames = 0;
            _enemySprite.TextureRect = new IntRect(_animFrames * _animStop, _direction, _spriteWidth, _spriteHeight);
            ++_animFrames;
            EffectOnSprite();

            _enemySprite.Draw(window, RenderStates.Default);
        }


        public void EffectOnSprite()
        {
            if (_enemy.Effect == "pyro_fruit"||_enemy.Effect == "FreezeGun")
            {
                if (_effectTime == 10)
                {
                    if (_enemy.Effect == "pyro_fruit")
                    {
                        _enemySprite.Color = Color.Red;
                    }else if(_enemy.Effect == "FreezeGun")
                    {
                        _enemySprite.Color = Color.Blue;
                    }
                    _effectTime = 0;
                }
                else
                {
                    _enemySprite.Color = colorCharacters;
                }
                _effectTime++;
            }
            else
            {
                _enemySprite.Color = colorCharacters;
            }
        }

        public Vector2f EnemyPosition
        {
            get { return _enemySprite.Position; }
        }

        public FloatRect HitBoxEnnemies => this._hitBoxEnnemi;
    }
}
