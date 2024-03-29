using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1._2
{
    internal class Yard
    {
        public int GrassTexture { get; set; }
        public int FenceTexture { get; set; }

        public Box2 GrassTextureCoord { get; set; } = new(0f, 0f, 1f, 1f);
        public Box2 FenceTextureCoord { get; set; } = new(0f, 0f, 4f, 1f);

        public Box3 GrassCoords { get; set; } = new(-35f, -10f, -25f, 35f, -10f, 25f);
        public Box3 FenceCoords { get; set; } = new(-35f, -10f, -25f, -35f, 0f, 25f);

        public int FenceHeight { get; set; } = 10;



        public void Draw()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            DrawGrass();

            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Front);
            DrawFence();
            GL.CullFace(CullFaceMode.Back);
            DrawFence();
            GL.Disable(EnableCap.CullFace);
        }

        private void DrawGrass()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, GrassTexture);
            GL.Begin(PrimitiveType.Quads);

            GL.Normal3(0f, 1f, 0f);

            GL.TexCoord2(GrassTextureCoord.Min.X, GrassTextureCoord.Min.Y);
            GL.Vertex3(GrassCoords.Min.X, GrassCoords.Max.Y, GrassCoords.Min.Z);

            GL.TexCoord2(GrassTextureCoord.Min.X, GrassTextureCoord.Max.Y);
            GL.Vertex3(GrassCoords.Min.X, GrassCoords.Min.Y, GrassCoords.Max.Z);

            GL.TexCoord2(GrassTextureCoord.Max.X, GrassTextureCoord.Max.Y);
            GL.Vertex3(GrassCoords.Max.X, GrassCoords.Min.Y, GrassCoords.Max.Z);

            GL.TexCoord2(GrassTextureCoord.Max.X, GrassTextureCoord.Min.Y);
            GL.Vertex3(GrassCoords.Max.X, GrassCoords.Max.Y, GrassCoords.Min.Z);

            GL.End();

        }

        private void DrawFence()
        {
            float ratio = (GrassCoords.Max.X - GrassCoords.Min.X) / (GrassCoords.Max.Z - GrassCoords.Min.Z);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, FenceTexture);
            GL.Begin(PrimitiveType.Quads);

            //левая сторона
            GL.Normal3(-1f, 0f, 0f);
            GL.TexCoord2(FenceTextureCoord.Min.X / ratio, FenceTextureCoord.Min.Y);
            GL.Vertex3(GrassCoords.Min.X, GrassCoords.Min.Y + FenceHeight, GrassCoords.Min.Z);

            GL.TexCoord2(FenceTextureCoord.Min.X / ratio, FenceTextureCoord.Max.Y);
            GL.Vertex3(GrassCoords.Min.X, GrassCoords.Min.Y, GrassCoords.Min.Z);

            GL.TexCoord2(FenceTextureCoord.Max.X / ratio, FenceTextureCoord.Max.Y);
            GL.Vertex3(GrassCoords.Min.X, GrassCoords.Min.Y, GrassCoords.Max.Z);

            GL.TexCoord2(FenceTextureCoord.Max.X / ratio, FenceTextureCoord.Min.Y);
            GL.Vertex3(GrassCoords.Min.X, GrassCoords.Min.Y + FenceHeight, GrassCoords.Max.Z);

            GL.Normal3(1f, 0f, 0f);
            GL.TexCoord2(FenceTextureCoord.Min.X / ratio, FenceTextureCoord.Min.Y);
            GL.Vertex3(GrassCoords.Min.X + 0.1f, GrassCoords.Min.Y + FenceHeight, GrassCoords.Min.Z);

            GL.TexCoord2(FenceTextureCoord.Min.X / ratio, FenceTextureCoord.Max.Y);
            GL.Vertex3(GrassCoords.Min.X + 0.1f, GrassCoords.Min.Y, GrassCoords.Min.Z);

            GL.TexCoord2(FenceTextureCoord.Max.X / ratio, FenceTextureCoord.Max.Y);
            GL.Vertex3(GrassCoords.Min.X + 0.1f, GrassCoords.Min.Y, GrassCoords.Max.Z);

            GL.TexCoord2(FenceTextureCoord.Max.X / ratio, FenceTextureCoord.Min.Y);
            GL.Vertex3(GrassCoords.Min.X + 0.1f, GrassCoords.Min.Y + FenceHeight, GrassCoords.Max.Z);

            //--------------------------------------------------------------

            //задняя сторона
            GL.Normal3(0f, 0f, -1f);
            GL.TexCoord2(FenceTextureCoord.Min.X, FenceTextureCoord.Min.Y);
            GL.Vertex3(GrassCoords.Min.X, GrassCoords.Min.Y + FenceHeight, GrassCoords.Min.Z);

            GL.TexCoord2(FenceTextureCoord.Max.X, FenceTextureCoord.Min.Y);
            GL.Vertex3(GrassCoords.Max.X, GrassCoords.Min.Y + FenceHeight, GrassCoords.Min.Z);

            GL.TexCoord2(FenceTextureCoord.Max.X, FenceTextureCoord.Max.Y);
            GL.Vertex3(GrassCoords.Max.X, GrassCoords.Min.Y, GrassCoords.Min.Z);

            GL.TexCoord2(FenceTextureCoord.Min.X, FenceTextureCoord.Max.Y);
            GL.Vertex3(GrassCoords.Min.X, GrassCoords.Min.Y, GrassCoords.Min.Z);

            GL.Normal3(0f, 0f, 1f);
            GL.TexCoord2(FenceTextureCoord.Min.X, FenceTextureCoord.Min.Y);
            GL.Vertex3(GrassCoords.Min.X, GrassCoords.Min.Y + FenceHeight, GrassCoords.Min.Z + 0.1f);

            GL.TexCoord2(FenceTextureCoord.Max.X, FenceTextureCoord.Min.Y);
            GL.Vertex3(GrassCoords.Max.X, GrassCoords.Min.Y + FenceHeight, GrassCoords.Min.Z + 0.1f);

            GL.TexCoord2(FenceTextureCoord.Max.X, FenceTextureCoord.Max.Y);
            GL.Vertex3(GrassCoords.Max.X, GrassCoords.Min.Y, GrassCoords.Min.Z + 0.1f);

            GL.TexCoord2(FenceTextureCoord.Min.X, FenceTextureCoord.Max.Y);
            GL.Vertex3(GrassCoords.Min.X, GrassCoords.Min.Y, GrassCoords.Min.Z + 0.1f);

            //--------------------------------------------------------------

            //правая сторона
            GL.Normal3(1f, 0f, 0f);
            GL.TexCoord2(FenceTextureCoord.Min.X / ratio, FenceTextureCoord.Min.Y);
            GL.Vertex3(GrassCoords.Max.X, GrassCoords.Min.Y + FenceHeight, GrassCoords.Min.Z);

            GL.TexCoord2(FenceTextureCoord.Max.X / ratio, FenceTextureCoord.Min.Y);
            GL.Vertex3(GrassCoords.Max.X, GrassCoords.Min.Y + FenceHeight, GrassCoords.Max.Z);

            GL.TexCoord2(FenceTextureCoord.Max.X / ratio, FenceTextureCoord.Max.Y);
            GL.Vertex3(GrassCoords.Max.X, GrassCoords.Min.Y, GrassCoords.Max.Z);

            GL.TexCoord2(FenceTextureCoord.Min.X / ratio, FenceTextureCoord.Max.Y);
            GL.Vertex3(GrassCoords.Max.X, GrassCoords.Min.Y, GrassCoords.Min.Z);

            GL.Normal3(-1f, 0f, 0f);
            GL.TexCoord2(FenceTextureCoord.Min.X / ratio, FenceTextureCoord.Min.Y);
            GL.Vertex3(GrassCoords.Max.X - 0.1f, GrassCoords.Min.Y + FenceHeight, GrassCoords.Min.Z);

            GL.TexCoord2(FenceTextureCoord.Max.X / ratio, FenceTextureCoord.Min.Y);
            GL.Vertex3(GrassCoords.Max.X - 0.1f, GrassCoords.Min.Y + FenceHeight, GrassCoords.Max.Z);

            GL.TexCoord2(FenceTextureCoord.Max.X / ratio, FenceTextureCoord.Max.Y);
            GL.Vertex3(GrassCoords.Max.X - 0.1f, GrassCoords.Min.Y, GrassCoords.Max.Z);

            GL.TexCoord2(FenceTextureCoord.Min.X / ratio, FenceTextureCoord.Max.Y);
            GL.Vertex3(GrassCoords.Max.X - 0.1f, GrassCoords.Min.Y, GrassCoords.Min.Z);



            GL.End();

        }
    }
}
