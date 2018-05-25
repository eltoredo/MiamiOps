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
        private List<Text> _athList = new List<Text>();
        private Font _font = new Font("../../../Menu/arial.ttf");
        Texture _athLifeBar;
        Sprite _athLifeSprite;
        Texture _athGun;
        Sprite _athGunSprite;

        Round _ctx;

        public ATH(Round Context, float width, float height, View View)
        {
            _life = Context.Player.LifePlayer;
            _ammo = Context.Player.CurrentWeapon.Ammo;
            _ctx = Context;
            _athLifeBar = new Texture("../../../../Images/LifeBar.png");
            _athLifeSprite = new Sprite(_athLifeBar);
            _athGun = new Texture("../../../../Images/Gun.png");
            _athGunSprite = new Sprite(_athGun);
            _athGunSprite.Scale = new Vector2f(0.4f, 0.4f);

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

        }


        public void Draw(RenderWindow window)
        {
            for (int i = 0; i < _athList.Count; i++)
            {
                window.Draw(_athList[i]);
            }

            _athLifeSprite.Draw(window, RenderStates.Default);
            _athGunSprite.Draw(window, RenderStates.Default);
            Console.WriteLine(_athGunSprite.Scale.X);
            Console.WriteLine(_athGunSprite.Scale.Y);


        }

        public void UpdateATH(View view,uint mapWidth, uint mapHeigth)
        {
            int b = 3;
            for (int i = 0; i < _athList.Count; i++)
            {
                _athList[i].Position = new Vector2f(view.Center.X + view.Size.X/3,view.Center.Y - view.Size.Y/b);
                b++;
            }
           
            _athList[0].DisplayedString = this._ctx.Player.LifePlayer.ToString() + "/100";
            _athList[1].DisplayedString = this._ctx.Player.CurrentWeapon.Ammo.ToString() + "/30";
            _athList[1].Position = new Vector2f(_athList[1].Position.X + 40, _athList[1].Position.Y);
            _athLifeSprite.Position = new Vector2f((float)_athList[0].Position.X - 100 , (float)_athList[0].Position.Y - 80);
            _athGunSprite.Position = new Vector2f((float)_athList[1].Position.X -100, (float)_athList[1].Position.Y);

        }

        public List<Text> AthList => _athList;

    }
}
