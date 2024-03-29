using OpenTK;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Drawing;


namespace _1._2
{
    internal class Cottage
    {
        public Cottage() 
        {
            house.WallTexture = brickWallTexture;
            house.DoorTexture = doorTexture;
            house.RootTexture = rootTilesTexture;
            house.GrafityTexture = grafityTexture;
            house.WindowTexture = windowTexture;
            house.AtticBoardsTexture = atticBoardsTexture;

            garage.WallTexture = brickWallTexture;
            garage.GarageDoorTexture = garageDoorTexture;
            garage.RootTexture = rootTilesTexture;
            garage.GrafityTexture = grafityTexture;
            garage.WindowTexture = windowTexture;
            garage.AtticBoardsTexture = atticBoardsTexture;

            yard.GrassTexture = grassTexture;
            yard.FenceTexture = fenceTexture;

            skySphere.SkySphereTexture = skysphereTexture;
        }

        static Texture texture = new Texture();
        private int brickWallTexture = texture.LoadTexture(
            "images/brick-wall.jpg", 
            TextureMagFilter.LinearDetailSgis,
            TextureMinFilter.Linear,
            TextureWrapMode.Repeat,
            TextureWrapMode.Repeat);
        private int doorTexture = texture.LoadTexture(
            "images/door.jpg", 
            TextureMagFilter.Linear,
            TextureMinFilter.Linear,
            TextureWrapMode.Repeat,
            TextureWrapMode.Repeat);
        private int garageDoorTexture = texture.LoadTexture(
            "images/garage-door.jpg", 
            TextureMagFilter.Linear,
            TextureMinFilter.Linear,
            TextureWrapMode.Repeat,
            TextureWrapMode.Repeat);
        private int grassTexture = texture.LoadTexture(
            "images/grass.jpg", 
            TextureMagFilter.Linear,
            TextureMinFilter.Linear,
            TextureWrapMode.Repeat,
            TextureWrapMode.Repeat);
        private int windowTexture = texture.LoadTexture(
            "images/window.jpg", 
            TextureMagFilter.Linear,
            TextureMinFilter.Linear,
            TextureWrapMode.Repeat,
            TextureWrapMode.Repeat);
        private int rootTilesTexture = texture.LoadTexture(
            "images/root-tiles.jpg", 
            TextureMagFilter.Linear,
            TextureMinFilter.Linear,
            TextureWrapMode.Repeat,
            TextureWrapMode.Repeat);
        private int grafityTexture = texture.LoadTexture(
            "images/grafity.png", 
            TextureMagFilter.Linear,
            TextureMinFilter.Linear,
            TextureWrapMode.ClampToBorder,
            TextureWrapMode.ClampToBorder);
        private int atticBoardsTexture = texture.LoadTexture(
            "images/attic-boards.jpg", 
            TextureMagFilter.Linear,
            TextureMinFilter.Linear,
            TextureWrapMode.Repeat,
            TextureWrapMode.Repeat);
        private int fenceTexture = texture.LoadTexture(
            "images/fence.png", 
            TextureMagFilter.Linear,
            TextureMinFilter.Linear,
            TextureWrapMode.Repeat,
            TextureWrapMode.Repeat);
        private int skysphereTexture = texture.LoadTexture(
            "images/sky.jpg", 
            TextureMagFilter.Linear,
            TextureMinFilter.Linear,
            TextureWrapMode.Repeat,
            TextureWrapMode.Repeat);

        House house = new();
        Garage garage = new();
        Yard yard = new ();
        SkySphere skySphere = new();

        public bool ShowFog = false;

        public void Draw()
        {

            //небо без тумана
            // Включаем либо выключаем туман
            if (ShowFog)
            {
                GL.Enable(EnableCap.Fog);
            }
            else
            {
                GL.Disable(EnableCap.Fog);
            }

            // Задаем режим тумана
            GL.Fog(FogParameter.FogMode, (int)FogMode.Exp2);

            // Задаем цвет тумана
            GL.Fog(FogParameter.FogColor, new float[] { 0.5f, 0.5f, 0.5f, 0f });

            // и его плотность
            GL.Fog(FogParameter.FogDensity, 0.015f);

            house.Draw();
            garage.Draw();
            yard.Draw();
            GL.Disable(EnableCap.Fog);

            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.Light1);
            //подобрать безшовную текстуру
            skySphere.Draw();

            GL.Disable(EnableCap.Light0);
            GL.Disable(EnableCap.Light1);

        }
    }
}
