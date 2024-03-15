using OpenTK;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Audio.OpenAL.Extensions.EXT.Double;
using System;

namespace _1
    //сделать так чтобы сохранялось соотношение сторон
{
    public class Game : GameWindow
    {

        private readonly float k = 1f / ((float)Math.PI * 10);

        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(Color4.White);

        }

        protected override void OnResize(ResizeEventArgs e)
        {
            //SwapBuffers();//почитьать как правильно использавать

            base.OnResize(e);
            this.OnRefresh();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            //SwapBuffers();
            base.OnUpdateFrame(args);
            this.OnRefresh();
        }



        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            //настроить матрицу проецирования так, чтобы при изменении окна сохранялось размеры


            GL.Color4(Color4.Black);

            GL.Begin(PrimitiveType.LineStrip);
            // x=r*cosf; y=r*sinf
            for (float i = 0; i < 10 * Math.PI; i += 0.001f)
            {
                float r = GetR(i);
                GL.Vertex2(r * Math.Cos(i), r * Math.Sin(i));

            }
            GL.End();
            SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnUnload()
        {
            base.OnUnload();
        }

        float GetR(float f)
        {
            return k * f;
        }

    }
}
