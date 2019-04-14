using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luigi_impossible
{
    class Goomba : Entity
    {
        private double x, y;
        private int movementType; // determines movement of goomba
        private double xa = 0.5;
        private int timer = 0;
        public int deadCounter = 0;
        public bool isDead = false;
        private Rectangle goombaRect;
        private Bitmap deadSprite = Sprite.goombaDead;
        private Bitmap currSprite = Sprite.goombas[0];
        private Bitmap[] sprites;
        private AnimationSprite animSprite;

        public Goomba(int x, int y, int movementType)
        {
            this.x = x * 16 * Window.gameScale;
            this.y = y * 16 * Window.gameScale;
            this.movementType = movementType;
            sprites = Sprite.goombas;
            animSprite = new AnimationSprite(5, Sprite.goombas);
            goombaRect = new Rectangle((int)this.x, (int)this.y, 16, 16); // used for collision
        }

        public double getX()
        {
            return x;
        }

        public void update()
        {
            if (Player.playerRect.Bottom >= goombaRect.Top - 64
                    && Player.playerRect.Right >= goombaRect.Left - 32
                    && Player.playerRect.Left <= goombaRect.Right - 32) isDead = true;
            timer++;
            if(isDead) deadCounter++; // how long goomba has been dead
            movement(movementType);
            x += -Tile.getTileSpeed() + xa;
            goombaRect.X = (int)x; // update to keep over entity
            if (isDead) currSprite = deadSprite;
            else
            {
                animSprite.update();
                currSprite = animSprite.animation();
            }
        }

        private void movement(int movementType)
        {
            switch(movementType)
            {
                case -1:
                    xa = -1;
                    break;
                case 0:
                    if (timer % 91 == 0)
                    {
                        timer = 0;
                        xa *= -1;
                    }
                    break;
                case 1:
                    xa = 1;
                    break;
            }
        }

        public void render(Graphics g)
        {
            g.DrawImage(currSprite, (int)x, (int)y);
        }
    }
}
