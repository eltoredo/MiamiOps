using System.Collections.Generic;

namespace MiamiOps
{
    public class GameHandler
    {
        Round _roundObject;
        bool _hasLeft;

        Convert _convert;

        HashSet<float[]> _collide;
        Map _map;

        public GameHandler(Convert convert)
        {
            _convert = convert;

            _roundObject = new Round(this, 10, 1, 1, null, enemieSpawn: new Vector(), enemiesSpeed: 0.0005f, playerSpeed: 0.005f, enemySpawn: _convert.ConvertXMLSpawn(@"..\..\..\..\MiamiOps.Map\Map\stage1-1.tmx"), playerHauteur: 0f, playerLargeur: 0f, playerLife: 100,enemiesLife:10);

            _collide = _convert.ConvertXMLCollide(@"..\..\..\..\MiamiOps.Map\Map\stage1-1.tmx");
            _map = new Map(@"..\..\..\..\MiamiOps.Map\Map\stage1-1.tmx", @"..\..\..\..\MiamiOps.Map\Map\tileset2.png");
        }

        public void OnLeaving()
        {
            _roundObject = new Round(this, 10, _roundObject.Stage, _roundObject.Level, _roundObject.StuffFactories, _roundObject.Player, enemieSpawn: new Vector(), enemiesSpeed: 0.0005f, playerSpeed: 0.005f, enemySpawn: _convert.ConvertXMLSpawn(@"..\..\..\..\MiamiOps.Map\Map\stage" + _roundObject.Level + "-" + _roundObject.Stage + ".tmx"), playerHauteur: 0f, playerLargeur: 0f, playerLife: 100);
            _collide = _convert.ConvertXMLCollide(@"..\..\..\..\MiamiOps.Map\Map\stage"+_roundObject.Level+"-"+_roundObject.Stage+".tmx");
            _map = new Map(@"..\..\..\..\MiamiOps.Map\Map\stage" + _roundObject.Level + "-" + _roundObject.Stage + ".tmx", @"..\..\..\..\MiamiOps.Map\Map\MiamiOPSlvl1.png");
            _hasLeft = true;
        }

        public Map Map => _map;
        public Round RoundObject => _roundObject;
        public bool HasLeft
        {
            get { return _hasLeft; }
            set { _hasLeft = value; }
        }
    }
}
