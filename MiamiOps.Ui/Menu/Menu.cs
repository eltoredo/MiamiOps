using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Audio;
using SFML.Window;
using SFML.System;


namespace MiamiOps
{
   public class Menu
    {
        private int selectedItemIndex;
        private Text[] menuList = new Text[3];
        private Texture[] buttonList = new Texture[3];
        private Font font = new Font("../../../Menu/arial.ttf");
        static Texture _backgroundTexture = new Texture("../../../../Images/background.png");
        //static Sprite _backgroundSprite;
        Music music = new Music("../../../Menu/TheLastOfUs.ogg");

        public Menu(float width, float height)
        {
            
            Text menu1 = new Text
            {
                Font = font,
                Color = Color.White,
                DisplayedString = "Nouvelle Partie",
                Position = new Vector2f( width/2  - width/10 , height - height / 3),
                
            };
            menuList[0] = menu1;
            
            Text menu2 = new Text
            {
                Font = font,
                Color = Color.White,
                DisplayedString = "Charger Partie",
                Position = new Vector2f(width / 2 - width / 10, height - height /4)
            };
            menuList[1] = menu2;

            Text menu3 = new Text
            {
                Font = font,
                Color = Color.White,
                DisplayedString = "Quitter",
                Position = new Vector2f(width / 2 - width / 18, height-height/6)
            };
            menuList[2] = menu3;
         //  _backgroundSprite = new Sprite(_backgroundTexture);zzzzz

        }

        public void Draw(RenderWindow window)
        {
           // _backgroundSprite.Draw(window, RenderStates.Default);
            for (int i = 0; i < menuList.Length; i++)
            {
                window.Draw(menuList[i]);
            }
           
        }

        public void Move(Keyboard.Key key)
        {
            if (key == Keyboard.Key.Z)
            {
                menuList[selectedItemIndex].Color = Color.White;
                selectedItemIndex--;
                if (selectedItemIndex < 0) selectedItemIndex = 2;
                menuList[selectedItemIndex].Color = Color.Red;
            }
           else  if (key == Keyboard.Key.S)
            {
                menuList[selectedItemIndex].Color = Color.White;
                selectedItemIndex++;
                if (selectedItemIndex > 2) selectedItemIndex = 0;
                menuList[selectedItemIndex].Color = Color.Red;
            }
        }

        public int SelectedItemIndex
        {
            get { return selectedItemIndex; }
            set { selectedItemIndex = value; }

        }

        public void PlaySoundMenu()
        { 
            music.Play();
        }

        public void StopSoundMenu()
        {
            music.Stop();
        }

    }
}
