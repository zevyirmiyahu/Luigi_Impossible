using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace luigi_impossible
{
    public partial class Window : Form
    {
        private Score score;
        public static int gameScale = 2; // scale all sprite sizes up by this amount
        private int time = 0;
        private double x, y;
        private double xa, ya; // offsets increments for movement
        private const int rightGameBorder = 25 * 16;
        private int leftGameBorder;
        private Level level;
        private static Player player;
        public static bool leftKey = false;
        public static bool rightKey = false;
        public static bool downKey = false;
        public static bool jumpKey = false; // spacebar

        public Window()
        {
            level = new Level();
            player = new Player(2, 13);

            addCoins();
            addGoombas();
            addWater();
            addClouds();
            level.add(new Castle(135, 3));
            InitializeComponent();
            setTimer();
            score = new Score(); // must come after coins have been added for proper calculation
        }

        private void setTimer()
        {
        System.Timers.Timer timer = new System.Timers.Timer();
        timer.Elapsed += gameLoop;
        // Set the Interval to 1 millisecond.  Note: Time is set in Milliseconds
        timer.Interval = 1;
        timer.AutoReset = true;
         timer.Enabled = true;
        }


        private void gameLoop(Object source, ElapsedEventArgs e) 
        {
           update();
            Invalidate();
        }
        
        private void update()
        {
            time++;
            level.update();
            player.update();
            score.update();
            removeOffScreenAssets(); // for efficiency remove
            if (time > 7200) time = 0; // reset time 
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            level.render(g);
            player.render(g);
            score.render(g);
            base.OnPaint(e);
        }

        private void Window_Load(object sender, EventArgs e)
        {
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 32) jumpKey = true; // spacebar
            if (e.KeyValue == 37) leftKey = true;
            if (e.KeyValue == 39) rightKey = true;
            if (e.KeyValue == 40) downKey = true;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 32) jumpKey = false;
            if (e.KeyValue == 37) leftKey = false;
            if (e.KeyValue == 39) rightKey = false;
            if (e.KeyValue == 40) downKey = false;
        }

        // add npcs and other assets to level
        private void addWater()
        {
            for (int i = 85; i < 106; i++)
            {
                level.add(new Water(i, 14));
            }
        }

        private void removeOffScreenAssets()
        {
            for(int i = 0; i < Level.coins.Count; i++)
            {
                if (Level.coins[i].getX() < -32)
                {
                    Level.remove(Level.coins[i]);
                }
            }
            for(int i = 0; i < Level.goombas.Count; i++)
            {
                if(Level.goombas[i].getX() < -32)
                {
                    Level.remove(Level.goombas[i]);
                } 
                else if(Level.goombas[i].isDead && Level.goombas[i].deadCounter > 31) 
                {
                    Level.remove(Level.goombas[i]);
                }          
            }
            for (int i = 0; i < Level.clouds.Count; i++)
            {
                if (Level.clouds[i].getX() < -128)
                {
                    Level.clouds.Remove(Level.clouds[i]);
                }
            }
        }

        private void addClouds()
        {
            level.add(new Cloud(12, 2));
            level.add(new Cloud(18, 3));
            level.add(new Cloud(25, 1)); 
            level.add(new Cloud(40, 2)); 
            level.add(new Cloud(48, 3)); 
            level.add(new Cloud(56, 1)); 
        }

        private void addGoombas()
        {
            level.add(new Goomba(15, 13, -1));
            level.add(new Goomba(14, 13, -1));
            level.add(new Goomba(37, 13, 0));
        }

        private void addCoins()
        {
            level.add(new Coin(5, 7));
            level.add(new Coin(6, 7));
            level.add(new Coin(7, 7));

            level.add(new Coin(4, 10));
            level.add(new Coin(5, 10));
            level.add(new Coin(6, 10));
            level.add(new Coin(7, 10));

            level.add(new Coin(40, 6));
            level.add(new Coin(41, 6));
            level.add(new Coin(42, 6));

            level.add(new Coin(41, 3));
            level.add(new Coin(42, 3));
        }
    }

}
