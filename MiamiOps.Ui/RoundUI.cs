﻿using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiamiOps
{
    public class RoundUI
    {
        PlayerUI _playerUI;

        EnemiesUI[] _enemies;

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

        public RoundUI(Round roundCtx, uint mapWidth, uint mapHeight)
        {
            Random _random = new Random();

            _playerUI = new PlayerUI(this, 2, 3, 33, 32, new Vector(0, 0), mapWidth, mapHeight);

            _roundCtx = roundCtx;

            _enemies = new EnemiesUI[_roundCtx.Enemies.Length];
            for (int i = 0; i < _roundCtx.Enemies.Length; i++) _enemies[i] = new EnemiesUI(this, 4, 54, 48, _roundCtx.Enemies[i].Place, mapWidth, mapHeight);

            _mapWidth = mapWidth;
            _mapHeight = mapHeight;
        }

        public void Draw(RenderWindow window, uint mapWidth, uint mapHeight)
        {
            _playerUI.Draw(window, mapWidth, mapHeight);
            for (int i = 0; i < _roundCtx.Enemies.Length; i++) _enemies[i].Draw(window, mapWidth, mapHeight, _roundCtx.Enemies[i].Place;
        }
    }
}
