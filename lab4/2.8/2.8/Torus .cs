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

        public void Draw()
        {
           
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Front);
            GL.PointSize(5);
            GL.LineWidth(10);
            GL.Color3(Color.Red);
            GL.Begin(PrimitiveType.QuadStrip);
            for (float a = 0; a < 2 * MathF.PI; a += 0.05f)
            {
                for(float b = 0; b < 2 * MathF.PI; b += 0.05f)
                {
                    Vector3 center = new Vector3(R * MathF.Cos(b), R * MathF.Sin(b), 0);
                    Vector3 p1 = new Vector3(
                        (R + r * MathF.Cos(a)) * MathF.Cos(b),
                        (R + r * MathF.Cos(a)) * MathF.Sin(b),
                        r * MathF.Sin(a));

                    var normal = p1 - center;
                    normal.Normalize();

                    GL.Normal3(normal);

                    GL.Vertex3(p1);

                    Vector3 p2 = new Vector3(
                        (R + r * MathF.Cos(a + 0.1f)) * MathF.Cos(b),
                        (R + r * MathF.Cos(a + 0.1f)) * MathF.Sin(b),
                        r * MathF.Sin(a + 0.1f)); 
                    
                    normal = p2 - center;
                    normal.Normalize();

                    GL.Normal3(normal);

                    GL.Vertex3(p2);
                }
            }
            GL.End();
        }
    }
}
