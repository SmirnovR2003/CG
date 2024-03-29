using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1._2
{
    internal class SkySphere
    {
        public SkySphere() { }

        private int _displayList = 0;

        public float Radius { get; set; } = 200f;
        public int Slices { get; set; } = 100;
        public int Stacks { get; set; } = 50;
        public int SkySphereTexture { get; set; }

        private void DrawSphere()
        {
            // шаг по параллелям
            float stackStep = MathF.PI / Stacks;

            // шаг по меридианам
            float sliceStep = 2 * MathF.PI / Slices;

            // начальный угол по параллелям
            for (int stack = 0; stack < Stacks; ++stack)
            {
                float stackAngle = MathF.PI * 0.5f - stack * stackStep;
                float nextStackAngle = stackAngle - stackStep;

                float stackRadius = Radius * MathF.Cos(stackAngle);
                float nextStackRadius = Radius * MathF.Cos(nextStackAngle);
                float z0 = Radius * MathF.Sin(stackAngle);
                float z1 = Radius * MathF.Sin(nextStackAngle);

                GL.Begin(PrimitiveType.TriangleStrip);
                // цикл по меридианам
                for (int slice = 0; slice <= Slices; ++slice)
                {
                    // вычисляем угол, текущего меридиана
                    float sliceAngle = (slice != Slices) ? slice * sliceStep : 0;

                    // Вычисляем координаты на текущей параллели
                    float x0 = stackRadius * MathF.Cos(sliceAngle);
                    float y0 = stackRadius * MathF.Sin(sliceAngle);
                    // вычисляем и задаем вектор нормали, текстурные координаты
                    // и положение вершины в пространстве
                    Vector3 normal0 = (x0, y0, z0);
                    normal0.Normalize();
                    GL.Normal3(-normal0);
                    GL.TexCoord2((float)(slice) / Slices, (float)(stack) / Stacks);
                    GL.Vertex3(x0, y0, z0);

                    float x1 = nextStackRadius * MathF.Cos(sliceAngle);
                    float y1 = nextStackRadius * MathF.Sin(sliceAngle);
                    Vector3 normal1 = (x1, y1, z1);
                    normal1.Normalize();
                    GL.Normal3(-normal1);
                    GL.TexCoord2((float)(slice) / Slices, (float)(stack + 1) / Stacks);
                    GL.Vertex3(x1, y1, z1);
                }
                GL.End();
            }
        }

        public void Draw()
        {
            if (_displayList == 0)
            {
                _displayList = GL.GenLists(1);
                GL.NewList(_displayList,ListMode.Compile);

                DrawSphere();

                GL.EndList();
            }

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, SkySphereTexture);

            GL.CallList(_displayList);
        }

        ~SkySphere()
        {
            if (_displayList != 0)
            {
                GL.DeleteLists(_displayList, 1);
            }
        }
    }
}
