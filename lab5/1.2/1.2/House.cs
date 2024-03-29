using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1._2
{
    internal class House
    {
        public House() { }

        public int WallTexture { get; set; }
        public int DoorTexture { get; set; }
        public int RootTexture { get; set; }
        public int GrafityTexture { get; set; }
        public int WindowTexture { get; set; }
        public int AtticBoardsTexture { get; set; }

        public Box2 WallTextureCoord { get; set; } = new(0f, 0f, 1f, 1f);
        public Box2 DoorTextureCoord { get; set; } = new(0f, 0f, 1f, 1f);
        public Box2 RootTextureCoord { get; set; } = new(0f, 0f, 1f, 1f);
        public Box2 GrafiryTextureCoord { get; set; } = new(0f, -1f, 1f, 1f);
        public Box2 WindowTextureCoord { get; set; } = new(0f, 0f, 1f, 1f);
        public Box2 AtticBoardsTextureCoord { get; set; } = new(0f, 0f, 1f, 1f);

        public Box3 WallCoords { get; set; } = new(-20f, -10f, -15f, 0f, 10f, 15f);
        public Box3 DoorCoords { get; set; } = new(-12f, -10f, 15.1f, -8f, -3f, 15.1f);
        public Box3 RootCoords { get; set; } = new(-22f, 8f, -17f, 2f, 20f, 17f);
        //public Box3 GrafityCoords { get; set; } = new Box3(); 
        public Box3[] WindowsCoords { get; set; } = 
        {
            new Box3(-16f, 2f, 15.1f, -12f, 9f, 15.1f),
            new Box3(-8f, 2f, 15.1f, -4f, 9f, 15.1f),

            new Box3(-20.1f, 0f, -12f, -20.1f, 7f, -8f),
            new Box3(-20.1f, 0f, 8f, -20.1f, 7f, 12f),
            new Box3(-20.1f, 0f, -2f, -20.1f, 7f, 2f),

            new Box3(-16f, 2f, -15.1f, -12f, 9f, -15.1f),
            new Box3(-8f, 2f, -15.1f, -4f, 9f, -15.1f),
            new Box3(-16f, -7f, -15.1f, -12f, 0f, -15.1f),
            new Box3(-8f, -7f, -15.1f, -4f, 0f, -15.1f),

            new Box3(0.1f, 0f, -12f, 0.1f, 7f, -8f),
        };



        public void Draw()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            DrawHouseBox();
            DrawHouseRoof();
            DrawHouseWindows();
            DrawDoor();
        }

        private void DrawHouseBox()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, WallTexture);

            GL.ActiveTexture(TextureUnit.Texture1);
            GL.Enable(EnableCap.Texture2D);
            GL.TexEnv(TextureEnvTarget.TextureEnv,TextureEnvParameter.TextureEnvMode,(int)All.Decal);
            GL.BindTexture(TextureTarget.Texture2D, GrafityTexture);

            GL.Begin(PrimitiveType.Quads);

            //задняя стена
            GL.Normal3(0f, 0f, -1f);
            GL.MultiTexCoord2(TextureUnit.Texture0, WallTextureCoord.Min.X, WallTextureCoord.Min.Y);
            //GL.MultiTexCoord2(TextureUnit.Texture1, GrafiryTextureCoord.Min.X, GrafiryTextureCoord.Min.Y);
            GL.Vertex3(WallCoords.Min.X, WallCoords.Max.Y, WallCoords.Min.Z);

            GL.MultiTexCoord2(TextureUnit.Texture0, WallTextureCoord.Min.X, WallTextureCoord.Max.Y);
            //GL.MultiTexCoord2(TextureUnit.Texture1, GrafiryTextureCoord.Min.X, GrafiryTextureCoord.Max.Y);
            GL.Vertex3(WallCoords.Min.X, WallCoords.Min.Y, WallCoords.Min.Z);

            GL.MultiTexCoord2(TextureUnit.Texture0, WallTextureCoord.Max.X, WallTextureCoord.Max.Y);
            //GL.MultiTexCoord2(TextureUnit.Texture1, GrafiryTextureCoord.Max.X, GrafiryTextureCoord.Max.Y);
            GL.Vertex3(WallCoords.Max.X, WallCoords.Min.Y, WallCoords.Min.Z);

            GL.MultiTexCoord2(TextureUnit.Texture0, WallTextureCoord.Max.X, WallTextureCoord.Min.Y);
            //GL.MultiTexCoord2(TextureUnit.Texture1, GrafiryTextureCoord.Max.X, GrafiryTextureCoord.Min.Y);
            GL.Vertex3(WallCoords.Max.X, WallCoords.Max.Y, WallCoords.Min.Z);

            //------------------------------------------------------
            //левая стена
            GL.Normal3(-1f, 0f, 0f);
            GL.MultiTexCoord2(TextureUnit.Texture0, WallTextureCoord.Max.X, WallTextureCoord.Max.Y);
            GL.MultiTexCoord2(TextureUnit.Texture1, GrafiryTextureCoord.Min.X, GrafiryTextureCoord.Max.Y);
            GL.Vertex3(WallCoords.Min.X, WallCoords.Min.Y, WallCoords.Min.Z);

            GL.MultiTexCoord2(TextureUnit.Texture0, WallTextureCoord.Max.X, WallTextureCoord.Min.Y);
            GL.MultiTexCoord2(TextureUnit.Texture1, GrafiryTextureCoord.Min.X, GrafiryTextureCoord.Min.Y);
            GL.Vertex3(WallCoords.Min.X, WallCoords.Max.Y, WallCoords.Min.Z);

            GL.MultiTexCoord2(TextureUnit.Texture0, WallTextureCoord.Min.X, WallTextureCoord.Min.Y);
            GL.MultiTexCoord2(TextureUnit.Texture1, GrafiryTextureCoord.Max.X, GrafiryTextureCoord.Min.Y);
            GL.Vertex3(WallCoords.Min.X, WallCoords.Max.Y, WallCoords.Max.Z);

            GL.MultiTexCoord2(TextureUnit.Texture0, WallTextureCoord.Min.X, WallTextureCoord.Max.Y);
            GL.MultiTexCoord2(TextureUnit.Texture1, GrafiryTextureCoord.Max.X, GrafiryTextureCoord.Max.Y);
            GL.Vertex3(WallCoords.Min.X, WallCoords.Min.Y, WallCoords.Max.Z);
            //-------------------------------------------
            //передняя стена
            GL.Normal3(0f, 0f, 1f);
            GL.TexCoord2(WallTextureCoord.Min.X, WallTextureCoord.Min.Y);
            GL.Vertex3(WallCoords.Min.X, WallCoords.Max.Y, WallCoords.Max.Z);

            GL.TexCoord2(WallTextureCoord.Min.X, WallTextureCoord.Max.Y);
            GL.Vertex3(WallCoords.Min.X, WallCoords.Min.Y, WallCoords.Max.Z);

            GL.TexCoord2(WallTextureCoord.Max.X, WallTextureCoord.Max.Y);
            GL.Vertex3(WallCoords.Max.X, WallCoords.Min.Y, WallCoords.Max.Z);

            GL.TexCoord2(WallTextureCoord.Max.X, WallTextureCoord.Min.Y);
            GL.Vertex3(WallCoords.Max.X, WallCoords.Max.Y, WallCoords.Max.Z);
            //--------------------------------------------------------
            //правая стена
            GL.Normal3(1f, 0f, 0f);
            GL.TexCoord2(WallTextureCoord.Max.X, WallTextureCoord.Max.Y);
            GL.Vertex3(WallCoords.Max.X, WallCoords.Min.Y, WallCoords.Max.Z);

            GL.TexCoord2(WallTextureCoord.Max.X, WallTextureCoord.Min.Y);
            GL.Vertex3(WallCoords.Max.X, WallCoords.Max.Y, WallCoords.Max.Z);

            GL.TexCoord2(WallTextureCoord.Min.X, WallTextureCoord.Min.Y);
            GL.Vertex3(WallCoords.Max.X, WallCoords.Max.Y, WallCoords.Min.Z);

            GL.TexCoord2(WallTextureCoord.Min.X, WallTextureCoord.Max.Y);
            GL.Vertex3(WallCoords.Max.X, WallCoords.Min.Y, WallCoords.Min.Z);

            GL.End();

        }

        private void DrawHouseRoof()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, RootTexture);
            GL.Begin(PrimitiveType.Quads);

            GL.Normal3(1f, 1f, 0f);
            GL.TexCoord2(RootTextureCoord.Max.X, RootTextureCoord.Max.Y);
            GL.Vertex3(RootCoords.Max.X, RootCoords.Min.Y, RootCoords.Max.Z);

            GL.TexCoord2(RootTextureCoord.Min.X, RootTextureCoord.Max.Y);
            GL.Vertex3(RootCoords.Max.X, RootCoords.Min.Y, RootCoords.Min.Z);

            GL.TexCoord2(RootTextureCoord.Min.X, RootTextureCoord.Min.Y);
            GL.Vertex3(RootCoords.Center.X, RootCoords.Max.Y, RootCoords.Min.Z);


            GL.TexCoord2(RootTextureCoord.Max.X, RootTextureCoord.Min.Y);
            GL.Vertex3(RootCoords.Center.X, RootCoords.Max.Y, RootCoords.Max.Z);
            //---------------------------------------------------

            GL.Normal3(-1f, 1f, 0f);
            GL.TexCoord2(RootTextureCoord.Max.X, RootTextureCoord.Max.Y);
            GL.Vertex3(RootCoords.Min.X, RootCoords.Min.Y, RootCoords.Max.Z);

            GL.TexCoord2(RootTextureCoord.Max.X, RootTextureCoord.Min.Y);
            GL.Vertex3(RootCoords.Center.X, RootCoords.Max.Y, RootCoords.Max.Z);

            GL.TexCoord2(RootTextureCoord.Min.X, RootTextureCoord.Min.Y);
            GL.Vertex3(RootCoords.Center.X, RootCoords.Max.Y, RootCoords.Min.Z);

            GL.TexCoord2(RootTextureCoord.Min.X, RootTextureCoord.Max.Y);
            GL.Vertex3(RootCoords.Min.X, RootCoords.Min.Y, RootCoords.Min.Z);

            GL.End();


            GL.BindTexture(TextureTarget.Texture2D, AtticBoardsTexture);
            GL.Begin(PrimitiveType.Triangles);


            GL.Normal3(0f, 0f, 1f);

            GL.TexCoord2(WallTextureCoord.Min.X, WallTextureCoord.Max.Y);
            GL.Vertex3(WallCoords.Min.X, WallCoords.Max.Y, WallCoords.Max.Z);

            GL.TexCoord2(WallTextureCoord.Max.X, WallTextureCoord.Max.Y);
            GL.Vertex3(WallCoords.Max.X, WallCoords.Max.Y, WallCoords.Max.Z);

            GL.TexCoord2(
                WallTextureCoord.Center.X,
                0);
            GL.Vertex3(RootCoords.Center.X, RootCoords.Max.Y, WallCoords.Max.Z);
            //-------------------------------------------
            GL.Normal3(0f, 0f, -1f);

            GL.TexCoord2(WallTextureCoord.Min.X, WallTextureCoord.Max.Y);
            GL.Vertex3(WallCoords.Min.X, WallCoords.Max.Y, WallCoords.Min.Z);

            GL.TexCoord2(WallTextureCoord.Max.X, WallTextureCoord.Max.Y);
            GL.Vertex3(WallCoords.Max.X, WallCoords.Max.Y, WallCoords.Min.Z);

            GL.TexCoord2(
                WallTextureCoord.Center.X,
                0);
            GL.Vertex3(RootCoords.Center.X, RootCoords.Max.Y, WallCoords.Min.Z);


            GL.End();


        }

        private void DrawHouseWindows()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, WindowTexture);

            GL.Begin(PrimitiveType.Quads);

            foreach (var window in WindowsCoords)
            {
                SetNormalForWindow(window);
                GL.TexCoord2(WindowTextureCoord.Max.X, WindowTextureCoord.Max.Y);
                GL.Vertex3(window.Min.X, window.Min.Y, window.Min.Z);

                GL.TexCoord2(WindowTextureCoord.Max.X, WindowTextureCoord.Min.Y);
                GL.Vertex3(window.Min.X, window.Max.Y, window.Min.Z);

                GL.TexCoord2(WindowTextureCoord.Min.X, WindowTextureCoord.Min.Y);
                GL.Vertex3(window.Max.X, window.Max.Y, window.Max.Z);

                GL.TexCoord2(WindowTextureCoord.Min.X, WindowTextureCoord.Max.Y);
                GL.Vertex3(window.Max.X, window.Min.Y, window.Max.Z);
            }

            GL.End();

        }

        private void SetNormalForWindow(Box3 window)
        {
            if (window.Max.Z >= WallCoords.Max.Z)
            {
                GL.Normal3(0f, 0f, 1f);
            }
            else if (window.Min.Z <= WallCoords.Min.Z)
            {
                GL.Normal3(0f, 0f, -1f);
            }
            else if (window.Max.X >= WallCoords.Max.X)
            {
                GL.Normal3(1f, 0f, 0f);
            }
            else if (window.Min.X <= WallCoords.Min.X)
            {
                GL.Normal3(-1f, 0f, 0f);
            }
            else
                GL.Normal3(1f, 0f, 0f);
        }

        private void DrawDoor()
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, DoorTexture);

            GL.Begin(PrimitiveType.Quads);

            GL.Normal3(0f, 0f, 1f);
            GL.TexCoord2(DoorTextureCoord.Max.X, DoorTextureCoord.Max.Y);
            GL.Vertex3(DoorCoords.Min.X, DoorCoords.Min.Y, DoorCoords.Max.Z);

            GL.TexCoord2(DoorTextureCoord.Max.X, DoorTextureCoord.Min.Y);
            GL.Vertex3(DoorCoords.Min.X, DoorCoords.Max.Y, DoorCoords.Max.Z);

            GL.TexCoord2(DoorTextureCoord.Min.X, DoorTextureCoord.Min.Y);
            GL.Vertex3(DoorCoords.Max.X, DoorCoords.Max.Y, DoorCoords.Max.Z);

            GL.TexCoord2(DoorTextureCoord.Min.X, DoorTextureCoord.Max.Y);
            GL.Vertex3(DoorCoords.Max.X, DoorCoords.Min.Y, DoorCoords.Max.Z);


            GL.End();

        }
    }
}
