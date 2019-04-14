using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luigi_impossible
{
    class Sprite
    {
        private static int scale = Window.gameScale; // resizes sprites
        private static int currIndex = 0;

        //  Spritesheet
        public static Bitmap playerSheet = resizeSprite(new Bitmap(Image.FromFile("../../Resources/Luigi.png")), scale);
        public static Bitmap levelSheet = resizeSprite(new Bitmap(Image.FromFile("../../Resources/Mario_Tiles_Level.png")), scale);
        public static Bitmap castleSheet = resizeSprite(new Bitmap(Image.FromFile("../../Resources/Mario_Castle.png")), scale);

        // non-player sprites
        public static Bitmap cloud = extractSprite(levelSheet, new Rectangle(2 * 16 * scale, 7 * 16 * scale, 4 * 16 * scale, 1 * 16 * scale));
        public static Bitmap coin1 = extractSprite(levelSheet, new Rectangle(3 * 16 * scale, 2 * 16 * scale, 16 * scale, 16 * scale));
        public static Bitmap coin2 = extractSprite(levelSheet, new Rectangle(4 * 16 * scale, 2 * 16 * scale, 16 * scale, 16 * scale));
        public static Bitmap coin3 = extractSprite(levelSheet, new Rectangle(5 * 16 * scale, 2 * 16 * scale, 16 * scale, 16 * scale));

        public static Bitmap goombaDead = extractSprite(levelSheet, new Rectangle(3 * 16 * scale, 1 * 16 * scale, 16 * scale - 1, 16 * scale - 1));
        public static Bitmap goomba1 = extractSprite(levelSheet, new Rectangle(4 * 16 * scale, 1 * 16 * scale, 16 * scale - 1, 16 * scale - 1));
        public static Bitmap goomba2 = extractSprite(levelSheet, new Rectangle(5 * 16 * scale, 1 * 16 * scale, 16 * scale - 1, 16 * scale - 1));

        public static Bitmap castle = extractSprite(castleSheet, new Rectangle(0, 0, 10 * 16 * scale - 1, 11 * 16 * scale - 1));

        // player sprites
        public static Bitmap smallPlayerJump = extractSprite(playerSheet, new Rectangle(0, 0, 16 * scale, 16 * scale));
        public static Bitmap smallPlayerWalk1 = extractSprite(playerSheet, new Rectangle(3 * 16 * scale, 1 * 16 * scale, 16 * scale, 16 * scale));
        public static Bitmap smallPlayerWalk2 = extractSprite(playerSheet, new Rectangle(2 * 16 * scale, 1 * 16 * scale, 16 * scale, 16 * scale));
        public static Bitmap smallPlayerWalk3 = extractSprite(playerSheet, new Rectangle(1 * 16 * scale, 1 * 16 * scale, 16 * scale, 16 * scale));

        public static Bitmap bigPlayerDownRight = extractSprite(playerSheet, new Rectangle(0, 3 * 32 * scale, 32 * scale, 32 * scale));
        public static Bitmap bigPlayerDownLeft = flipSprite(bigPlayerDownRight);
        public static Bitmap bigPlayerJump = extractSprite(playerSheet, new Rectangle(0, 1 * 32 * scale, 32 * scale, 32 * scale));
        public static Bitmap bigPlayerLeftWalk1 = extractSprite(playerSheet, new Rectangle(3 * 32 * scale, 2 * 32 * scale, 32 * scale, 32 * scale));
        public static Bitmap bigPlayerLeftWalk2 = extractSprite(playerSheet, new Rectangle(2 * 32 * scale, 2 * 32 * scale, 32 * scale, 32 * scale));
        public static Bitmap bigPlayerLeftWalk3 = extractSprite(playerSheet, new Rectangle(1 * 32 * scale, 2 * 32 * scale, 32 * scale, 32 * scale));


        // TILE SPRITES
        public static Bitmap ground1 = extractSprite(levelSheet, new Rectangle(1 * 16 * scale, 4 * 16 * scale, 16 * scale - 1, 16 * scale - 1));
        public static Bitmap water1 = extractSprite(levelSheet, new Rectangle(1 * 16 * scale, 6 * 16 * scale, 16 * scale - 1, 16 * scale - 1));
        public static Bitmap water2 = extractSprite(levelSheet, new Rectangle(2 * 16 * scale, 6 * 16 * scale, 16 * scale - 1, 16 * scale - 1));
        public static Bitmap water3 = extractSprite(levelSheet, new Rectangle(3 * 16 * scale, 6 * 16 * scale, 16 * scale - 1, 16 * scale -1));
        public static Bitmap sky1 = extractSprite(levelSheet, new Rectangle(0, 6 * 16 * scale, 16 * scale - 1, 16 * scale - 1));
        public static Bitmap platform1 = extractSprite(levelSheet, new Rectangle(3 * 16 * scale, 4 * 16 * scale, 16 * scale - 1, 16 * scale - 1));
        public static Bitmap brick1 = extractSprite(levelSheet, new Rectangle(0, 0, 16 * scale - 1, 16 * scale - 1));
        public static Bitmap grass1 = extractSprite(levelSheet, new Rectangle(1 * 16 * scale, 5 * 16 * scale, 16 * scale - 1, 16 * scale - 1));
        public static Bitmap grassLeft = extractSprite(levelSheet, new Rectangle(0, 5 * 16 * scale, 16 * scale - 1, 16 * scale - 1));
        public static Bitmap grassRight = extractSprite(levelSheet, new Rectangle(2 * 16 * scale, 5 * 16 * scale, 16 * scale - 1, 16 * scale - 1));
        public static Bitmap mysteryBox1 = extractSprite(levelSheet, new Rectangle(1 * 16 * scale, 0, 16 * scale - 1, 16 * scale - 1));
        
        // for animation sprites
        public static Bitmap[] bigPlayerLeftWalking = { bigPlayerLeftWalk1, bigPlayerLeftWalk2, bigPlayerLeftWalk3 };
        public static Bitmap[] bigPlayerRightWalking = flipSprites(bigPlayerLeftWalking);

        public static Bitmap[] smallPlayerWalking = { smallPlayerWalk1, smallPlayerWalk2, smallPlayerWalk3 };

        public static Bitmap[] goombas = { goomba1, goomba2 };
        public static Bitmap[] coins = { coin1, coin2, coin3 };
        public static Bitmap[] waters = { water1, water2, water3 };
       
        // take piece of spritesheet AND resize
        private static Bitmap extractResizeSprite(Bitmap bitmap, Rectangle section, int scale)
        {
            return resizeSprite(extractSprite(bitmap, section), scale);
        }
      
        // take piece of a spritesheet
        private static Bitmap extractSprite(Bitmap src, Rectangle section)
        {
            Bitmap bitmap = new Bitmap(section.Width, section.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(src, 0, 0, section, GraphicsUnit.Pixel);
            }
            return bitmap;
        }

        // resize based on dimensions
        private static Bitmap resizeSprite(Bitmap bitmap, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage((Image)result))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.DrawImage(bitmap, 0, 0, width, height);
            }
            return result;
        }

        // resize based on scale
        private static Bitmap resizeSprite(Bitmap bitmap, int scale)
        {
            Bitmap result = new Bitmap(scale * bitmap.Width, scale * bitmap.Height);
            using (Graphics g = Graphics.FromImage((Image)result))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.DrawImage(bitmap, 0, 0, scale * bitmap.Width, scale * bitmap.Height);
            }
            return result;
        }
 
        // used for player walk animation
        public static Bitmap playerAnimation(double xa, Boolean isSmall, Boolean isDown, Boolean flip)
        {
            if(isSmall)
            {
                if (xa == 0) return smallPlayerWalking[0];
                else
                {
                Bitmap currBitmap = bigPlayerLeftWalking[currIndex];
                if (currIndex == 2) currIndex = 0;
                else currIndex++;
                return currBitmap;
                }
            } 
            else
            {
                if(flip)
                {
                    if (isDown) return bigPlayerDownRight;
                    if (xa == 0) return bigPlayerRightWalking[0];
                    else
                    {
                    Bitmap currBitmap = bigPlayerRightWalking[currIndex];
                    if (currIndex == 2) currIndex = 0;
                    else currIndex++;
                    return currBitmap;
                    }
                }
                else
                {
                    if (isDown) return bigPlayerDownLeft;
                    if (xa == 0) return bigPlayerLeftWalking[0];
                    else
                    {
                    Bitmap currBitmap = bigPlayerLeftWalking[currIndex];
                    if (currIndex == 2) currIndex = 0;
                    else currIndex++;
                    return currBitmap;
                    }
                }
            }
        }

        // flip a single sprite;
        private static Bitmap flipSprite(Bitmap sprite)
        {
            Bitmap result = new Bitmap(sprite);
            result.RotateFlip(RotateFlipType.RotateNoneFlipX);
            return result;
        }

        // flip an array of sprites
        private static Bitmap[] flipSprites(Bitmap[] sprites)
        {
            Bitmap[] result = new Bitmap[sprites.Length];
            for(int i = 0; i < sprites.Length; i++)
            {
                Bitmap currSprite = new Bitmap(sprites[i]);
                currSprite.RotateFlip(RotateFlipType.RotateNoneFlipX);
                result[i] = currSprite;
            }
            return result;
        }
    }
}
