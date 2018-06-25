using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
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
        RectangleShape playerBound = new RectangleShape();
        Texture _monsterTexture = new Texture("../../../../Images/monstersprite.png");
        ATH _ath;
        View _view;
        View _viewATH;
        bool reset;
        bool _musicReset;

        List<FloatRect> _boundingBoxPackage;

        uint _mapWidth;
        uint _mapHeight;
        Round _roundCtx;
        Music _effectMusic;
        private List<float[]> _obstacles;
        private List<RectangleShape> _drawObstacles = new List<RectangleShape>();


        Texture _stuffTexture = new Texture("../../../../Images/monstersprite.png");
        Sprite _stuffSprite = new Sprite();

        Texture _doorTexture;
        Sprite _doorSprite;

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

            Texture _weaponTexture = new Texture("../../../../Images/soulcalibur.png");
            Texture _bulletTexture = new Texture("../../../../Images/fireball.png");

            _doorTexture = new Texture("../../../../Images/doortextureclosed.png");
            _doorSprite = new Sprite(_doorTexture);

            _musicReset = false;
            _effectMusic = new Music("../../../../Images/brute.ogg");
            _gameCtx = gameCtx;
            _mapCtx = mapCtx;
            _view = viewPlayer;
            _viewATH = viewATH;
            _playerUI = new PlayerUI(this, 2, 3, 32, 32, new Vector(0, 0), mapWidth, mapHeight, mapCtx);
            _boundingBoxPackage = new List<FloatRect>();
            _enemies = new EnemiesUI[_roundCtx.Enemies.Length];
            for (int i = 0; i < this._roundCtx.CountEnnemi; i++)
            {

                _enemies[i] = new EnemiesUI(this, _monsterTexture, 4, 54, 48, _roundCtx.Enemies[i], mapWidth, mapHeight, mapCtx);
            }

            _ath = new ATH(_roundCtx, screenWidth, screenHeight,_view);
            _weaponUI = new WeaponUI(this, _weaponTexture, _bulletTexture, _roundCtx.Player.Place, mapWidth, mapHeight);

            _mapWidth = mapWidth;
            _mapHeight = mapHeight;
            foreach (var item in _roundCtx.Obstacles)
            {
                RectangleShape lol = new RectangleShape();
                Vector2f position = new Vector2f();
                Vector2f size = new Vector2f();


                float xPixel = ((item[0]+1) * 32) / 0.02f;
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

                //_drawObstacles.Add(lol);

            }

            //playerBound.Position = new Vector2f(1000, 1000);
            //playerBound.Size = new Vector2f(32, 32);
            //playerBound.FillColor = Color.Red;
           
     
        }

        public void Draw(RenderWindow window, uint mapWidth, uint mapHeight)
        {

            if (_roundCtx.IsDoorOpened == false)
            {
                _doorTexture.Dispose();
                _doorSprite.Dispose();
                _doorTexture = new Texture("../../../../Images/doortextureclosed.png");
                _doorSprite = new Sprite(_doorTexture);
            }
            else
            {
                _doorTexture.Dispose();
                _doorSprite.Dispose();
                _doorTexture = new Texture("../../../../Images/doortextureopened.png");
                _doorSprite = new Sprite(_doorTexture);
            }

            _doorSprite.Position = new Vector2f(mapWidth / 2, mapHeight / 2);
            _doorSprite.Draw(window, RenderStates.Default);
            FloatRect _hitBoxDoor = _doorSprite.GetGlobalBounds();
            
            for (int i = 0; i < this._roundCtx.CountEnnemi; i++) _enemies[i].Draw(window, mapWidth, mapHeight, _roundCtx.Enemies[i]);
            foreach (var item in _drawObstacles)
            {
                item.Draw(window, RenderStates.Default);
            }
            _playerUI.Draw(window, mapWidth, mapHeight);
            _weaponUI.Draw(window, mapWidth, mapHeight);

            //playerBound.Position = new Vector2f(_playerUI.PlayerPosition.X, _playerUI.PlayerPosition.Y);
            //playerBound.Draw(window, RenderStates.Default);

            //Dessine Tous les stuffs et construit un tableau de FloatRect qui comporte tous les BoundingBox des stuffs
            foreach (IStuff stuff in _roundCtx.StuffList)
            {
                if (reset == false)
                {
                    this._boundingBoxPackage.Clear();
                    reset = true;
                }
                _stuffTexture.Dispose();
                _stuffSprite.Dispose();
                _stuffTexture = new Texture("../../../../Images/" + stuff.Name + ".png");
                _stuffSprite = new Sprite(_stuffTexture);
                _stuffSprite.Position = new Vector2f(((float)stuff.Position.X + 1) * (mapWidth / 2), (((float)stuff.Position.Y - 1) * (mapHeight / 2)) * (-1));
                _boundingBoxPackage.Add(_stuffSprite.GetGlobalBounds());
                _stuffSprite.Draw(window, RenderStates.Default);
            }


            _ath.Draw(window);

            CollideToPackage();

            CollideToShootEnnemiesAndPlayerToEnnemies();
           
            if (this._playerUI.HitBoxPlayer.Intersects(_hitBoxDoor) && this.RoundContext.IsDoorOpened == true)
            {
                this.RoundContext.LevelPass = true;
            }
            //else if(this._playerUI.HitBoxPlayer.Intersects(_hitBoxDoor) && this.RoundContext.IsDoorOpened == false)
            //{
            //    Console.WriteLine("ZA WARUDO");
            //}
        }

        public void Update()
        {
            _ath.UpdateATH(this._view,MapWidth,MapHeight);
            UpdateSpawnEnnemie();
            UpdateMusic();

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
                    _enemies[i] = new EnemiesUI(this, _monsterTexture, 4, 54, 48, _roundCtx.Enemies[i], MapWidth, MapHeight, MapCtx);
                }
                this._roundCtx.Time = 0;
            }
        }

        public void UpdateMusic()
        {
           if(_roundCtx.Player.Effect == "brute"||
             _roundCtx.Player.CurrentWeapon.Name == "chaos_blade"||
             _roundCtx.Player.CurrentWeapon.Name == "soulcalibur"||
             _roundCtx.Player.Effect == "pyro_fruit"||
             _roundCtx.Player.CurrentWeapon.Name == "FreezeGun"
              )
            {
                GameCtx.MusicMain.Pause();
            }
            else if(_musicReset == true)
            {
                this._effectMusic.Stop();
                GameCtx.MusicMain.Play();
                _musicReset = false;
            }

        }

        public void CollideToPackage()
        {

            reset = false;

            //Verifie si les Bounding box des stuffs collisione avec le personnage
            int count = 0;
            foreach (var item in _boundingBoxPackage)
            {
                count++;
                if (this._playerUI.HitBoxPlayer.Intersects(item))
                {
                    if (_roundCtx.StuffList.Count != 0)
                    {
                        string Music = "../../../../Images/" + _roundCtx.StuffList[count - 1].Name + ".ogg";
                        if (File.Exists(Music))
                        {
                            this._effectMusic.Dispose();
                            this._effectMusic = new Music("../../../../Images/" + _roundCtx.StuffList[count - 1].Name + ".ogg");
                            _effectMusic.Play();
                        }

                        if (_roundCtx.StuffList[count - 1].Name != "speed"||
                            _roundCtx.StuffList[count - 1].Name != "health"||
                            _roundCtx.StuffList[count - 1].Name != "point"
                            ) _musicReset = true;

                        _roundCtx.StuffList[count - 1].WalkOn(_roundCtx);
                        break;

                    }
                }

            }
        }

        public void CollideToShootEnnemiesAndPlayerToEnnemies()
        {
            // verifie que les tirs collisionne avec les ennemis
            for (int i = 0; i < this._roundCtx.CountEnnemi; i++)
            {
                for (int a = 0; a < this._weaponUI.BoundingBoxBullet.Count; a++)
                {
                    if (this._weaponUI.BoundingBoxBullet.Count > 0)
                    {
                        if (this._weaponUI.BoundingBoxBullet[a].Intersects(_enemies[i].HitBoxEnnemies))
                        {
                            if(this.RoundContext.Player.CurrentWeapon.Name == "FreezeGun")
                            {
                                _roundCtx.Enemies[i].Effect = "FreezeGun";
                                _roundCtx.Enemies[i].CreationDateEffect = DateTime.UtcNow;
                                _roundCtx.Enemies[i].LifeSpanEffect = TimeSpan.FromSeconds(3);
                            }
                                _roundCtx.Enemies[i].Hit((float)_roundCtx.Player.CurrentWeapon.Attack);
                                _roundCtx.ListBullet.RemoveAt(a);
                                this._weaponUI.BoundingBoxBullet.RemoveAt(a);
                                break;
                        }
                    }
                }


                //verifie que le player colisione avec les ennemis
                if (this._playerUI.HitBoxPlayer.Intersects(_enemies[i].HitBoxEnnemies))
                {
                    if (_roundCtx.Player.Effect == "brute")
                    {
                        _roundCtx.Enemies[i].Hit((float)_roundCtx.Enemies[i].Life);

                    }
                    else if(_roundCtx.Player.Effect == "pyro_fruit")
                    {
                        _roundCtx.Enemies[i].Effect = "pyro_fruit";
                        _roundCtx.Enemies[i].CreationDateEffect = DateTime.UtcNow;
                        _roundCtx.Enemies[i].LifeSpanEffect = TimeSpan.FromSeconds(3);

                    }
                    else
                    {
                        _roundCtx.Player.LifePlayer -= 1;
                    }

                    if (_roundCtx.Player.LifePlayer <= 0)
                    {
                        GameCtx.MusicMain.Stop();
                        _roundCtx.GameState = true;
                    }
                }

            }
        }


        public Map MapCtx => _mapCtx;
        public Music EffectMusic
        {
            get { return _effectMusic; }
            set { _effectMusic = value; }
        }
    }
}
