using OpenTK;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Drawing;

namespace _2
{
    //вынести кроша в отдельный класс
    internal class Crosh : GameWindow
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

        private BezierCurveCubic leftEar;
        private BezierCurveCubic rightEar;
        private BezierCurveCubic bodyUpperHalf;
        private BezierCurveCubic bodyLowerHalf;
        private BezierCurveCubic leftHand;
        private BezierCurveCubic rightHand;
        private BezierCurveCubic rightLeg1; 
        private BezierCurveCubic rightLeg2;
        private BezierCurveCubic leftLeg1; 
        private BezierCurveCubic leftLeg2;
        private BezierCurveCubic smile;
        private BezierCurveCubic leftTooth;
        private BezierCurveCubic rightTooth;
        private Box2d box;
        private DragAndDrop dragAndDrop;
        Vector3 moveVector;

        public Crosh(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        public static bool NearlyEqual(float a, float b, float epsilon)
        {
            return Math.Abs(a - b) < epsilon;
        }

        //разбить все методы
        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(Color4.White);
            InitCrosh();
            box = new Box2d(-0.4, -0.3, 0.4, 0.8) ;

            dragAndDrop = new DragAndDrop();
            moveVector = new Vector3(0,0,0);
        }

        void FillEllipse(float cx, float cy, float rx, float ry, int num_segments)
        {
            float theta = 2 * 3.1415926f / (float)(num_segments);
            float c = ((float)Math.Cos(theta));
            float s = ((float)Math.Sin(theta));
            float t;
            float x = 1;
            float y = 0;
            GL.Begin(PrimitiveType.TriangleFan);
            GL.Vertex2(cx, cy);
            for (int ii = 0; ii < num_segments; ii++)
            {
                GL.Vertex2(x * rx + cx, y * ry + cy);
                t = x;
                x = c * x - s * y;
                y = s * t + c * y;
            }
            GL.Vertex2(1 * rx + cx, 0 * ry + cy);
            GL.End();
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
            double viewWidth = 2.0;
            double viewHeight = viewWidth;
            if (aspectRatio > 1.0)
            {
                viewWidth = viewHeight * aspectRatio;
            }
            else
            {
                viewHeight = viewWidth / aspectRatio;
            }

            GL.Ortho(-viewWidth * 0.5, viewWidth * 0.5, -viewHeight * 0.5, viewHeight * 0.5, -1.0, 1.0);

        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.PointSize(5);
            GL.LineWidth(2);

            GL.Color4(Color4.Yellow);
            FillEllipse(0, 0, 0.5f, 0.5f, 100);

            GL.Translate(moveVector);
            DrawCrosh();
            DrawBox();
            GL.Translate(-moveVector);

            SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            var mousePos = GetCursorPosInModalSpace(MousePosition.X, MousePosition.Y);

            if (!CheckMousePosTostartDragAndDrop(mousePos))
                return;
            float mousePosX = ((float)mousePos.X);
            float mousePosY = ((float)mousePos.Y);

            dragAndDrop.start = true;

            dragAndDrop.startX = mousePosX;
            dragAndDrop.startY = mousePosY;

            dragAndDrop.lastX = moveVector.X;
            dragAndDrop.lastY = moveVector.Y;

            base.OnMouseDown(e);
        }
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            if (!dragAndDrop.start) return;

            var mousePos = GetCursorPosInModalSpace(MousePosition.X, MousePosition.Y);
            moveVector.X = (float)mousePos.X - dragAndDrop.startX + dragAndDrop.lastX;
            moveVector.Y = (float)mousePos.Y - dragAndDrop.startY + dragAndDrop.lastY;
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

        private bool CheckMousePosTostartDragAndDrop(Vector2d mousePos)
        {
            if (box.Contains(mousePos - moveVector.Xy))
                return true;

            return false;
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

        unsafe private Size GetFramebufferSize()
        {
            Size size = new Size();

            GLFW.GetFramebufferSize(WindowPtr, out int width, out int height);

            size.Width = width;
            size.Height = height;

            return size;
        }

        private void DrawBox()
        {
            GL.Color4(Color4.Black);
            GL.Begin(PrimitiveType.LineLoop);
            GL.Vertex2(box.Min.X, box.Min.Y);
            GL.Vertex2(box.Max.X, box.Min.Y);
            GL.Vertex2(box.Max.X, box.Max.Y);
            GL.Vertex2(box.Min.X, box.Max.Y);
            GL.End();
        }

        private void InitCrosh()
        {
            InitEars();
            InitBody();
            InitHands();
            InitLegs();
            InitSmile();
            InitTeeth();
        }

        private void InitEars()
        {
            leftEar = new BezierCurveCubic(
                new Vector2(-0.099999994f, 0.3f),
                new Vector2(-0f, 0.323333322f),
                new Vector2(-0.43333333f, 0.820000012f),
                new Vector2(-0.07333333f, 0.950000002f));

            rightEar = new BezierCurveCubic(
                new Vector2(0.099999994f, 0.3f),
                new Vector2(0f, 0.323333322f),
                new Vector2(0.43333333f, 0.820000012f),
                new Vector2(0.07333333f, 0.950000002f));
        }

        private void InitBody()
        {
            bodyUpperHalf = new BezierCurveCubic(
                new Vector2(-0.23666666f, 0.1f),
                new Vector2(0.24f, 0.1f),
                new Vector2(-0.23666666f, 0.42333335f),
                new Vector2(0.24f, 0.42333335f));

            bodyLowerHalf = new BezierCurveCubic(
                new Vector2(-0.23666666f, 0.12f),
                new Vector2(0.24f, 0.12f),
                new Vector2(-0.23666666f, -0.232323229f),
                new Vector2(0.24f, -0.232323229f));
        }

        private void InitHands()
        {
            leftHand = new BezierCurveCubic(
                new Vector2(-0.21774407f, 0.06153846f),
                new Vector2(-0.20894632f, 0.017582418f),
                new Vector2(-0.49267343f, -0.03736264f),
                new Vector2(-0.37170452f, -0.17362638f));

            rightHand = new BezierCurveCubic(
                new Vector2(0.21774407f, 0.06153846f),
                new Vector2(0.20894632f, 0.017582418f),
                new Vector2(0.49267343f, -0.03736264f),
                new Vector2(0.37170452f, -0.17362638f));
        }

        private void InitLegs()
        {
            rightLeg1 = new BezierCurveCubic(
                new Vector2(0.06343268f, -0.1750547f),
                new Vector2(0.172799364f, -0.15098469f),
                new Vector2(0.06780735f, -0.133479208f),
                new Vector2(0.113741353f, -0.113785557f));

            rightLeg2 = new BezierCurveCubic(
                new Vector2(0.172799364f, -0.15098469f),
                new Vector2(0.06343268f, -0.1750547f),
                new Vector2(0.313686281f, -0.234f),
                new Vector2(0.0305247344f, -0.259259254f));


            leftLeg1 = new BezierCurveCubic(
                new Vector2(-0.06343268f, -0.1750547f),
                new Vector2(-0.172799364f, -0.15098469f),
                new Vector2(-0.06780735f, -0.133479208f),
                new Vector2(-0.113741353f, -0.113785557f));

            leftLeg2 = new BezierCurveCubic(
                new Vector2(-0.172799364f, -0.15098469f),
                new Vector2(-0.06343268f, -0.1750547f),
                new Vector2(-0.313686281f, -0.234f),
                new Vector2(-0.0305247344f, -0.259259254f));
        }

        private void InitSmile()
        {
            smile = new BezierCurveCubic(
                new Vector2(-0.13f, 0.0133333337f),
                new Vector2(0.126666665f, 0.0166666675f),
                new Vector2(-0.07666667f, -0.05666667f),
                new Vector2(0.086666666f, -0.0533333346f));
        }

        private void InitTeeth()
        {
            leftTooth = new BezierCurveCubic(
                new Vector2(-0.0359640345f, -0.036f),
                new Vector2(-0.00599400559f, -0.038f),
                new Vector2(-0.0459540449f, -0.084f),
                new Vector2(0.00599400559f, -0.084f));

            rightTooth = new BezierCurveCubic(
                new Vector2(0.0359640345f, -0.036f),
                new Vector2(0.00599400559f, -0.038f),
                new Vector2(0.0459540449f, -0.084f),
                new Vector2(0.00599400559f, -0.084f));
        }

        private void DrawCrosh()
        {
            DrawEars();
            DrawBody();
            DrawHands();
            DrawLegs();
            DrawSmile();
            DrawTeeth();
            DrawEyes();
            DrawNose();
        }

        private void DrawEars()
        {
            GL.Color4(Color4.LightBlue);
            GL.Begin(PrimitiveType.TriangleFan);
            for (float i = 0; i < 1; i += 0.0001f)
            {
                GL.Vertex2(leftEar.CalculatePoint(i));

            }
            GL.End();

            GL.Begin(PrimitiveType.TriangleFan);
            for (float i = 0; i < 1; i += 0.0001f)
            {
                GL.Vertex2(rightEar.CalculatePoint(i));

            }
            GL.End();
        }

        private void DrawBody()
        {
            GL.Color4(Color4.LightBlue);
            GL.Begin(PrimitiveType.TriangleFan);
            for (float i = 0; i < 1; i += 0.0001f)
            {
                GL.Vertex2(bodyUpperHalf.CalculatePoint(i));

            }
            for (float i = 0; i < 1; i += 0.0001f)
            {
                GL.Vertex2(bodyLowerHalf.CalculatePoint(i));

            }
            GL.End();
        }

        private void DrawHands()
        {
            GL.Color4(Color4.LightBlue);
            GL.Begin(PrimitiveType.TriangleFan);
            for (float i = 0; i < 1; i += 0.0001f)
            {
                GL.Vertex2(leftHand.CalculatePoint(i));

            }
            GL.End();

            GL.Begin(PrimitiveType.TriangleFan);
            for (float i = 0; i < 1; i += 0.0001f)
            {
                GL.Vertex2(rightHand.CalculatePoint(i));

            }
            GL.End();

        }

        private void DrawLegs()
        {
            GL.Color4(Color4.LightBlue);
            GL.Begin(PrimitiveType.TriangleFan);
            for (float i = 0; i < 1; i += 0.0001f)
            {
                GL.Vertex2(rightLeg1.CalculatePoint(i));

            }
            GL.End();

            GL.Begin(PrimitiveType.TriangleFan);
            for (float i = 0; i < 1; i += 0.0001f)
            {
                GL.Vertex2(rightLeg2.CalculatePoint(i));
            }
            GL.End();

            GL.Begin(PrimitiveType.TriangleFan);
            for (float i = 0; i < 1; i += 0.0001f)
            {
                GL.Vertex2(leftLeg1.CalculatePoint(i));

            }
            GL.End();

            GL.Begin(PrimitiveType.TriangleFan);
            for (float i = 0; i < 1; i += 0.0001f)
            {
                GL.Vertex2(leftLeg2.CalculatePoint(i));
            }
            GL.End();

        }

        private void DrawSmile()
        {
            GL.Color4(Color4.Black);
            GL.Begin(PrimitiveType.LineStrip);
            for (float i = 0; i < 1; i += 0.0001f)
            {
                GL.Vertex2(smile.CalculatePoint(i));
            }
            GL.End();
        }

        private void DrawTeeth()
        {
            GL.Color4(Color4.White);
            GL.Begin(PrimitiveType.TriangleFan);
            for (float i = 0; i < 1; i += 0.0001f)
            {
                GL.Vertex2(leftTooth.CalculatePoint(i));
            }
            GL.End();

            GL.Color4(Color4.Black);
            GL.Begin(PrimitiveType.LineStrip);
            for (float i = 0; i < 1; i += 0.0001f)
            {
                GL.Vertex2(leftTooth.CalculatePoint(i));
            }
            GL.End();

            GL.Color4(Color4.White);
            GL.Begin(PrimitiveType.TriangleFan);
            for (float i = 0; i < 1; i += 0.0001f)
            {
                GL.Vertex2(rightTooth.CalculatePoint(i));
            }
            GL.End();

            GL.Color4(Color4.Black);
            GL.Begin(PrimitiveType.LineStrip);
            for (float i = 0; i < 1; i += 0.0001f)
            {
                GL.Vertex2(rightTooth.CalculatePoint(i));
            }
            GL.End();

        }

        private void DrawEyes()
        {
            //leftEye
            GL.Color4(Color4.White);
            FillEllipse(-0.07f, 0.18f, 0.05f, 0.08f, 1000);
            GL.Color4(Color4.Black);
            FillEllipse(-0.07f, 0.18f, 0.008f, 0.015f, 1000);

            //rightEye
            GL.Color4(Color4.White);
            FillEllipse(0.07f, 0.18f, 0.05f, 0.08f, 1000);
            GL.Color4(Color4.Black);
            FillEllipse(0.07f, 0.18f, 0.008f, 0.015f, 1000);
        }

        private void DrawNose()
        {
            //nose
            GL.Color4(Color4.HotPink);
            FillEllipse(0f, 0.1f, 0.02f, 0.02f, 1000);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            this.OnRefresh();
        }

        protected override void OnUnload()
        {
            base.OnUnload();
        }
    }
}




