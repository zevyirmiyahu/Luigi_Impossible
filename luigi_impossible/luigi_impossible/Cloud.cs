using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luigi_impossible
{
    class Cloud : Entity
    {
        private int timer = 0;
        private double x, y;
        private double xa = 0.5;
        private Bitmap sprite = Sprite.cloud;

        public Cloud(int x, int y)
        {
            this.x = x * 16 * Window.gameScale;
            this.y = y * 16 * Window.gameScale;
        }

        public double getX()
        {
            return x;
        }

        public void update()
        {
            timer++;
            if (timer % 31 == 0)
            {
                timer = 0;
                xa *= -1;
            }
            x += -Tile.getTileSpeed() + xa;
        }

        public void render(Graphics g)
        {
            g.DrawImage(sprite, (int)x, (int)y);
        }
    }
}
