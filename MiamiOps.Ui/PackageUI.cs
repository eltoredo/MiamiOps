using SFML.Graphics;
using SFML.System;

namespace MiamiOps
{
    public class PackageUI
    {
        RoundUI _roundUIContext;

        Texture _packageTexture;
        Sprite _packageSprite;

        public PackageUI(RoundUI roundUIContext, Texture packageTexture, Vector packagePlace, uint mapWidth, uint mapHeight)
        {
            _roundUIContext = roundUIContext;

            _packageTexture = packageTexture;
            _packageSprite = new Sprite(_packageTexture);
        }

        public void Draw(RenderWindow window, uint mapWidth, uint mapHeight)
        {
            //this._packageSprite.Position = x;
            _packageSprite.Draw(window, RenderStates.Default);
        }

        public Vector2f PackagePosition
        {
            get { return _packageSprite.Position; }
        }
    }
}
