using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiamiOps
{
    public class ATH
    {
        float _life;
        uint _ammo;
        int _lvl;
        float _galaxCoin;
        float _xp;

        private List<Text> _athList = new List<Text>();
        private Font _font = new Font("../../../Menu/pricedown.ttf");
        Texture _athLifeBar;
        Sprite _athLifeSprite;
        Texture _athGun;
        Sprite _athGunSprite;

        Vector2f _barPlace = new Vector2f(20, 50);
        RectangleShape _athBar;
        Color _couleur = new Color(255, 0, 0);

        Round _ctx;

        public ATH(Round Context, float width, float height, View View)
        {

            _ctx = Context;

            _life = _ctx.Player.LifePlayer;
            _ammo = _ctx.Player.CurrentWeapon.Ammo;
            _lvl = _ctx.Player.Level;
            _galaxCoin = _ctx.Player.Points;
            _xp = _ctx.Player.Experience;

            
            _athLifeBar = new Texture("../../../../Images/HUD/LifeBar.png");
            _athLifeSprite = new Sprite(_athLifeBar);
            _athGun = new Texture("../../../../Images/HUD/" + this._ctx.Player.CurrentWeapon.Name + ".png");
            _athGunSprite = new Sprite(_athGun);

            _athBar = new RectangleShape(_barPlace);
            _athBar.Size = new Vector2f(272, 40);
            _athBar.Position = new Vector2f(-1f, -0.5f);

            Text LifeBar = new Text
            {
                Font = _font,
                Color = Color.White,
                DisplayedString = "PV: "+_life.ToString() + "/100",
                
            };
            _athList.Add(LifeBar);

            Text AmmoBar = new Text
            {
                Font = _font,
                Color = Color.White,
                DisplayedString = "Ammo: " + _ammo.ToString(),

            };
            _athList.Add(AmmoBar);


            Text GalaxCoinBar = new Text
            {
                Font = _font,
                Color = Color.White,
                DisplayedString = "Points: " + _galaxCoin.ToString(),

            };
            _athList.Add(GalaxCoinBar);

            Text LvlBar = new Text
            {
                Font = _font,
                Color = Color.White,
                DisplayedString = "Lvl: " + _lvl.ToString(),

            };
            _athList.Add(LvlBar);

            Text XPBar = new Text
            {
                Font = _font,
                Color = Color.White,
                DisplayedString = "XP: " + _xp.ToString(),

            };
            _athList.Add(XPBar);
        }


        public void Draw(RenderWindow window)
        {
            
            _athBar.Draw(window, RenderStates.Default);

            for (int i = 0; i < _athList.Count; i++)
            {
                window.Draw(_athList[i]);
            }
                      
            _athLifeSprite.Draw(window, RenderStates.Default);
            _athGunSprite.Draw(window, RenderStates.Default);
            Console.WriteLine(_athBar.Position);
            Console.WriteLine(_athGunSprite.Scale.Y);           
        }

        public void UpdateATH(View view,uint mapWidth, uint mapHeigth)
        {

            int b = 3;
            bool a = true;
            for (int i = 0; i < _athList.Count; i++)
            {
                if (i > 1)
                {
                    if (a == true)
                    {
                        b = 2;
                    }
                    _athList[i].Position = new Vector2f(view.Center.X  - view.Size.X /b, view.Center.Y - view.Size.Y/2);
                    a = false;
                }
                else
                {
                    _athList[i].Position = new Vector2f(view.Center.X + view.Size.X / 3, view.Center.Y - view.Size.Y / b);
                }

                b++;
                
            }

              _athBar.Size = new Vector2f((this._ctx.Player.LifePlayer*275 / this._ctx.Player.LifePlayerMax),40);

          //  _athBar.Size = new Vector2f((this._ctx.Player.CurrentWeapon.Ammo*275 /30), 40);
            _athGun.Dispose();
            _athGunSprite.Dispose();
            _athGun = new Texture("../../../../Images/HUD/" + this._ctx.Player.CurrentWeapon.Name + ".png");
            _athGunSprite = new Sprite(_athGun);

            
            _athList[0].DisplayedString = this._ctx.Player.LifePlayer.ToString() + "/" + this._ctx.Player.LifePlayerMax.ToString();
            _athList[1].DisplayedString = this._ctx.Player.CurrentWeapon.Ammo.ToString() +"/"+ this._ctx.Player.CurrentWeapon.MaxAmmo.ToString();
            _athList[2].DisplayedString = "Points: " + this._ctx.Player.Points.ToString();
            _athList[3].DisplayedString = "Lvl: " + this._ctx.Player.Level.ToString();
            _athList[4].DisplayedString = "XP: " + this._ctx.Player.Experience.ToString()+"/"+this._ctx.Player.ExperienceMax.ToString();
            _athList[1].Position = new Vector2f(_athList[1].Position.X + 40, _athList[1].Position.Y);
            _athLifeSprite.Position = new Vector2f((float)_athList[0].Position.X - 100 , (float)_athList[0].Position.Y - 80);
            _athGunSprite.Position = new Vector2f((float)_athList[1].Position.X -100, (float)_athList[1].Position.Y);
            _athBar.FillColor = _couleur;
            _athBar.Position = new Vector2f((float)_athList[0].Position.X - 85, (float)_athList[0].Position.Y);


        }

        public List<Text> AthList => _athList;

    }
}
