using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MiamiOps
{
    public class RoundUI
    {
        PlayerUI _playerUI;
        EnemiesUI[] _enemies;
        WeaponUI _weaponUI;
        Game _gameCtx;
        Map _mapCtx;

        uint _mapWidth;
        uint _mapHeight;

        Round _roundCtx;

        private List<float[]> _obstacles;
        private List<RectangleShape> _drawObstacles = new List<RectangleShape>();


        //EnemiesUI = _enemiesUI;

        public Round RoundContext
        {
            get { return _roundCtx; }
        }

        public uint MapWidth
        {
            get { return _mapWidth; }
        }

        public uint MapHeight
        {
            get { return _mapHeight; }
        }

        public PlayerUI PlayerUI
        {
            get { return _playerUI; }
        }

        public Game GameCtx
        {
            get { return _gameCtx; }
        }

        public RoundUI(Round roundCtx, Game gameCtx, uint mapWidth, uint mapHeight, Map mapCtx)
        {
            Texture _monsterTexture = new Texture("../../../../Images/monstersprite.png");

            Random _random = new Random();

            _roundCtx = roundCtx;
            _gameCtx = gameCtx;
            _mapCtx = mapCtx;
            _playerUI = new PlayerUI(this, 2, 3, 33, 32, new Vector(0, 0), mapWidth, mapHeight, mapCtx);

            _enemies = new EnemiesUI[_roundCtx.Enemies.Length];
            for (int i = 0; i < _roundCtx.Enemies.Length; i++)
            {
                
                _enemies[i] = new EnemiesUI(this, _monsterTexture, 4, 54, 48, _roundCtx.Enemies[i].Place, mapWidth, mapHeight, mapCtx);

            }
            _weaponUI = new WeaponUI(this, _roundCtx.Player.Place, mapWidth, mapHeight);

            _mapWidth = mapWidth;
            _mapHeight = mapHeight;
            foreach (var item in _roundCtx.Obstacles)
            {
                RectangleShape lol = new RectangleShape();
                Vector2f position = new Vector2f();
                Vector2f size = new Vector2f();


                float xPixel = (item[0] * 32) / 0.02f;
                float yPixel = (item[1] * 32) / 0.02f;

                position.X = xPixel;
                position.Y = yPixel * -1;

                lol.Position = position;

                float lengthPixel = (item[2] * 32) / 0.02f;
                float heigthPixel = (item[3] * 32) / 0.02f;

                size.X = lengthPixel;
                size.Y = heigthPixel;

                lol.Size = size;
                lol.FillColor = Color.Red;

                _drawObstacles.Add(lol);


            }

        }

        public void Draw(RenderWindow window, uint mapWidth, uint mapHeight)
        {
            _playerUI.Draw(window, mapWidth, mapHeight);
            _weaponUI.Draw(window, mapWidth, mapHeight);
            for (int i = 0; i < _roundCtx.Enemies.Length; i++) _enemies[i].Draw(window, mapWidth, mapHeight, _roundCtx.Enemies[i].Place);
            foreach (var item in _drawObstacles)
            {
                item.Draw(window, RenderStates.Default);
            }
        }
    }
}
