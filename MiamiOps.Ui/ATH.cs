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
        public List<Text> AthList => _athList;

        private Font _font = new Font("../../../Menu/pricedown.ttf");
        Texture _athLifeBar;
        Sprite _athLifeSprite;
        Texture _athGun;
        Sprite _athGunSprite;
        RectangleShape _HPbar;
        RectangleShape _XPbar;

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

            _HPbar = new RectangleShape(new Vector2f(271, 40));
            _HPbar.FillColor = _couleur;

            _XPbar = new RectangleShape(new Vector2f(271, 30));
            _XPbar.FillColor = Color.Blue;


            Text LifeBar = new Text
            {
                Font = _font,
                Color = Color.White,
                             
            };
            _athList.Add(LifeBar);

            Text AmmoBar = new Text
            {
                Font = _font,
                Color = Color.White,
             
            };
            _athList.Add(AmmoBar);


            Text GalaxCoinBar = new Text
            {
                Font = _font,
                Color = Color.Green,
                DisplayedString = "Points:" + _galaxCoin.ToString(),

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
                CharacterSize = 25

            };
            _athList.Add(XPBar);
        }



        public void UpdateATH(View view, uint mapWidth, uint mapHeigth)
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
                    _athList[i].Position = new Vector2f(view.Center.X - view.Size.X / b, view.Center.Y - view.Size.Y / 2);
                    a = false;
                }
                else
                {
                    _athList[i].Position = new Vector2f(view.Center.X + view.Size.X / 3, (view.Center.Y - view.Size.Y / b));
                }

                b++;

            }

            _HPbar.Size = new Vector2f((this._ctx.Player.LifePlayer * 271 / this._ctx.Player.LifePlayerMax), 40);
            _XPbar.Size = new Vector2f((this._ctx.Player.Experience * 271 / this._ctx.Player.ExperienceMax), 25);


            //  _HPbar.Size = new Vector2f((this._ctx.Player.CurrentWeapon.Ammo*275 /30), 40);
            _athGun.Dispose();
            _athGunSprite.Dispose();
            _athGun = new Texture("../../../../Images/HUD/" + this._ctx.Player.CurrentWeapon.Name + ".png");
            _athGunSprite = new Sprite(_athGun);

            //player life
            _athList[0].DisplayedString = this._ctx.Player.LifePlayer.ToString() + "/" + this._ctx.Player.LifePlayerMax.ToString();

            //bullet count
            _athList[1].DisplayedString = this._ctx.Player.CurrentWeapon.Ammo.ToString() + "/" + this._ctx.Player.CurrentWeapon.MaxAmmo.ToString();

            //money
            _athList[2].DisplayedString = "Ω " + this._ctx.Player.Points.ToString();

            //current lvl
            _athList[3].DisplayedString = "Lvl: " + this._ctx.Player.Level.ToString();

            //current XP
            _athList[4].DisplayedString = this._ctx.Player.Experience.ToString() + "/" + this._ctx.Player.ExperienceMax.ToString();


            _athLifeSprite.Position = new Vector2f(_athList[0].Position.X - 100, _athList[0].Position.Y - 100);
            _athGunSprite.Position = new Vector2f(_athLifeSprite.Position.X +170, _athLifeSprite.Position.Y - 10 );
            _XPbar.Position = new Vector2f(_athList[0].Position.X - 87, _athList[0].Position.Y + 20);
            _HPbar.Position = new Vector2f(_athList[0].Position.X - 85, _athList[0].Position.Y - 20);
            

            _athList[0].Position = new Vector2f(_athList[0].Position.X +10, _athList[0].Position.Y -20);
            _athList[1].Position = new Vector2f(_athGunSprite.Position.X - 80, _athGunSprite.Position.Y + 20);
            _athList[2].Position = new Vector2f(_athList[0].Position.X +70 , _athList[0].Position.Y +70);
            _athList[3].Position = new Vector2f(_athList[0].Position.X - 70, _athList[0].Position.Y + 70);
            _athList[4].Position = new Vector2f(_athList[0].Position.X + 10, _athList[0].Position.Y + 40);


        }


        public void Draw(RenderWindow window)
        {

            _HPbar.Draw(window, RenderStates.Default);
            _XPbar.Draw(window, RenderStates.Default);


            for (int i = 0; i < _athList.Count; i++)
            {
                window.Draw(_athList[i]);
            }

            _athLifeSprite.Draw(window, RenderStates.Default);
            _athGunSprite.Draw(window, RenderStates.Default);

        }



    }
}
