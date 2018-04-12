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
    class Map
    {
        private Texture tileset;
        private VertexArray vertexArray;

        private int width;
        private int height;
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
                string[] level_array = level.Split(',');
            }

            width = 100;
            height = 100;
            tilewidth = 64;
            tileheight = 64;
            tileset = new Texture("maptest.tsx");
           
        }

        public void ConstructMap()
        {

            vertexArray = new VertexArray(PrimitiveType.Quads, (uint)(width * height * 4));

            int a = 0;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int tileID = Int32.Parse(level_array[a]);
                    a++;
                }
            }
        }

        private void AddBlackTile(Tile tile, Vector2f position)
        {
            vertexArray.Append(new Vertex((new Vector2f(0.0f, 0.0f) + position) * tileDimension,
                new Vector2f(tileDimension * tile.X, tileDimension * tile.Y)));
            vertexArray.Append(new Vertex((new Vector2f(1.0f, 0.0f) + position) * tileDimension,
                new Vector2f(tileDimension * tile.X + tileDimension, tileDimension * tile.Y)));
            vertexArray.Append(new Vertex((new Vector2f(1.0f, 1.0f) + position) * tileDimension,
                new Vector2f(tileDimension * tile.X + tileDimension, tileDimension * tile.Y + tileDimension)));
            vertexArray.Append(new Vertex((new Vector2f(0.0f, 1.0f) + position) * tileDimension,
                new Vector2f(tileDimension * tile.X, tileDimension * tile.Y + tileDimension)));
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Texture = tileset;
            target.Draw(vertexArray, states);
        }
    }
}