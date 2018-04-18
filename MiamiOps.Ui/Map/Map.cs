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
        private VertexArray _vertexArray;

        private int width;
        private int height;
        private Vector2u tileSize;
        private int tilewidth;
        private int tileheight;
        private string[] level_array1;
        private string[] level_array2;
        private string[] level_array3;
        private int level_layers_length;
        private Dictionary<int,string[]> total_array = new Dictionary<int, string[]>();
        private Dictionary<int, VertexArray> total_array_vertex = new Dictionary<int, VertexArray>();

        public Map(String XML)
        {

            using (FileStream fs = File.OpenRead(XML))
            using (StreamReader sr = new StreamReader(fs, true))
            {
                XElement xml = XElement.Load(sr);
                string level = xml.Descendants("layer")
                                  .Single(l => l.Attribute("name").Value == "terrain")
                                  .Element("data").Value;
                string level_layer2 = xml.Descendants("layer")
                                  .Single(l => l.Attribute("name").Value == "terrain 2")
                                  .Element("data").Value;
                //string level_layer3 = xml.Descendants("layer")
                //                  .Single(l => l.Attribute("name").Value == "collision")
                //                  .Element("data").Value;
                level_array1 = level.Split(',');
                level_array2 = level_layer2.Split(',');
              //  level_array3 = level_layer2.Split(',');
                total_array.Add(0, level_array1);
                total_array.Add(1,level_array2);
               // total_array.Add(2, level_array3);
                level_layers_length = total_array.Count;

                width = 100;
                height = 100;
                tileSize = new Vector2u(64, 64);
                tileset = new Texture(@"..\..\..\tileset1.png");
                ConstructMap();
            }
           
        }

        public void ConstructMap()
        {

            int i = 0;
            while (level_layers_length != 0)
            {
                _vertexArray = new VertexArray(PrimitiveType.Quads, (uint)(width * height * 4));
                int a = 0;
                for (uint y = 0; y < width; y++)
                {
                    for (uint x = 0; x < height; x++)
                    {
                        string[] level_array = total_array[i];
                        
                        int tileID = Int32.Parse(level_array[a]);

                        if (tileID != 0)
                        {

                            long tu = tileID % (tileset.Size.X / tileSize.X) - 1;
                            long tv = tileID / (tileset.Size.X / tileSize.X);
                            uint index = (uint)(x + y * width) * 4;


                            _vertexArray[index + 0] = new Vertex(new Vector2f(x * tileSize.X, y * tileSize.Y), new Vector2f(tu * tileSize.X, tv * tileSize.Y));
                            _vertexArray[index + 1] = new Vertex(new Vector2f((x + 1) * tileSize.X, y * tileSize.Y), new Vector2f((tu + 1) * tileSize.X, tv * tileSize.Y));
                            _vertexArray[index + 2] = new Vertex(new Vector2f((x + 1) * tileSize.X, (y + 1) * tileSize.Y), new Vector2f((tu + 1) * tileSize.X, (tv + 1) * tileSize.Y));
                            _vertexArray[index + 3] = new Vertex(new Vector2f(x * tileSize.X, (y + 1) * tileSize.Y), new Vector2f(tu * tileSize.X, (tv + 1) * tileSize.Y));
                        }
                        a++;

                    }
                }
                total_array_vertex.Add(i, _vertexArray);
                level_layers_length--;
                i++;
            }
            Console.WriteLine(total_array_vertex[0][4]);
            Console.WriteLine(total_array_vertex[0][5]);
            Console.WriteLine(total_array_vertex[1][3]);
        }

        public Dictionary<int,VertexArray> TotalArrayVertex
        {
            get { return total_array_vertex; }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Texture = tileset;
            for (int i = 0; i < TotalArrayVertex.Count; i++)
            {
                target.Draw(total_array_vertex[i], states);
            }
            
        }
    }
}