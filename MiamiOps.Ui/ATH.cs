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

        public ATH(Round Context, float width, float height)
        {
            _life = Context.Player.LifePlayer;
            _ammo = Context.Player.CurrentWeapon.Ammo;

            Text LifeBar = new Text
            {
                Font = font,
                Color = Color.White,
                DisplayedString = "PV: "+_life.ToString() + "/100",
                Position = new Vector2f(width / 2 - width / 10, height - height / 3),
                
            };
            athList.Add(LifeBar);

            Text AmmoBar = new Text
            {
                Font = font,
                Color = Color.White,
                DisplayedString ="Ammo: " + _ammo.ToString(),
                Position = new Vector2f(width / 2 - width / 10, height - height / 4),

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

        public List<Text> AthList => athList;

    }
}
