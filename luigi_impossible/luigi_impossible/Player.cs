using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;

namespace luigi_impossible
{
    class Player
    {
        private int timer = 0;
        private int jumpTimer = 0;
        public double x, y;
        public double xa, ya;
        private int startY; // for jumping
        private double netForce;
        private double jumpForce = -16;
        private double gravity = 8.0;
        private const int rightGameBorder = 25 * 16;
        private int leftGameBorder;
        private int playerSpeed = 4;
        private int TILE_SIZE = 16 - 1;
        private TileCoordinate tileCoordinate;
        public static Rectangle playerRect; // for collision dectection

        private bool xCollision = false; // used for camera scrolling control
        private bool onGround = false;
        private bool isJumping = false;
        public bool isWalking = false;
        public bool isDown = false;
        public bool flip = true;
        private bool isSmall = false; // size of player

        private Bitmap sprite = Sprite.bigPlayerLeftWalk1;

        public Player(int x, int y)
        {
            this.x = x * TILE_SIZE * Window.gameScale;
            this.y = y * TILE_SIZE * Window.gameScale;// - 8;
            playerRect = new Rectangle((int)this.x, (int)this.y, 32, 64); 
            tileCoordinate = new TileCoordinate(x * Window.gameScale, y * Window.gameScale);
        }

        public void update()
        {
            tileCoordinate.setX(x * Window.gameScale);
            tileCoordinate.setY(y * Window.gameScale);
            move();
        }

        private void move()
        {
            coinCollected();
            timer++;
            if (Window.jumpKey)
            {
                isJumping = true;
            }
            else if (Window.downKey)
            {
                isDown = true;
                xa = 0;
            }
            else if (Window.leftKey)
            {
                xa = -playerSpeed;
            }
            else if (Window.rightKey)
            {
                xa = playerSpeed;
            }
            else xa = 0;
            
            jump();
            flipSprite(); // face player correct direction
            gameBorder(); // ensure player remains in proper region
            collision(); // tile collision

            // gravity
            if (onGround) gravity = 0.0;
            else gravity = 8.0;

            x += xa;
            y += ya + gravity;
            System.Console.WriteLine("x = " + x);
            playerRect.X = (int)x;
            playerRect.Y = (int)y;
            if (timer % 5 == 0)
            {
                sprite = Sprite.playerAnimation(xa, isSmall, isDown, flip);
            }
            // reset values
            if (timer > 7200) timer = 0; 
            isDown = false;
        }

        private void jump()
        {
            int currentY = (int)y;
            if (!isJumping) startY = (int)y;
            int jumpHeight = Math.Abs(startY - currentY);
            if (isJumping && jumpHeight < 122)
            {
                onGround = false;
                ya = -16;
            }
            else
            {
                isJumping = false;
                ya = 0;
            }
        }

        private void collision()
        {
            Tile[] tiles = Level.getTiles();
            for (int i = 0; i < tiles.Length; i++)
            {
                Tile tile = tiles[i];
                if (tile.solid)
                {
                    if (playerRect.IntersectsWith(tile.tileRect))
                    {
                        // relative to player
                        bool collisionXRight = playerRect.Right > tile.tileRect.Left && playerRect.Left < tile.tileRect.Left;
                        bool collisionXLeft = playerRect.Left < tile.tileRect.Right && playerRect.Right > tile.tileRect.Right;
                        bool collisionYTop;
                        bool collisionYBottom = playerRect.Bottom >= tile.tileRect.Top - 64;
                        
                        // y-axis collision
                        if (collisionYBottom)
                        {
                            onGround = true;
                            y = tile.tileRect.Top - 70;
                        }
                        else
                        {
                            onGround = false;
                        }

                        // x-axis collision
                        if (collisionXRight && !onGround)
                        {
                            System.Console.WriteLine("HITTTING!!!!");
                            xCollision = true;
                            x -= (playerSpeed + 0.1);
                            //xa = playerSpeed;
                        }
                        else if(collisionXLeft && !onGround)
                        {
                            xCollision = true;
                            x += (playerSpeed + 0.1);
                            //xa = playerSpeed;
                        }
                        else
                        {
                            xCollision = false;
                        }
                    }
                }
                else // if not solid must not be on ground
                {
                    onGround = false;
                }
            }
        }

        private void flipSprite()
        {
            if (xa < 0 && isSmall) flip = true;
            if (xa > 0 && !isSmall) flip = true;
            else if (xa < 0 && !isSmall) flip = false;
            else if (xa == 0 && !isSmall) return;
            else flip = false;
        }

        private void gameBorder()
        {
            if (x >= rightGameBorder)
            {
                for(int i = 0; i < Level.getTiles().Length; i++)
                {
                    Level.getTiles()[i].setXa(xa);
                }
                Tile.setTileSpeed(xa); // update speed of tile for static use for coins, clouds etc.
                x -= (playerSpeed + 0.1); // keep player on screen
            }
            else if(x <= 0)
            {
                for (int i = 0; i < Level.getTiles().Length; i++)
                {
                    Level.getTiles()[i].setXa(0);
                }
                Tile.setTileSpeed(0);
                x += (playerSpeed + 0.1);
            } 
        }

        public void coinCollected()
        {
            for(int i = 0; i < Level.coins.Count; i++)
            {
                if (playerRect.IntersectsWith(Level.coins[i].coinRect)) Level.remove(Level.coins[i]);
            }
        }

        // same method as IntersectsWith()
        private bool rangeIntersection(int min0, int min1, int max0, int max1)
        {
            return Math.Max(min0, max0) >= Math.Min(min1, max1)
                        && Math.Min(min0, max0) <= Math.Max(min1, max1);
        }
 
        public void render(Graphics g)
        {
            g.DrawImage(sprite, (int)x, (int)y);
        }
    }
}
