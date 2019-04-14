using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace luigi_impossible
{
    class Level
    {
        private static int width;
        private static Bitmap spawnLevel; // pixel version of level to be replaced by tiles for specific colors
        public static string[] pixelLevel; // store entire level desing as pixel array
        private static Tile[] tiles;
        public static List<Cloud> clouds = new List<Cloud>();
        public static List<Goomba> goombas = new List<Goomba>();
        public static List<Coin> coins = new List<Coin>();
        public static List<Water> waters = new List<Water>();
        public static List<Castle> castles = new List<Castle>();

        public Level()
        {
            spawnLevel = new Bitmap(Image.FromFile("../../Resources/Pixel_Level.png"));
            width = spawnLevel.Width;
            pixelLevel = new string[spawnLevel.Width * spawnLevel.Height];
            tiles = new Tile[pixelLevel.Length];
            storePixels(); // store all pixels
        }

        public void add(Entity e)
        {
            if(e.GetType() == typeof(Goomba))
            {
                goombas.Add((Goomba)e);
            }
            if(e.GetType() == typeof(Coin))
            {
                coins.Add((Coin)e);
            }
            if(e.GetType() == typeof(Cloud))
            {
                clouds.Add((Cloud)e);
            }
            if(e.GetType() == typeof(Water))
            {
                waters.Add((Water)e);
            }
            if(e.GetType() == typeof(Castle))
            {
                castles.Add((Castle)e);
            }
        }

        public static void remove(Entity e)
        {
            if(e.GetType() == typeof(Coin))
            {
                coins.Remove((Coin)e);
            }
            if (e.GetType() == typeof(Goomba))
            {
                goombas.Remove((Goomba)e);
            }
            if (e.GetType() == typeof(Cloud))
            {
                clouds.Remove((Cloud)e);
            }
        }

        public static Tile[] getTiles()
        {
            return tiles;
        }

        private void storePixels()
        {
            for (int y = 0; y < spawnLevel.Height; y++) {
                for(int x = 0; x < spawnLevel.Width; x++)
                {
                    Color color = spawnLevel.GetPixel(x, y);
                    Tile tile = new Tile(x, y, color);
                    tiles[x + y * spawnLevel.Width] = tile;
                    pixelLevel[x + y * spawnLevel.Width] = ColorTranslator.ToHtml(color);
                }
            }
        }

        public void update()
        {
            for(int i = 0; i < tiles.Length; i++)
            {
                tiles[i].update();
            }
            for(int i = 0; i < coins.Count; i++)
            {
                coins[i].update();
            }
            for(int i = 0; i < clouds.Count; i++)
            {
                clouds[i].update();
            }
            for(int i = 0; i < goombas.Count; i++)
            {
                goombas[i].update();
            }
            for(int i = 0; i < waters.Count; i++)
            {
                waters[i] .update();
            }
            for (int i = 0; i < castles.Count; i++)
            {
                castles[i].update();
            }
        }

        public void render(Graphics g)
        {
            for(int i = 0; i < tiles.Length; i++)
            {
                tiles[i].render(g);
            }
            for(int i = 0; i < coins.Count; i++)
            {
                coins[i].render(g);
            }
            for(int i = 0; i < clouds.Count; i++)
            {
                clouds[i].render(g);
            }
            for(int i = 0; i < goombas.Count; i++)
            {
                goombas[i].render(g);
            }
            for(int i = 0; i < waters.Count; i++)
            {
                waters[i].render(g);
            }
            for (int i = 0; i < castles.Count; i++)
            {
                castles[i].render(g);
            }
        }
    }
}
