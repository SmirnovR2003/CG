using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3
{
    public partial class Form1 : Form
    {
        private int counter = 0;
        public Form1()
        {
            InitializeComponent();
            fillColorComboBox.SelectedIndex = 0;
            borderColorComboBox.SelectedIndex = 0;
        }

        private void drawButton_Click(object sender, EventArgs e)
        {
            Graphics g = canvas.CreateGraphics();
            
            FillCircle(
                g,
                new Point(((int)centerXValue.Value), ((int)centerYValue.Value)),
                ((int)radiusValue.Value),
                Color.FromName(fillColorComboBox.AccessibilityObject.Value));

            DrawCircle(
                g,
                new Point(((int)centerXValue.Value), ((int)centerYValue.Value)),
                ((int)radiusValue.Value),
                Color.FromName(borderColorComboBox.AccessibilityObject.Value));
        }

        private void DrawCircle(Graphics g, Point center, int radius, Color color)
        {
            int x = 0;
            int y = radius;
            int delta = 1 - 2 * radius;
            int error = 0;
            while (y >= 0)
            {
                g.DrawRectangle(new Pen(color), center.X + x, center.Y + y, 1, 1);
                g.DrawRectangle(new Pen(color), center.X + x, center.Y - y, 1, 2);
                g.DrawRectangle(new Pen(color), center.X - x, center.Y + y, 1, 1);
                g.DrawRectangle(new Pen(color), center.X - x, center.Y - y, 1, 1);
                error = 2 * (delta + y) - 1;
                if (delta < 0 && error <= 0)
                {
                    ++x;
                    delta += 2 * x + 1;
                    continue;
                }
                error = 2 * (delta - x) - 1;
                if (delta > 0 && error > 0)
                {
                    --y;
                    delta += 1 - 2 * y;
                    continue;
                }
                ++x;
                delta += 2 * (x - y);
                --y;
            }
        }



        private void FillCircle(Graphics g, Point center, int radius, Color color)
        {
            int x = 0;
            int y = radius;
            int delta = 1 - 2 * radius;
            int error = 0;
            while (y >= 0)
            {
                //сделать так, чтобы линии не рисовались друг на друге

                g.DrawLine(new Pen(color), center.X + x, center.Y + y, center.X - x, center.Y + y);
                g.DrawLine(new Pen(color), center.X + x, center.Y - y, center.X - x, center.Y - y);
                counter++;
                error = 2 * (delta + y) - 1;
                if (delta < 0 && error <= 0)
                {
                    ++x;
                    delta += 2 * x + 1;
                    continue;
                }
                error = 2 * (delta - x) - 1;
                if (delta > 0 && error > 0)
                {
                    --y;
                    delta += 1 - 2 * y;
                    continue;
                }
                ++x;
                delta += 2 * (x - y);
                --y;
            }

        }
    }
}
