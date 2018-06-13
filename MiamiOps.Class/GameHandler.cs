namespace MiamiOps
{
    public class GameHandler
    {
        Round _roundObject;
        Convert _convert;

        public GameHandler(Round roundObject, Convert convert)
        {
            _roundObject = roundObject;
            _convert = convert;
        }

        public void OnLeaving()
        {
            if (_roundObject.Stage <= 5) _roundObject = new Round(this, 10, enemieSpawn: new Vector(), enemiesSpeed: 0.0005f, playerSpeed: 0.005f, enemySpawn: _convert.ConvertXMLSpawn(@"..\..\..\..\MiamiOps.Map\Map\tilemap.tmx"), playerHauteur: 0f, playerLargeur: 0f, playerLife: 100);
        }

        public Round RoundObject => _roundObject;
    }
}
