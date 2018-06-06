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
<<<<<<< HEAD
        RectangleShape playerBound = new RectangleShape();
=======
        Texture _monsterTexture = new Texture("../../../../Images/monstersprite.png");
        ATH _ath;
        View _view;
        View _viewATH;
>>>>>>> develop

        uint _mapWidth;
        uint _mapHeight;

        Round _roundCtx;

<<<<<<< HEAD
        private List<float[]> _obstacles;
        private List<RectangleShape> _drawObstacles = new List<RectangleShape>();


        //EnemiesUI = _enemiesUI;
=======
        Texture _stuffTexture = new Texture("../../../../Images/monstersprite.png");
        Sprite _stuffSprite = new Sprite();

>>>>>>> develop

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

        public RoundUI(Round roundCtx, Game gameCtx, uint mapWidth, uint mapHeight, Map mapCtx,uint screenWidth,uint screenHeight,View viewPlayer, View viewATH)
        {
            Texture _athLifeBar = new Texture("../../../../Images/HUD/LifeBar.png");

            Random _random = new Random();

            _roundCtx = roundCtx;

            Texture _weaponTexture = new Texture("../../../../Images/weaponsprite.png");
            Texture _bulletTexture = new Texture("../../../../Images/fireball.png");

            _gameCtx = gameCtx;
            _mapCtx = mapCtx;
            _view = viewPlayer;
            _viewATH = viewATH;
            _playerUI = new PlayerUI(this, 2, 3, 32, 32, new Vector(0, 0), mapWidth, mapHeight, mapCtx);

            _enemies = new EnemiesUI[_roundCtx.Enemies.Length];
            for (int i = 0; i < this._roundCtx.CountEnnemi; i++)
            {

                _enemies[i] = new EnemiesUI(this, _monsterTexture, 4, 54, 48, _roundCtx.Enemies[i].Place, mapWidth, mapHeight, mapCtx);
            }

            _ath = new ATH(_roundCtx, screenWidth, screenHeight,_view);
            _weaponUI = new WeaponUI(this, _weaponTexture, _bulletTexture, _roundCtx.Player.Place, mapWidth, mapHeight);

            _mapWidth = mapWidth;
            _mapHeight = mapHeight;
<<<<<<< HEAD
            foreach (var item in _roundCtx.Obstacles)
            {
                RectangleShape lol = new RectangleShape();
                Vector2f position = new Vector2f();
                Vector2f size = new Vector2f();


                float xPixel = ((item[0]) * 32) / 0.02f;
                float yPixel = ((item[1]-1) * 32) / 0.02f;

                position.X = xPixel;
                position.Y = yPixel*-1;

                lol.Position = position;

                float lengthPixel = (item[2] * 32) / 0.02f;
                float heigthPixel = (item[3] * 32) / 0.02f;

                size.X = lengthPixel;
                size.Y = heigthPixel;

                lol.Size = size;
                lol.FillColor = Color.Red;

                _drawObstacles.Add(lol);

            }

            //playerBound.Position = new Vector2f(1000, 1000);
            //playerBound.Size = new Vector2f(32, 32);
            //playerBound.FillColor = Color.Red;
           
     
=======
>>>>>>> develop
        }

        public void Draw(RenderWindow window, uint mapWidth, uint mapHeight)
        {
            _playerUI.Draw(window, mapWidth, mapHeight);
            _weaponUI.Draw(window, mapWidth, mapHeight);
<<<<<<< HEAD
            for (int i = 0; i < _roundCtx.Enemies.Length; i++) _enemies[i].Draw(window, mapWidth, mapHeight, _roundCtx.Enemies[i].Place);
            foreach (var item in _drawObstacles)
            {
                item.Draw(window, RenderStates.Default);
            }
            //playerBound.Position = new Vector2f(_playerUI.PlayerPosition.X, _playerUI.PlayerPosition.Y);
            //playerBound.Draw(window, RenderStates.Default);
=======
            _ath.Draw(window);
            for (int i = 0; i < this._roundCtx.CountEnnemi; i++) _enemies[i].Draw(window, mapWidth, mapHeight, _roundCtx.Enemies[i].Place);

            foreach (IStuff stuff in _roundCtx.StuffList)
            {
                _stuffTexture.Dispose();
                _stuffSprite.Dispose();
                _stuffTexture = new Texture("../../../../Images/" + stuff.Name + ".png");
                 _stuffSprite = new Sprite(_stuffTexture);
                _stuffSprite.Position = new Vector2f(((float)stuff.Position.X + 1) * (mapWidth / 2), (((float)stuff.Position.Y - 1) * (mapHeight / 2)) * (-1));
                _stuffSprite.Draw(window, RenderStates.Default);
            }
        }

        public void Update()
        {
            _ath.UpdateATH(this._view,MapWidth,MapHeight);
            UpdateSpawnEnnemie();
        }

        public void UpdateSpawnEnnemie()
        {
            if (this._roundCtx.Time == 120)
            {
                int index = this._roundCtx.CountEnnemi - this._roundCtx.SpawnCount;
                if(index < 0)
                {
                    index = 0;
                }

                for (int i = index; i < this._roundCtx.CountEnnemi; i++)
                {
                    _enemies[i] = new EnemiesUI(this, _monsterTexture, 4, 54, 48, _roundCtx.Enemies[i].Place, MapWidth, MapHeight, MapCtx);
                }
                this._roundCtx.Time = 0;
            }
>>>>>>> develop
        }

      
        public Map MapCtx => _mapCtx;
    }
}
