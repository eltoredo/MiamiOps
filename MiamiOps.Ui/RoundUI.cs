﻿using SFML.Graphics;
using System;

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
            for (int i = 0; i < _roundCtx.Enemies.Length; i++) _enemies[i] = new EnemiesUI(this,_monsterTexture, 4, 54, 48, _roundCtx.Enemies[i].Place, mapWidth, mapHeight,mapCtx);

            _weaponUI = new WeaponUI(this, _roundCtx.Player.Place, mapWidth, mapHeight);

            _mapWidth = mapWidth;
            _mapHeight = mapHeight;
           
        }

        public void Draw(RenderWindow window, uint mapWidth, uint mapHeight)
        {
            _playerUI.Draw(window, mapWidth, mapHeight);
            _weaponUI.Draw(window, mapWidth, mapHeight);
            for (int i = 0; i < _roundCtx.Enemies.Length; i++) _enemies[i].Draw(window, mapWidth, mapHeight, _roundCtx.Enemies[i].Place);
        }
    }
}
