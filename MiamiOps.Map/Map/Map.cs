using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
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
        private uint tilewidth;
        private uint tileheight;
        private string[] level_array1;
        private string[] level_array2;
        private string[] level_array3;
        private int level_layers_length;
        private Dictionary<int,string[]> total_array = new Dictionary<int, string[]>();
        private Dictionary<int,VertexArray> total_array_vertex = new Dictionary<int,VertexArray>();
        Round _ctxround;
        private uint firstX;

        public Map(String XML, string tilesset)
        {

            using (FileStream fs = File.OpenRead(XML))
            using (StreamReader sr = new StreamReader(fs, true))
            {
                XElement xml = XElement.Load(sr);

                tileheight   = uint.Parse(xml.Attribute("tileheight").Value);
                Console.WriteLine(tileheight);

                tilewidth = tileheight;


                height = int.Parse(xml.Attribute("height").Value);
                width = int.Parse(xml.Attribute("width").Value);

                string level = xml.Descendants("layer")
                                  .Single((l) => {
                                      return l.Attribute("name").Value == "terrain1";
                                  })
                                  .Element("data").Value;

                string level_layer2 = xml.Descendants("layer")
                                  .Single(l => l.Attribute("name").Value == "terrain2")
                                  .Element("data").Value;

                string level_layer3 = xml.Descendants("layer")
                                  .Single(l => l.Attribute("name").Value == "spawn")
                                  .Element("data").Value;
                level_array1 = level.Split(',');
                level_array2 = level_layer2.Split(',');
                level_array3 = level_layer3.Split(',');
                total_array.Add(0,level_array1);
                total_array.Add(1,level_array2);
                total_array.Add(2,level_array3);
                level_layers_length = total_array.Count;

                tileSize = new Vector2u(tileheight,tilewidth);
                tileset = new Texture(tilesset);
               // _ctxround = ctxRound;
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
                            if (tu < 0)
                            {
                                tu = tileset.Size.X / tileSize.X - 1;
                                tv--;

                            }
                            uint index = (uint)(x + y * width) * 4;
                            Color _textureColor = new Color(255, 255, 255, 255);
                            
                                _vertexArray[index + 0] = new Vertex(new Vector2f(x * tileSize.X, y * tileSize.Y), _textureColor, new Vector2f(tu * tileSize.X, tv * tileSize.Y));
                                _vertexArray[index + 1] = new Vertex(new Vector2f((x + 1) * tileSize.X, y * tileSize.Y), _textureColor, new Vector2f((tu + 1) * tileSize.X, tv * tileSize.Y));
                                _vertexArray[index + 2] = new Vertex(new Vector2f((x + 1) * tileSize.X, (y + 1) * tileSize.Y), _textureColor, new Vector2f((tu + 1) * tileSize.X, (tv + 1) * tileSize.Y));
                                _vertexArray[index + 3] = new Vertex(new Vector2f(x * tileSize.X, (y + 1) * tileSize.Y), _textureColor, new Vector2f(tu * tileSize.X, (tv + 1) * tileSize.Y));
                            

                        }

                        a++;


                    }
                }
                total_array_vertex.Add(i, _vertexArray);
                level_layers_length--;
                i++;
            }

          
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