using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Coordinate class for spawning player

namespace luigi_impossible
{
    class TileCoordinate
    {
        private int x, y;
        private const int TILE_SIZE = 16;

        public TileCoordinate(double x, double y)
        {
            this.x = (int)x / TILE_SIZE;
            this.y = (int)y / TILE_SIZE;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public void setX(double x)
        {
            this.x = (int)x / TILE_SIZE;
        }

        public void setY(double y)
        {
            this.y = (int)y / TILE_SIZE;
        }
    }
}
