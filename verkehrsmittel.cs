using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace dopravniProstredek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
        }

        public int speed = 1;

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            canvas.SmoothingMode = SmoothingMode.HighQuality;

            /*
            LinearGradientBrush skyColor = new LinearGradientBrush(
            new Point(900,500),
            new Point(1600, 0),
            Color.FromArgb(255, 30, 30, 30),
            Color.FromArgb(255, 120, 120, 90));

            canvas.FillRectangle(Brushes.White, 900, 500, 1600, 0);
            */

            // stars
            Image star = Image.FromFile(@"F:\Vlastní dokumenty\School files\IT3\Programování\dopravniProstredek\dopravniProstredek\star.png");
            Random rnd = new Random();

            for (int i = 0; i < 10; i++)
            {
                int posX = rnd.Next(10,1560);
                int posY = rnd.Next(10, 550);
                canvas.DrawImage(star, posX, posY);
            }

            // rocket
            canvas.TranslateTransform(500, -250);
            canvas.RotateTransform(10);
                
                // body
            canvas.FillRectangle(Brushes.Gray, 0, 560, 200, 340);
            

                // front
            canvas.FillEllipse(Brushes.Gray, 0, 333, 200, 500);

                // body-window
            Pen windowFramePen = new Pen(Color.DarkGray, 8);

            canvas.FillEllipse(Brushes.LightBlue, 30, 500, 140, 140);
            canvas.DrawEllipse(windowFramePen, 30, 500, 140, 140);
            canvas.DrawLine(windowFramePen, 100, 500, 100, 640);
            canvas.DrawLine(windowFramePen, 30, 575, 170, 575);

                // rocket label
            string text = "Союз";
            Font textFont = new Font("Arial", 24, FontStyle.Bold);
            canvas.DrawString(text, textFont, Brushes.Red, 47, 700);

            // thruster
            SolidBrush thrusterColor = new SolidBrush(Color.FromArgb(255, 25, 25, 25));

            Point polygon1 = new Point(0, 898);
            Point polygon2 = new Point(0 - 40,   900 + 60);
            Point polygon3 = new Point(200 + 40, 900 + 60);
            Point polygon4 = new Point(200, 898);

            Point[] thruster = { polygon1, polygon2, polygon3, polygon4 };

            canvas.FillPolygon(thrusterColor, thruster);

        }

        public int dummy = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            dummy += 1;

            int displacementX = 5;
            int displacementY = 3 * displacementX;

            if (dummy <= 15)
            {
                pictureBox1.Location = new Point(pictureBox1.Location.X - displacementX, pictureBox1.Location.Y + displacementY);
                pictureBox2.Location = new Point(pictureBox2.Location.X - displacementX, pictureBox2.Location.Y + displacementY);
                pictureBox3.Location = new Point(pictureBox3.Location.X - displacementX, pictureBox3.Location.Y + displacementY);
                pictureBox4.Location = new Point(pictureBox4.Location.X - displacementX, pictureBox4.Location.Y + displacementY);
                pictureBox5.Location = new Point(pictureBox5.Location.X - displacementX, pictureBox5.Location.Y + displacementY);
                pictureBox6.Location = new Point(pictureBox6.Location.X - displacementX, pictureBox6.Location.Y + displacementY);
                pictureBox7.Location = new Point(pictureBox7.Location.X - displacementX, pictureBox7.Location.Y + displacementY);
                pictureBox8.Location = new Point(pictureBox8.Location.X - displacementX, pictureBox8.Location.Y + displacementY);
                pictureBox9.Location = new Point(pictureBox9.Location.X - displacementX, pictureBox9.Location.Y + displacementY);
            }
            else
            {
                pictureBox1.Location = new Point(378, 717);
                pictureBox2.Location = new Point(324, 717);
                pictureBox3.Location = new Point(335, 777);
                pictureBox4.Location = new Point(381, 780);
                pictureBox5.Location = new Point(446, 730);
                pictureBox6.Location = new Point(427, 780);
                pictureBox7.Location = new Point(492, 738);
                pictureBox8.Location = new Point(481, 799);
                pictureBox9.Location = new Point(278, 764);

                dummy = 0;
            }
        }

        public double tt;

        private void timer2_Tick(object sender, EventArgs e)
        {
            // bezier
            int xTop = 1340;    // 1340
            int yTop = 555;     // 130
            Point bezier1 = new Point(xTop, yTop);
            Point bezier2 = new Point(xTop + 100, yTop);
            Point bezier3 = new Point(xTop + 100, yTop + 100);
            Point bezier4 = new Point(xTop, yTop + 100);

            if (tt <= 1)
            {
                double drawX = Math.Pow(1 - tt, 3) * bezier1.X + 3 * tt * Math.Pow(1 - tt, 2) * bezier2.X + 3 * tt * tt * (1 - tt) * bezier3.X + Math.Pow(tt, 3) * bezier4.X;
                double drawY = Math.Pow(1 - tt, 3) * bezier1.Y + 3 * tt * Math.Pow(1 - tt, 2) * bezier2.Y + 3 * tt * tt * (1 - tt) * bezier3.Y + Math.Pow(tt, 3) * bezier4.Y;
                pictureBox10.Location = new Point(Convert.ToInt32(drawX), Convert.ToInt32(drawY));
                tt += 0.01;
            }
            else if (tt > 1 && tt <= 2)
            {
                double offset = 1 - (tt-1);
                double drawX = Math.Pow(1 - offset, 3) * bezier1.X + 3 * offset * Math.Pow(1 - offset, 2) * bezier2.X + 3 * offset * offset * (1 - offset) * bezier3.X + Math.Pow(offset, 3) * bezier4.X;
                double drawY = Math.Pow(1 - offset, 3) * bezier1.Y + 3 * offset * Math.Pow(1 - offset, 2) * bezier2.Y + 3 * offset * offset * (1 - offset) * bezier3.Y + Math.Pow(offset, 3) * bezier4.Y;
                pictureBox10.Location = new Point(Convert.ToInt32(drawX), Convert.ToInt32(drawY));
                tt += 0.01;
            }
            else
            {
                tt = 0;
            }


        }
    }
}
