using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2
{
    internal class Drawer
    {
        public Drawer()
        {
        }

        private int Sign(int value)
        {
            return ((0 < value) ? 1 : 0) - ((value < 0) ? 1 : 0);
        }

        private void DrawSteepLine(Bitmap bmp, Point from, Point to, Color color)
        {
            int deltaX = Math.Abs(to.X - from.X);
            int deltaY = Math.Abs(to.Y - from.Y);


            if (from.Y > to.Y)
            {
                (from, to) = (to, from);
            }

            int stepX = Sign(to.X - from.X);
            int errorThreshold = deltaY + 1;
            int deltaErr = deltaX + 1;

            int error = deltaErr / 2;

            for (Point p = from; p.Y <= to.Y; ++p.Y)
            {
                if (p.X >= 0 && p.Y >= 0 && bmp.Width > p.X && bmp.Height > p.Y)
                    bmp.SetPixel(p.X, p.Y, color);

                error += deltaErr;

                if (error >= errorThreshold)
                {
                    p.X += stepX;
                    error -= errorThreshold;
                }
            }
        }



        private void DrawSlopeLine(Bitmap bmp, Point from, Point to, Color color)
        {
            int deltaX = Math.Abs(to.X - from.X);
            int deltaY = Math.Abs(to.Y - from.Y);


            if (from.X > to.X)
            {
                (from, to) = (to, from);
            }

            int stepY = Sign(to.Y - from.Y);
            int errorThreshold = deltaX + 1;
            int deltaErr = deltaY + 1;

            int error = deltaErr / 2;

            for (Point p = from; p.X <= to.X; ++p.X)
            {
                if (p.X >= 0 && p.Y >= 0 && bmp.Width > p.X && bmp.Height > p.Y)
                    bmp.SetPixel(p.X, p.Y, color);

                error += deltaErr;

                if (error >= errorThreshold)
                {
                    p.Y += stepY;
                    error -= errorThreshold;
                }
            }
        }

        private void DrawLine(Bitmap bmp, Point from, Point to, Color color)
        {
            int deltaX = Math.Abs(to.X - from.X);
            int deltaY = Math.Abs(to.Y - from.Y);

            if (deltaY > deltaX)
            {
                DrawSteepLine(bmp, from, to, color);
            }
            else
            {
                DrawSlopeLine(bmp, from, to, color);
            }
        }

        public void FillCircle(Bitmap bmp, Point center, int radius, Color color)
        {
            int x = 0;
            int y = radius;
            int delta = 1 - 2 * radius;
            int error = 0;
            while (y >= 0)
            {

                DrawLine(bmp, new Point(center.X + x, center.Y + y), new Point(center.X - x, center.Y + y), color);
                DrawLine(bmp, new Point(center.X + x, center.Y - y), new Point(center.X - x, center.Y - y), color);
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
