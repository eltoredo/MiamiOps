using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.System;
using SFML.Graphics;
using SFML.Audio;


namespace MiamiOps
{
    class Map
        {
            private Texture tileset;
            private VertexArray vertexArray;

            private int width;
            private int height;
            private float tileDimension;
            private float tileWorldDimension;
            private List<int> idMap = new List<int>();

        public Map(Texture tileset, int width, int height, float tileDimension, float tileWorldDimension, List<int> idMap)
        {
            this.tileset = tileset;

            this.width = width;
            this.height = height;
            this.tileDimension = tileDimension;
            this.tileWorldDimension = tileWorldDimension;
            this.idMap = idMap;
            
        }

        public void ConstructMap() { 

          vertexArray = new VertexArray(PrimitiveType.Quads, (uint)(width * height * 4));

           Tile tile = new Tile(0, 1, Color.White);

           for (int x = 0; x < width; x++)
           {
               for (int y = 0; y < height; y++)
               {
                   
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


