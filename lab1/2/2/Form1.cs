using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace House
{
    public partial class Form1 : Form
    {
        private struct SDragAndDrop
        {
            public bool start;
            public int startX;
            public int startY;
            public int lastX;
            public int lastY;
        };

        private SDragAndDrop dragAndDrop;

        //сделать два домика, но один меньше другого
        public Form1()
        {
            InitializeComponent();
            dragAndDrop = new SDragAndDrop();
        }

        private void picture_MouseDown(object sender, MouseEventArgs e)
        {
            dragAndDrop.start = true;

            dragAndDrop.startX = MousePosition.X;
            dragAndDrop.startY = MousePosition.Y; 
            dragAndDrop.lastX = picture.Location.X;
            dragAndDrop.lastY = picture.Location.Y;
        }

        private void picture_MouseMove(object sender, MouseEventArgs e)
        {
            if (!dragAndDrop.start) return;
            picture.Location = new Point(
                MousePosition.X - dragAndDrop.startX + dragAndDrop.lastX,
                MousePosition.Y - dragAndDrop.startY + dragAndDrop.lastY);

        }

        private void picture_MouseUp(object sender, MouseEventArgs e)
        {
            dragAndDrop.start = false;
        }

        private void picture_MouseLeave(object sender, EventArgs e)
        {
            dragAndDrop.start = false;

        }

        private void picture_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //дом
            g.FillRectangle(
                new SolidBrush(Color.Yellow),
                200, 150, 150, 150);
            g.DrawRectangle(
                new Pen(Color.Black, 2),
                200, 150, 150, 150);

            //труба
            g.FillRectangle(
                new SolidBrush(Color.Black),
                300, 50, 30, 70);

            //окно
            g.FillRectangle(
                new SolidBrush(Color.Blue),
                295, 175, 35, 35);
            g.DrawRectangle(
                new Pen(Color.Black, 2),
                295, 175, 35, 35);

            //дверь
            g.FillRectangle(
                new SolidBrush(Color.Red),
                215, 190, 50, 100);
            g.DrawRectangle(
                new Pen(Color.Black, 2),
                215, 190, 50, 100);


            //крыша
            g.FillPolygon(
                new SolidBrush(Color.Green),
                new PointF[]
                {
                    new PointF(180, 150),
                    new PointF(275, 70),
                    new PointF(370, 150),
                });

            g.DrawPolygon(
                new Pen(Color.Black, 2),
                new PointF[]
                {
                    new PointF(180, 150),
                    new PointF(275, 70),
                    new PointF(370, 150),
                });

            //забор слева
            for (int i = 0; i < 5; i++)
            {
                g.FillPolygon(
                new SolidBrush(Color.Brown),
                new PointF[]
                {
                    new PointF(0 + i * 40, 300),
                    new PointF(0 + i * 40, 250),
                    new PointF(20 + i * 40, 220),
                    new PointF(40 + i * 40, 250),
                    new PointF(40 + i * 40, 300),
                });

                g.DrawPolygon(
                    new Pen(Color.Black, 2),
                    new PointF[]
                    {
                    new PointF(0 + i * 40, 300),
                    new PointF(0 + i * 40, 250),
                    new PointF(20 + i * 40, 220),
                    new PointF(40 + i * 40, 250),
                    new PointF(40 + i * 40, 300),
                    });
            }

            //забор справа
            for (int i = 0; i < 10; i++)
            {
                g.FillPolygon(
                new SolidBrush(Color.Brown),
                new PointF[]
                {
                    new PointF(350 + i * 40, 300),
                    new PointF(350 + i * 40, 250),
                    new PointF(370 + i * 40, 220),
                    new PointF(390 + i * 40, 250),
                    new PointF(390 + i * 40, 300),
                });

                g.DrawPolygon(
                    new Pen(Color.Black, 2),
                    new PointF[]
                    {
                    new PointF(350 + i * 40, 300),
                    new PointF(350 + i * 40, 250),
                    new PointF(370 + i * 40, 220),
                    new PointF(390 + i * 40, 250),
                    new PointF(390 + i * 40, 300),
                    });
            }

        }
    }
}
