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


    class TileMap : public sf::Drawable, public sf::Transformable
{
public:

    bool load(const std::string& tileset, sf::Vector2u tileSize, const int* tiles, unsigned int width, unsigned int height)
    {
        // on charge la texture du tileset
        if (!m_tileset.loadFromFile(tileset))
            return false;

        // on redimensionne le tableau de vertex pour qu'il puisse contenir tout le niveau
        m_vertices.setPrimitiveType(sf::Quads);
        m_vertices.resize(width * height * 4);

        // on remplit le tableau de vertex, avec un quad par tuile
        for (unsigned int i = 0; i < width; ++i)
            for (unsigned int j = 0; j < height; ++j)
            {
                // on récupère le numéro de tuile courant
                int tileNumber = tiles[i + j * width];

                // on en déduit sa position dans la texture du tileset
                int tu = tileNumber % (m_tileset.getSize().x / tileSize.x);
                int tv = tileNumber / (m_tileset.getSize().x / tileSize.x);

                // on récupère un pointeur vers le quad à définir dans le tableau de vertex
                sf::Vertex* quad = &m_vertices[(i + j * width) * 4];

                // on définit ses quatre coins
                quad[0].position = sf::Vector2f(i * tileSize.x, j * tileSize.y);
                quad[1].position = sf::Vector2f((i + 1) * tileSize.x, j * tileSize.y);
                quad[2].position = sf::Vector2f((i + 1) * tileSize.x, (j + 1) * tileSize.y);
                quad[3].position = sf::Vector2f(i * tileSize.x, (j + 1) * tileSize.y);

                // on définit ses quatre coordonnées de texture
                quad[0].texCoords = sf::Vector2f(tu * tileSize.x, tv * tileSize.y);
                quad[1].texCoords = sf::Vector2f((tu + 1) * tileSize.x, tv * tileSize.y);
                quad[2].texCoords = sf::Vector2f((tu + 1) * tileSize.x, (tv + 1) * tileSize.y);
                quad[3].texCoords = sf::Vector2f(tu * tileSize.x, (tv + 1) * tileSize.y);
            }

        return true;
    }

    private:

    virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const
    {
        // on applique la transformation
        states.transform *= getTransform();

    // on applique la texture du tileset
    states.texture = &m_tileset;

        // et on dessine enfin le tableau de vertex
        target.draw(m_vertices, states);
    }

sf::VertexArray m_vertices;
sf::Texture m_tileset;
};
}


