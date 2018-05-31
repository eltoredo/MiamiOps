using System;

using SFML.Graphics;
using SFML.System;

namespace MiamiOps
{
    public class WeaponUI
    {
        RoundUI _roundUIContext;

        Texture _weaponTexture;
        Sprite _weaponSprite;

        public WeaponUI(RoundUI roundUIContext, Vector weaponPlace, uint mapWidth, uint mapHeight)
        {
            _roundUIContext = roundUIContext;

            _weaponTexture = new Texture("../../../../Images/weaponsprite.png");
            _weaponSprite = new Sprite(_weaponTexture);

            _weaponSprite.Position = new Vector2f(((float)_roundUIContext.RoundContext.Player.Place.X + 3) * (mapWidth / 2), (float)_roundUIContext.RoundContext.Player.Place.Y * (mapHeight / 2));
        }

        private Vector2f UpdatePlace(uint mapWidth, uint mapHeight)
        {
            return new Vector2f(((float)_roundUIContext.RoundContext.Player.Place.X + (float)1.03) * (mapWidth / 2), (((float)_roundUIContext.RoundContext.Player.Place.Y + (float)0.98) * (mapHeight / 2)))*-1;
        }

        public void Draw(RenderWindow window, uint mapWidth, uint mapHeight)
        {
            this._weaponSprite.Position = UpdatePlace(mapWidth, mapHeight);

            _weaponSprite.Draw(window, RenderStates.Default);
        }

        public Vector2f WeaponPosition
        {
            get { return _weaponSprite.Position; }
        }
    }
}
