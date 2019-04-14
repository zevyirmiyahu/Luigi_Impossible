using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luigi_impossible
{
    class Water : Entity
    {
        private double x, y;
        private const int TILE_SIZE = 16 - 1; // -1 to align with tile background
        private Bitmap currSprite = Sprite.waters[0];
        private Bitmap[] sprites;
        private AnimationSprite animSprite;
        public Rectangle waterRect;

        public Water(int x, int y)
        {
            this.x = x * TILE_SIZE * Window.gameScale; // world coordinates
            this.y = y * TILE_SIZE * Window.gameScale; // off set y to move closer to base of tile
            waterRect = new Rectangle((int)this.x, (int)this.y, 16, 16); // used for collision
            sprites = Sprite.waters;
            animSprite = new AnimationSprite(5, Sprite.waters);
        }

        public double getX()
        {
            return x;
        }

        public double getY()
        {
            return y;
        }

        public void update()
        {
            x += -Tile.getTileSpeed();
            animSprite.update();
            currSprite = animSprite.animation();
        }

        public void render(Graphics g)
        {
            g.DrawImage(currSprite, (int)x, (int)y);
        }
    }
}

