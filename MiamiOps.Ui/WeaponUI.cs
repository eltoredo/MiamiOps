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

            _weaponSprite.Position = new Vector2f((float)weaponPlace.X * (mapWidth / 2), (float)weaponPlace.Y * (mapHeight / 2));
        }

        private Vector2f UpdatePlace(Vector weaponPlace, uint mapWidth, uint mapHeight)
        {
            return new Vector2f(((float)weaponPlace.X + 1) * (mapWidth / 2), ((float)weaponPlace.Y + 1) * (mapHeight / 2));
        }

        public void Draw(RenderWindow window, uint mapWidth, uint mapHeight, Vector position)
        {
            this._weaponSprite.Position = UpdatePlace(position, mapWidth, mapHeight);

            _weaponSprite.Draw(window, RenderStates.Default);
        }

        public Vector2f WeaponPosition
        {
            get { return _weaponSprite.Position; }
        }
    }
}
