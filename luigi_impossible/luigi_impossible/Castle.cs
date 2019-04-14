using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luigi_impossible
{
    class Castle : Entity
    {
        private double x, y;
        private Bitmap sprite = Sprite.castle;

        public Castle(int x, int y)
        {
            this.x = x * 16 * Window.gameScale;
            this.y = y * 16 * Window.gameScale + 4;
        }

        public void update() {
            x -= Tile.getTileSpeed();
        }

        public void render(Graphics g)
        {
            g.DrawImage(sprite, (int)x, (int)y);
        }
    }
}
