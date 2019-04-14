using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luigi_impossible
{
    class Score
    {
        private int score = 0;
        private int currentNumber = Level.coins.Count;
        private int prevNumber = Level.coins.Count;
        private Font font = new Font("Comic Sans MS", 12.0f, FontStyle.Bold);
        private SolidBrush brush = new SolidBrush(Color.White);

        public void update()
        {
            currentNumber = Level.coins.Count;
            if(currentNumber != prevNumber)
            {
                score ++;
                prevNumber = currentNumber;
            }
        }

        public void render(Graphics g)
        {
            g.DrawString(("Score: " + score), font, brush, 1, 1);
        }
    }
}
