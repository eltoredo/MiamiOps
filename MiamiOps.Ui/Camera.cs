using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiamiOps
{
    public class Camera
    {

       

        public View CameraPlayerUpdate(float Xplayer,float Yplayer, uint XSizeMap,uint YSizeMap, View view)
        {

            if (Xplayer > 3160 / (float)1.24)//on the right side
            {
                Xplayer = 3160 / (float)1.24;
            }
            if (Xplayer < 3160 / (float)4.9)//on the right side
            {
                Xplayer = 3160 / (float)4.9;
            }

            if (Yplayer > 3160 / (float)1.113)//on the right side
            {
                Yplayer = 3160 / (float)1.113;
            }
            if (Yplayer < 3160 / (float)8.8)//on the right side
            {
                Yplayer = 3160 / (float)8.8;
            }

            Console.WriteLine("Size X :" + view.Size.X);
            Console.WriteLine("Size Y :" + view.Size.Y);
            Console.WriteLine("xplayer :" + Xplayer);
            Console.WriteLine("yplayer :" + Yplayer);
            Console.WriteLine();


            view.Center = new Vector2f(Xplayer, Yplayer);
            return view;
        }

    }
}
