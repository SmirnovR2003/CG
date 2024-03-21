using OpenTK;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Drawing;

namespace _2._8
{
    internal class Torus
    {

        private readonly float R = 10f;
        private readonly float r = 3f;
        private readonly float step = MathF.PI /70;

        private void SetColorByCoords(Vector3 p)
        {
            p.Normalize();
            GL.Color3(1 - p.X, 1 - p.Y, 1 - p.Z);
        }

        private void SetVertex(float a, float b)
        {
            Vector3 center = new Vector3(R * MathF.Cos(b), R * MathF.Sin(b), 0);

            Vector3 p = new Vector3(
                (R + r * MathF.Cos(a)) * MathF.Cos(b),
                (R + r * MathF.Cos(a)) * MathF.Sin(b),
                r * MathF.Sin(a));
            var normal = p - center;
            normal.Normalize();
            GL.Normal3(normal);
            SetColorByCoords(p);
            GL.Vertex3(p);
        }

        public void Draw()
        {
            GL.Begin(PrimitiveType.Quads);
            for (float b = 0; b < 2 * MathF.PI; b += step)
            {
                for (float a = 0; a < 2 * MathF.PI; a += step)
                {
                    SetVertex(a, b);
                    SetVertex(a + step, b);
                    SetVertex(a + step, b + step);
                    SetVertex(a, b + step);
                }
            }
            GL.End();
        }
    }
}
