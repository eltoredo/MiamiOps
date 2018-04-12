using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace MiamiOps
{
    class Tile
    {
        public int X;
        public int Y;

        public Color color;

        public Tile(int X, int Y, Color color)
        {
            this.X = X;
            this.Y = Y;
            this.color = color;
        }
    }
}

