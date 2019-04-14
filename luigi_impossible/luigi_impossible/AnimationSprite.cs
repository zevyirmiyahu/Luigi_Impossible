using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luigi_impossible
{
    class AnimationSprite
    {
        private Bitmap[] sprites;
        private int currIndex = 0;
        private int size;
        private int timer = 0;
        private int animationMaxTime;

        public AnimationSprite(int animationMaxTime, Bitmap[] sprites)
        {
            this.sprites = sprites;
            this.animationMaxTime = animationMaxTime;
            size = sprites.Length;
        }

        public void update()
        {
            timer++;
            if (timer % animationMaxTime == 0)
            {
                timer = 0; // reset
                if (currIndex == size - 1) currIndex = 0;
                else currIndex++;
            }
        }

        public Bitmap animation()
        {
            Bitmap currBitmap = sprites[currIndex];
            return currBitmap;
        }

    }
}
