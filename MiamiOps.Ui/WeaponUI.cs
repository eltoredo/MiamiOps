﻿using System;
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

        List<Sprite> _bulletSpriteList;
        bool reset;

        public WeaponUI(RoundUI roundUIContext, Texture weaponTexture, Texture bulletTexture, Vector weaponPlace, uint mapWidth, uint mapHeight)
        {
            _roundUIContext = roundUIContext;

            _weaponTexture = new Texture("../../../../Images/" + this._roundUIContext.RoundContext.Player.CurrentWeapon.Name + ".png");
            _weaponSprite = new Sprite(_weaponTexture);

            _bulletTexture = bulletTexture;
            _bulletSprite = new Sprite(_bulletTexture);

            _weaponSprite.Position = new Vector2f(((float)_roundUIContext.RoundContext.Player.Place.X + 3) * (mapWidth / 2), (float)_roundUIContext.RoundContext.Player.Place.Y * (mapHeight / 2));
            _bulletSpriteList = new List<Sprite>();
        }

        private Vector2f UpdatePlaceWeapon(uint mapWidth, uint mapHeight)
        {
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
            this._weaponSprite.Position = UpdatePlaceWeapon(mapWidth, mapHeight);
            _weaponSprite.Draw(window, RenderStates.Default);

            foreach (Shoot bullet in _roundUIContext.RoundContext.Player.CurrentWeapon.Bullets)
            {
                if(reset == false)
                {
                    this._bulletSpriteList.Clear();
                     reset = true;
                }

                this._bulletSprite.Position = UpdatePlaceBullet(bullet, mapWidth, mapHeight);
                this._bulletSpriteList.Add(_bulletSprite);
                _bulletSprite.Draw(window, RenderStates.Default);
                
            }

            reset = false;
        }

        public Vector2f WeaponPosition
        {
            get { return _weaponSprite.Position; }
        }

        public List<Sprite> SpriteBulletList => this._bulletSpriteList;
    }
}
