using SFML.Graphics;
using SFML.System;
using System;
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
        Texture _monsterTexture = new Texture("../../../../Images/monstersprite.png");
        ATH _ath;
        View _view;
        View _viewATH;

        uint _mapWidth;
        uint _mapHeight;

        Round _roundCtx;

        Texture _stuffTexture;
       
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

            // Si c'est l'arme 1 soit le fusil d'assaut
            //if (_roundCtx.Player.CurrentWeapon == _roundCtx.Player.Weapons[0])
            //{
            Texture _bulletTexture = new Texture("../../../../Images/fireball.png");
            Texture _closeRangeWeaponTexture = new Texture("../../../../Images/weaponsprite.png");
            // }
            /*else if (_roundCtx.Player.CurrentWeapon == _roundCtx.Player.Weapons[1])
            {
                Texture _weaponTexture = new Texture("../../../../Images/weaponsprite.png");
                Texture _bulletTexture = new Texture("../../../../Images/fireball.png");
            }*/

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
            _weaponUI = new WeaponUI(this, _closeRangeWeaponTexture, _bulletTexture, _roundCtx.Player.Place, mapWidth, mapHeight);

            _mapWidth = mapWidth;
            _mapHeight = mapHeight;
        }

        public void Draw(RenderWindow window, uint mapWidth, uint mapHeight)
        {
            _playerUI.Draw(window, mapWidth, mapHeight);
            _weaponUI.Draw(window, mapWidth, mapHeight);
            _ath.Draw(window);
            for (int i = 0; i < this._roundCtx.CountEnnemi; i++) _enemies[i].Draw(window, mapWidth, mapHeight, _roundCtx.Enemies[i].Place);

            foreach (IStuff stuff in _roundCtx.StuffList)
            {
                _stuffTexture = new Texture("../../../../Images/" + stuff.Name + ".png");
                Sprite _stuffSprite = new Sprite(_stuffTexture);
                _stuffSprite.Position = new Vector2f((float)stuff.Position.X * (mapWidth / 2), (float)stuff.Position.Y * (mapHeight / 2));
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
        }

      
        public Map MapCtx => _mapCtx;
    }
}
