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
        private List<Text> athList = new List<Text>();
        private Font font = new Font("../../../Menu/arial.ttf");

        Round _ctx;

        public ATH(Round Context, float width, float height, View View)
        {
            _life = Context.Player.LifePlayer;
            _ammo = Context.Player.CurrentWeapon.Ammo;
            _ctx = Context;

            Text LifeBar = new Text
            {
                Font = font,
                Color = Color.White,
                DisplayedString = "PV: "+_life.ToString() + "/100",
                Position = new Vector2f(View.Size.X, View.Size.Y/3),
                
            };
            athList.Add(LifeBar);

            Text AmmoBar = new Text
            {
                Font = font,
                Color = Color.White,
                DisplayedString ="Ammo: " + _ammo.ToString(),
                Position = new Vector2f(View.Size.X, View.Size.Y / 2),

            };
            athList.Add(AmmoBar);

        }


        public void Draw(RenderWindow window)
        {
            for (int i = 0; i < athList.Count; i++)
            {
                window.Draw(athList[i]);
            }

        }

        public void UpdateATH(View view)
        {
            int b = 300;
            for (int i = 0; i < athList.Count; i++)
            {
                athList[i].Position = new Vector2f(view.Center.X + 200 * 2, view.Center.Y - b);
                b -= 100;
            }

            athList[0].DisplayedString = "PV: " + this._ctx.Player.LifePlayer.ToString() + "/100";

        }

        public List<Text> AthList => athList;

    }
}
