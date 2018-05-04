using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiamiOps
{
    public class Camera
    {

        float xcamera;
        float ycamera;

        public View CameraPlayerUpdate(float xplayer,float yplayer, uint screenwidth,uint screenheight, View view)
        {

            xcamera = xplayer;
            ycamera = yplayer;

            //if (xplayer > xplayer - 3160/2)//on the right side
            //{
            //    xcamera = xplayer - 3160 ;
            //}
            //if (xplayer < 3160 - view.Size.X / 2)//on the left side
            //{
            //    xcamera = 3160 - view.Size.X / 2;
            //}

            //if (yplayer > 3160 - view.Size.Y / 2)//on the right side
            //{
            //    ycamera = 3160 - view.Size.Y / 2;
            //}
            //if (yplayer < 3160 - view.Size.Y / 2)//on the left side
            //{
            //    ycamera = 3160 - view.Size.Y / 2;
            //}

            //Console.WriteLine("Size X :" + view.Size.X);
            //Console.WriteLine("Size Y :" + view.Size.Y);
            //Console.WriteLine("xplayer :" + xplayer);
            //Console.WriteLine("yplayer :" + yplayer);
            //Console.WriteLine();

            view.Center = new Vector2f(xcamera, ycamera);
            return view;
        }

    }
}
