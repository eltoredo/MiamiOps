using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiamiOps
{
   public class GameOver
    {
        private int selectedItemIndex;
        private Text[] GameOverList = new Text[2];
        private Texture[] buttonList = new Texture[3];
        private Font font = new Font("../../../Menu/arial.ttf");
        static Texture _backgroundTexture = new Texture("../../../../Images/game_over_screen.png");
        static Sprite _backgroundSprite;
        Music music = new Music("../../../Menu/DarkSoulsDie.ogg");
        Text _continue;

        public GameOver(float width, float height)
        {

            selectedItemIndex = 0;
            Text GameOvermenu1 = new Text
            {
                Font = font,
                Color = Color.White,
                DisplayedString = "Yes",
                Position = new Vector2f(300,600),

            };
            GameOverList[0] = GameOvermenu1;

            Text GameOvermenu2 = new Text
            {
                Font = font,
                Color = Color.White,
                DisplayedString = "No(return to title)",
                Position = new Vector2f(900, 600)
            };
            GameOverList[1] = GameOvermenu2;

            Text Continue = new Text
            {
                Font = font,
                Color = Color.White,
                DisplayedString = "Continue ?",
                Position = new Vector2f(width / 2 - width / 10 + 50, height - height / 4)

            };
            _continue = Continue;
             _backgroundSprite = new Sprite(_backgroundTexture);

        }

        public void Draw(RenderWindow window)
        {
            _backgroundSprite.Draw(window, RenderStates.Default);
            for (int i = 0; i < GameOverList.Length; i++)
            {
                window.Draw(GameOverList[i]);
            }
            window.Draw(this._continue);
        }

        public void Move(Keyboard.Key key)
        {
            if (key == Keyboard.Key.Q)
            {
                GameOverList[selectedItemIndex].Color = Color.White;
                selectedItemIndex--;
                if (selectedItemIndex < 0) selectedItemIndex = 0;
                GameOverList[selectedItemIndex].Color = Color.Yellow;
            }
            else if (key == Keyboard.Key.D)
            {
                GameOverList[selectedItemIndex].Color = Color.White;
                selectedItemIndex++;
                if (selectedItemIndex > 1) selectedItemIndex = 1;
                GameOverList[selectedItemIndex].Color = Color.Yellow;
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

