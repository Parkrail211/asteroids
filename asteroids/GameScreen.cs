using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace asteroids
{
    public partial class GameScreen : UserControl
    {
        Random rnd = new Random();
        List<Asteroid> asteroidList = new List<Asteroid>();


        SolidBrush drawBrush = new SolidBrush(Color.White);

        Player p1 = new Player(204, 405);
        Player p2 = new Player(612, 405);

        int asteroidSize = 5;
        int playerSize = 7;
        const int playerSpeed = 5;

        bool p1Up = false;
        bool p2Up = false;
        bool p1Down = false;
        bool p2Down = false;
        int p1Score = 0;
        int p2Score = 0;

        int gameTime = 978;
        public GameScreen()
        {
            InitializeComponent();
        }


        private void gameTick_Tick(object sender, EventArgs e)
        {
            gameTime--;
            foreach (Asteroid a in asteroidList)
            {
                if (a.x <= 0 || a.x >= this.Width - asteroidSize)
                {
                    a.direction = !a.direction;

                }


                if (a.direction)
                {
                    a.x += a.speed;
                }
                else
                {
                    a.x -= a.speed;

                  
                }
            }

            if (p1Up)
            {
                p1.y -= playerSpeed;
            }
            else if (p1Down)
            {
                p1.y += playerSpeed;

            }

            if (p2Up)
            {
                p2.y -= playerSpeed;
            }
            else if (p2Down)
            {
                p2.y += playerSpeed;

            }

            Rectangle p1Rec = new Rectangle(p1.x, p1.y, playerSize, playerSize);
            Rectangle p2Rec = new Rectangle(p2.x, p2.y, playerSize, playerSize);
            // makes rectangles for the players
            foreach (Asteroid a in asteroidList)
            {
                Rectangle asteroidRec = new Rectangle(a.x, a.y, asteroidSize, asteroidSize);

                if (p1Rec.IntersectsWith(asteroidRec))
                {
                    p1.y = 405;
                }

                if (p2Rec.IntersectsWith(asteroidRec))
                {
                    p2.y =405;
                }

                //checks if either player is touching an asteroid
            }

            if (p1.y < 405)
            {
                p1Score++;
                p1.y = 405;
            }
            if (p2.y <= 0)
            {
                p2Score++;
                p2.y = 405;
            }
            // makes it so players cant go infinitely down

            scoreLabel.Text = $"P1: {p1Score} P2: {p2Score}";

            if (gameTime <= 0)
            {
                GameOver();
                //ends the game
            }
            Refresh();
        }

        private void GameScreen_Load(object sender, EventArgs e)
        {
            gameTick.Enabled = true;
            for (int i = 0; i < 41; i++)
            {
                int x = rnd.Next(5, this.Width);
                int y = rnd.Next(0, 375);
                int speed = rnd.Next(4, 15);
                bool direction = Convert.ToBoolean(rnd.Next(-1, 1));

                Asteroid rock = new Asteroid(x, y, speed, direction);
                asteroidList.Add(rock);
            }
            //creates the asteroids


        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            foreach (Asteroid a in asteroidList)
            {
                e.Graphics.FillRectangle(drawBrush, a.x, a.y, asteroidSize, asteroidSize);

            }


            e.Graphics.FillRectangle(drawBrush, p1.x, p1.y, playerSize, playerSize);
            e.Graphics.FillRectangle(drawBrush, p2.x, p2.y, playerSize, playerSize);
            e.Graphics.FillRectangle(drawBrush, 400, 0, 5, gameTime / 2);
        }
        //draws asteroids and players
        private void GameScreen_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    p1Up = true;
                    break;

                case Keys.S:
                    p1Down = true;
                    break;



            }
            switch (e.KeyCode)
            {
                case Keys.J:
                    p2Up = true;
                    break;
                case Keys.M:
                    p2Down = true;
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    p1Up = false;
                    break;

                case Keys.S:
                    p1Down = false;
                    break;



            }
            switch (e.KeyCode)
            {
                case Keys.J:
                    p2Up = false;
                    break;
                case Keys.M:
                    p2Down = false;
                    break;
            }

        }
        
        public void GameOver()
        {
            Form f = this.FindForm();
            f.Controls.Remove(this);

            GameOverScreen gos = new GameOverScreen();
            f.Controls.Add(gos);

            /*
             * i have no idea why, but this code just will not work
             */
        }
    }
}
