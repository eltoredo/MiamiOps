using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace MiamiOps
{
    public class WeaponUI
    {
        RoundUI _roundUIContext;

        Texture _weaponTexture;
        Sprite _weaponSprite;

        Texture _bulletTexture;
        Sprite _bulletSprite;
        int _nbSprite;
        int _spriteWidth;
        int _spriteHeight;
        int _animFrames;    // Number of animation frames (0 to 3 so a total of 4)
        int _nbDirection;
        int _direction;
        int _animStop;

        List<FloatRect> _bulletBoundingBox;
        bool reset;

        public WeaponUI(RoundUI roundUIContext, Texture weaponTexture, Texture bulletTexture, Vector weaponPlace, uint mapWidth, uint mapHeight)
        {
            _roundUIContext = roundUIContext;

            _weaponTexture = new Texture("../../../../Images/" + this._roundUIContext.RoundContext.Player.CurrentWeapon.Name + ".png");
            _weaponSprite = new Sprite(_weaponTexture);

            _bulletTexture = bulletTexture;
            _bulletSprite = new Sprite(_bulletTexture);

            _nbSprite = 3;

            _animFrames = 0;    // Basically, the player is not moving
            _direction = 2;
            _animStop = 0;

            _weaponSprite.Position = new Vector2f(((float)_roundUIContext.RoundContext.Player.Place.X + 3) * (mapWidth / 2), (float)_roundUIContext.RoundContext.Player.Place.Y * (mapHeight / 2));
            _bulletBoundingBox = new List<FloatRect>();
        }

        private Vector2f UpdatePlaceWeapon(uint mapWidth, uint mapHeight)
        {
            _nbDirection = Conversion(_roundUIContext.RoundContext.Player.Direction);
            return new Vector2f(((float)_roundUIContext.RoundContext.Player.Place.X +(float)1.01) * (mapWidth / 2), (((float)_roundUIContext.RoundContext.Player.Place.Y - (float)1.01) * (mapHeight / 2))*-1);
        }

        private Vector2f UpdatePlaceBullet(Shoot bullet, uint mapWidth, uint mapHeight)
        {
            return new Vector2f(((float)bullet.BulletPosition.X + 1) * (mapWidth / 2), (((float)bullet.BulletPosition.Y - 1) * (mapHeight / 2))*-1);
        }

        public void Draw(RenderWindow window, uint mapWidth, uint mapHeight)
        {
            this._weaponTexture.Dispose();
            this._weaponSprite.Dispose();

            _weaponTexture = new Texture("../../../../Images/" + this._roundUIContext.RoundContext.Player.CurrentWeapon.Name + ".png");
            _weaponSprite = new Sprite(_weaponTexture);

            this._spriteWidth = 32;
            this._spriteHeight = 32;
            _animStop = _spriteWidth;
            _direction = _spriteHeight * _nbDirection;

            this._weaponSprite.Position = UpdatePlaceWeapon(mapWidth, mapHeight);
            if (_animFrames == _nbSprite) _animFrames = 0;
            _weaponSprite.TextureRect = new IntRect(_animFrames * _animStop, _direction, _spriteWidth, _spriteHeight);
            ++_animFrames;
            _weaponSprite.Draw(window, RenderStates.Default);

            foreach (Shoot bullet in _roundUIContext.RoundContext.ListBullet)
            {
                _bulletSprite.Dispose();
                _bulletTexture.Dispose();
                _bulletTexture = new Texture("../../../../Images/" + this._roundUIContext.RoundContext.Player.CurrentWeapon.Name + "Bullet.png");
                _bulletSprite = new Sprite(_bulletTexture);
                if (reset == false)
                {
                    this._bulletBoundingBox.Clear();
                     reset = true;
                }

                this._bulletSprite.Position = UpdatePlaceBullet(bullet, mapWidth, mapHeight);
                this._bulletBoundingBox.Add(_bulletSprite.GetGlobalBounds());
                _bulletSprite.Draw(window, RenderStates.Default);
                
            }

            reset = false;
        }

        private int Conversion(Vector vector)
        {
            if (vector.X > 0) return 2;
            if (vector.X < 0) return 1;
            if (vector.Y < 0) return 0;
            if (vector.Y > 0) return 3;
            return 1;
        }

        public Vector2f WeaponPosition
        {
            get { return _weaponSprite.Position; }
        }

        public List<FloatRect> BoundingBoxBullet => this._bulletBoundingBox;
    }
}
