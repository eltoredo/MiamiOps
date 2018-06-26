namespace MiamiOps
{
    public class GameHandler
    {
        Round _roundObject;
        bool _hasLeft;

        Convert _convert;

        public GameHandler(Convert convert)
        {
            _convert = convert;

            _roundObject = new Round(this, 10, enemieSpawn: new Vector(), enemiesSpeed: 0.0005f, playerSpeed: 0.005f, enemySpawn: _convert.ConvertXMLSpawn(@"..\..\..\..\MiamiOps.Map\Map\tilemap.tmx"), playerHauteur: 0f, playerLargeur: 0f, playerLife: 100,enemiesLife:10);
        }

        public void OnLeaving()
        {
            _roundObject = new Round(this, 10, enemieSpawn: new Vector(), enemiesSpeed: 0.0005f, playerSpeed: 0.005f, enemySpawn: _convert.ConvertXMLSpawn(@"..\..\..\..\MiamiOps.Map\Map\tilemap.tmx"), playerHauteur: 0f, playerLargeur: 0f, playerLife: 100);
            _hasLeft = true;
        }

        public Round RoundObject => _roundObject;
        public bool HasLeft
        {
            get { return _hasLeft; }
            set { _hasLeft = value; }
        }
    }
}
