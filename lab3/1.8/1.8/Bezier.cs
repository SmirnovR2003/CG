using OpenTK;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Drawing;

namespace _1._8
{
    //вынести в отдельный класс
    internal class Bezier : GameWindow
    {
        struct DragAndDrop
        {
            public bool start;
            public float startX;
            public float startY;
            public float lastX;
            public float lastY;
            public int chosenPoint;
        }

        private BezierCurveCubic bezierCurveCubic;
        private DragAndDrop dragAndDrop;
        private float clickRadius = 3f;

        public Bezier(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        public static bool NearlyEqual(float a, float b, float epsilon)
        {
            return Math.Abs(a - b) < epsilon;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(Color4.White);

            //рисовать в модельных координатах
            bezierCurveCubic = new BezierCurveCubic(
                new Vector2(-70f, -70f),
                new Vector2(70f, -70f),
                new Vector2(-70f, 70f),
                new Vector2(70f, 70f));

            dragAndDrop = new DragAndDrop();

        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            var mousePos = GetCursorPosInModalSpace(MousePosition.X, MousePosition.Y);
            float normalizedX = ((float)mousePos.X);
            float normalizedY = ((float)mousePos.Y);

            if (NearlyEqual(normalizedX, bezierCurveCubic.StartAnchor.X, clickRadius)
                && NearlyEqual(normalizedY, bezierCurveCubic.StartAnchor.Y, clickRadius))
            {
                dragAndDrop.chosenPoint = 0;
                dragAndDrop.start = true;
            }
            else if(NearlyEqual(normalizedX, bezierCurveCubic.FirstControlPoint.X, clickRadius)
                && NearlyEqual(normalizedY, bezierCurveCubic.FirstControlPoint.Y, clickRadius))
            {
                dragAndDrop.chosenPoint = 1;
                dragAndDrop.start = true;
            }
            else if (NearlyEqual(normalizedX, bezierCurveCubic.SecondControlPoint.X, clickRadius)
                && NearlyEqual(normalizedY, bezierCurveCubic.SecondControlPoint.Y, clickRadius))
            {
                dragAndDrop.chosenPoint = 2;
                dragAndDrop.start = true;
            }
            else if (NearlyEqual(normalizedX, bezierCurveCubic.EndAnchor.X, clickRadius)
                && NearlyEqual(normalizedY, bezierCurveCubic.EndAnchor.Y, clickRadius))
            {
                dragAndDrop.chosenPoint = 3;
                dragAndDrop.start = true;
            }
            
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            if (!dragAndDrop.start) return;

            var mousePos = GetCursorPosInModalSpace(MousePosition.X, MousePosition.Y);
            switch (dragAndDrop.chosenPoint)
            {
                case 0:
                    bezierCurveCubic.StartAnchor.X = ((float)mousePos.X);
                    bezierCurveCubic.StartAnchor.Y = ((float)mousePos.Y);
                    break;
                case 1:
                    bezierCurveCubic.FirstControlPoint.X = ((float)mousePos.X);
                    bezierCurveCubic.FirstControlPoint.Y = ((float)mousePos.Y);
                    break;
                case 2:
                    bezierCurveCubic.SecondControlPoint.X = ((float)mousePos.X);
                    bezierCurveCubic.SecondControlPoint.Y = ((float)mousePos.Y);
                    break;
                case 3:
                    bezierCurveCubic.EndAnchor.X = ((float)mousePos.X);
                    bezierCurveCubic.EndAnchor.Y = ((float)mousePos.Y) ;
                    break;
                default: break;
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            dragAndDrop.start = false;
            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave()
        {
            dragAndDrop.start = false;
            base.OnMouseLeave();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            int width = e.Width;
            int height = e.Height;

            GL.Viewport(0, 0, width, height);

            SetupProjectionMatrix(width, height);

            
            GL.MatrixMode(MatrixMode.Modelview);
            base.OnResize(e);

            OnRenderFrame(new FrameEventArgs());
        }

        void SetupProjectionMatrix(int width, int height)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            double aspectRatio = ((double)width) / ((double)height);
            double viewWidth = 200;
            double viewHeight = viewWidth;
            if (aspectRatio > 1.0)
            {
                viewWidth = viewHeight * aspectRatio;
            }
            else
            {
                viewHeight = viewWidth / aspectRatio;
            }
            GL.Ortho(-viewWidth * 0.5, viewWidth * 0.5, -viewHeight * 0.5, viewHeight * 0.5, 1.0, -1.0);
        }

        unsafe private Size GetFramebufferSize()
        {
            Size size = new Size();

            GLFW.GetFramebufferSize(WindowPtr, out int width, out int height);

            size.Width = width;
            size.Height = height;

            return size;
        }

        private Vector2d GetCursorPosInModalSpace(double x, double y)
        {
            GL.GetDouble(GetPName.ProjectionMatrix, out Matrix4d proj);
            GL.GetDouble(GetPName.ModelviewMatrix, out Matrix4d modelView);

            int[] viewport = new int[4];
            GL.GetInteger(GetPName.Viewport, viewport);

            var wndSize = GetFramebufferSize();

            Vector4d cursorNorm = new Vector4d(
                (x - viewport[0]) / (viewport[2] / 2) - 1.0,
                (wndSize.Height - y - viewport[1]) / (viewport[3] / 2) - 1.0,
                0.0,
                1.0
            );

            var mvp = proj * modelView;
            var mvpInvertse = mvp.Inverted();
            var cursorInModelSpace = mvpInvertse * cursorNorm;
            cursorInModelSpace.X /= cursorInModelSpace.W;
            cursorInModelSpace.Y /= cursorInModelSpace.W;

            return new Vector2d(cursorInModelSpace.X, cursorInModelSpace.Y);
        }

        private void DrawBezier(float step)
        {
            GL.Begin(PrimitiveType.LineStrip);
            for (float i = 0; i < 1; i += step)
            {
                GL.Vertex2(bezierCurveCubic.CalculatePoint(i));

            }
            GL.End();

        }

        private void DrawControlDots()
        {

            GL.PointSize(3);
            GL.Begin(PrimitiveType.Points);
            GL.Vertex2(bezierCurveCubic.StartAnchor);
            GL.Vertex2(bezierCurveCubic.FirstControlPoint);
            GL.Vertex2(bezierCurveCubic.SecondControlPoint);
            GL.Vertex2(bezierCurveCubic.EndAnchor);
            GL.End();

        }

        private void DrawLines()
        {
            GL.LineStipple(1, 0X00FF);
            GL.Enable(EnableCap.LineStipple);

            GL.Begin(PrimitiveType.LineStrip);
            GL.Vertex2(bezierCurveCubic.StartAnchor);
            GL.Vertex2(bezierCurveCubic.FirstControlPoint);
            GL.Vertex2(bezierCurveCubic.SecondControlPoint);
            GL.Vertex2(bezierCurveCubic.EndAnchor);
            GL.End();

            GL.Disable(EnableCap.LineStipple);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //рисовать отдельные блоки в разных методах
            GL.Color4(Color4.Black);

            DrawBezier(0.0001f);

            DrawLines();

            DrawControlDots();

            SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            //SwapBuffers();
            base.OnUpdateFrame(args);
            this.OnRefresh();
        }

        protected override void OnUnload()
        {
            base.OnUnload();
        }
    }
}
