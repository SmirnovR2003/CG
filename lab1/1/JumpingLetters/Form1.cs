using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JumpingLetters
{
    public partial class Form1 : Form
    {
        private const int LINE_WIDTH = 10;

        private delegate void MyDelegate(Label myControl);

        //сделать буквы круглыми
        //выделить координаты букв отдельно от изображения
        public Form1()
        {
            InitializeComponent();
        }

        private void firstLetter_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            
            g.FillRectangle(
                new SolidBrush(Color.Red),
                new Rectangle(0, 0, e.ClipRectangle.Width, LINE_WIDTH));

            g.FillRectangle(
                new SolidBrush(Color.Red),
                new Rectangle(0, 0, LINE_WIDTH, e.ClipRectangle.Height));

            g.FillRectangle(
                new SolidBrush(Color.Red),
                new Rectangle(0, e.ClipRectangle.Height - LINE_WIDTH, e.ClipRectangle.Width, LINE_WIDTH));
        }

        private void secondLetter_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.FillRectangle(
                new SolidBrush(Color.Green),
                new Rectangle(0, 0, e.ClipRectangle.Width, LINE_WIDTH));

            g.FillRectangle(
                new SolidBrush(Color.Green),
                new Rectangle(e.ClipRectangle.Width - LINE_WIDTH, 0, LINE_WIDTH, e.ClipRectangle.Height/2));

            g.FillRectangle( 
                new SolidBrush(Color.Green),
                new Rectangle(0, e.ClipRectangle.Height / 2, e.ClipRectangle.Width, LINE_WIDTH));

            g.FillRectangle(
                new SolidBrush(Color.Green),
                new Rectangle(0, 0, LINE_WIDTH, e.ClipRectangle.Height));
        }

        private void thirdLetter_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.FillRectangle(
                new SolidBrush(Color.Gold),
                new Rectangle(0, e.ClipRectangle.Height - 30, LINE_WIDTH, 30));

            g.FillRectangle(
                new SolidBrush(Color.Gold),
                new Rectangle(e.ClipRectangle.Width - LINE_WIDTH, e.ClipRectangle.Height - 30, LINE_WIDTH, 30));

            g.FillRectangle(
                new SolidBrush(Color.Gold),
                new Rectangle(0, e.ClipRectangle.Height - 30, e.ClipRectangle.Width, LINE_WIDTH));

            g.DrawPolygon(
                new Pen(Color.Gold, LINE_WIDTH),
                new PointF[]{
                    new PointF(LINE_WIDTH, e.ClipRectangle.Height - 30),
                    new PointF(e.ClipRectangle.Width/2, 0),
                    new PointF(e.ClipRectangle.Width - LINE_WIDTH, e.ClipRectangle.Height - 30),
                });

        }

        private void MoveLetters()
        {
            bool firstLetterDirection = true;
            bool secondLetterDirection = true;
            bool thirdLetterDirection = true;
            while (true)
            {
                Thread.Sleep(10);
                firstLetterDirection = MoveLabel(firstLetter, firstLetterDirection);
                secondLetterDirection = MoveLabel(secondLetter, secondLetterDirection);
                secondLetterDirection = MoveLabel(secondLetter, secondLetterDirection);
                thirdLetterDirection = MoveLabel(thirdLetter, thirdLetterDirection);
                thirdLetterDirection = MoveLabel(thirdLetter, thirdLetterDirection);
                thirdLetterDirection = MoveLabel(thirdLetter, thirdLetterDirection);

            }
        }

        private bool MoveLabel(Label sender, bool direction)
        {
            if (direction)
            {
                sender.Top--;
                if (sender.Top <= 0)
                {
                    direction = false;
                }
            }
            else 
            {
                sender.Top++;
                if (sender.Top >= 80)
                {
                    direction = true;
                }
            }
            return direction;


        }

        //сделать так, чтобы буква прыгала как в жизни
        private void jump_Click(object sender, EventArgs e)
        {   
            MoveLetters();

        }
    }
}
