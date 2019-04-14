using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luigi_impossible
{
    class Tile
    {
        private double x, y;
        public double xa, ya;
        private static double tileSpeed;
        public bool solid = false;
        private const int TILE_SIZE = 16 - 1;
        private string color;
        private Bitmap sprite;
        public static Bitmap voidTile = DrawFilledRectangle(TILE_SIZE * Window.gameScale, TILE_SIZE * Window.gameScale);
        public Rectangle tileRect;

        // DEFINE THE COLORS USED ON THE PIXEL MAP
        public static string col_ground1 = "#C19E00";
        public static string col_sky1 = "#0000FF";
        public static string col_brick1 = "#C10000";
        public static string col_platform1 = "#797979";
        public static string col_grass1 = "#00FF00";
        public static string col_grassLeft = "#00B600";
        public static string col_grassRight = "#006A00";
        public static string col_mysteryBox1 = "#FF6B00";
        public static string col_water1 = "#00C1C1";

        public Tile(int x, int y, Color color)
        {
            this.x = x * TILE_SIZE * Window.gameScale;
            this.y = y * TILE_SIZE * Window.gameScale;
            tileRect = new Rectangle((int)this.x, (int)this.y, 16 * Window.gameScale, 16 * Window.gameScale); // used for collision
            this.color = ColorTranslator.ToHtml(color); 
            setTileSprite(this.color);

            if (this.color == col_grassLeft || this.color == col_grass1 
                || this.color == col_grassLeft || this.color == col_grassRight
                || this.color == col_brick1 || this.color == col_platform1)
            {
                solid = true;
            }
        }

        public static void setTileSpeed(double xa)
        {
            tileSpeed = xa;
        }

        public static double getTileSpeed()
        {
            return tileSpeed;
        }

        public void setXa(double xa)
        {
            this.xa = xa;
        }
        public static void setYa()
        {
        }

        public double getX()
        {
            return x;
        }

        public double getY()
        {
            return y;
        }

        public string getColor()
        {
            return color;
        }

        private void setTileSprite(string color)
        {
            if(color.Equals(col_sky1))
            {
                sprite = new Bitmap(Sprite.sky1);
            }
            else if(color.Equals(col_platform1))
            {
                sprite = new Bitmap(Sprite.platform1);
            }
            else if (color.Equals(col_grass1))
            {
                sprite = new Bitmap(Sprite.grass1);
            }
            else if (color.Equals(col_grassLeft))
            {
                sprite = new Bitmap(Sprite.grassLeft);
            }
            else if (color.Equals(col_grassRight))
            {
                sprite = new Bitmap(Sprite.grassRight);
            }
            else if(color.Equals(col_mysteryBox1))
            {
                sprite = new Bitmap(Sprite.mysteryBox1);
            }
            else if (color.Equals(col_ground1))
            {
                sprite = new Bitmap(Sprite.ground1);
            }
            else if (color.Equals(col_brick1))
            {
                sprite = new Bitmap(Sprite.brick1);
            }
            else if (color.Equals(col_water1))
            {
                sprite = new Bitmap(Sprite.water1);
            }
            else
            {
                sprite = new Bitmap(voidTile);
            }
        }

        //Used for making a void Tile
        private static Bitmap DrawFilledRectangle(int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics graph = Graphics.FromImage(bmp))
            {
                Rectangle ImageSize = new Rectangle(0, 0, width, height);
                graph.FillRectangle(Brushes.Red, ImageSize);
            }
            return bmp;
        }

        public void update()
        {
            x -= xa; // for level camera scrolling
            tileRect.X = (int)x;
            tileRect.Y = (int)y;
        }

        public void render(Graphics g)
        {
            g.DrawImage(sprite, (int)x, (int)y);
        }
    }
}
