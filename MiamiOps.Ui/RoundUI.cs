using SFML.Graphics;
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
        EnemiesUI _enemiesUI;

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
            _playerUI = new PlayerUI(this, 2, 3, 33, 32, new Vector(0, 0), mapWidth, mapHeight);
            _enemiesUI = new EnemiesUI(this, 4, 54, 48, new Vector(0, 0), mapWidth, mapHeight);
            _roundCtx = roundCtx;

            _mapWidth = mapWidth;
            _mapHeight = mapHeight;
        }

        public void Draw(RenderWindow window, uint mapWidth, uint mapHeight)
        {
            //_enemiesUI.Draw();
            _playerUI.Draw(window, mapWidth, mapHeight);
            _enemiesUI.Draw(window, mapWidth, mapHeight);
        }
    }
}
