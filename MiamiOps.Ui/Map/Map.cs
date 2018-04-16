using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.System;
using SFML.Graphics;
using SFML.Audio;
using System.IO;
using System.Xml.Linq;

namespace MiamiOps
{
    public class Map : Drawable
    {
        private Texture tileset;
        private VertexArray vertexArray;

        private int width;
        private int height;
        private Vector2u tileSize;
        private int tilewidth;
        private int tileheight;
        private string[] level_array;
        private List<int> idMap = new List<int>();

        public Map(String XML)
        {

            using (FileStream fs = File.OpenRead(XML))
            using (StreamReader sr = new StreamReader(fs, true))
            {
                XElement xml = XElement.Load(sr);
                string level = xml.Descendants("data").Single().Value;
                level_array = level.Split(',');
                width = 100;
                height = 100;
                tileSize = new Vector2u(64, 64);
                tileset = new Texture(@"..\..\..\tileset1.png");
                ConstructMap();
            }
           
        }

        public void ConstructMap()
        {

            vertexArray = new VertexArray(PrimitiveType.Quads, (uint)(width * height * 4));
            int a = 0;


            for (uint y = 0; y < width; y++)
            {
                for (uint x = 0; x < height; x++)
                {
                  
                    int tileID = Int32.Parse(LevelArray[a]);

                    if (tileID != 0)
                    {

                        long tu = tileID % (tileset.Size.X / tileSize.X) - 1;
                        long tv = tileID / (tileset.Size.X / tileSize.X);
                        uint index = (uint)(x + y * width) * 4;


                        vertexArray[index + 0] = new Vertex(new Vector2f(x * tileSize.X, y * tileSize.Y), new Vector2f(tu * tileSize.X, tv * tileSize.Y));
                        vertexArray[index + 1] = new Vertex(new Vector2f((x + 1) * tileSize.X, y * tileSize.Y), new Vector2f((tu + 1) * tileSize.X, tv * tileSize.Y));
                        vertexArray[index + 2] = new Vertex(new Vector2f((x + 1) * tileSize.X, (y + 1) * tileSize.Y), new Vector2f((tu + 1) * tileSize.X, (tv + 1) * tileSize.Y));
                        vertexArray[index + 3] = new Vertex(new Vector2f(x * tileSize.X, (y + 1) * tileSize.Y), new Vector2f(tu * tileSize.X, (tv + 1) * tileSize.Y));
                    }
                        a++;
                    
                }
            }
        }

        public string [] LevelArray
        {
            get { return level_array; }
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Texture = tileset;
            target.Draw(vertexArray, states);
        }
    }
}