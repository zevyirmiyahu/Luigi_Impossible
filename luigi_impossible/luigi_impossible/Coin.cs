using luigi_impossible;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luigi_impossible
{ 
    class Coin : Entity
    {
        private double x, y;
        private const int TILE_SIZE = 16 - 1; // -1 to align with tile background
        private Bitmap currSprite = Sprite.coins[0];
        private Bitmap[] sprites;
        private AnimationSprite animSprite;
        public Rectangle coinRect;

        public Coin(int x, int y)
        {
            this.x = x * TILE_SIZE * Window.gameScale; // world coordinates
            this.y = y * TILE_SIZE * Window.gameScale + 8; // off set y to move closer to base of tile
            coinRect = new Rectangle((int)this.x, (int)this.y, 16, 16); // used for collision
            sprites = Sprite.coins;
            animSprite = new AnimationSprite(5, Sprite.coins);
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
            x -= Tile.getTileSpeed();
            coinRect.X = (int)x; // update to keep over entity
            animSprite.update();
            currSprite = animSprite.animation();
        }

        public void render(Graphics g)
        {
            g.DrawImage(currSprite, (int)x, (int)y);
        }
    }
}
