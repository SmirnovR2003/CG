using OpenTK;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using OpenTK.Compute.OpenCL;
using System.Timers;

namespace _4._3
{
    internal class Window : GameWindow
    {

        private bool leftButtonPressed = false;
        private float mouseX = 0;
        private float mouseY = 0;
        private int shaderProgram;
        private float progress = 0.0f;
        private int direction = 1;
        private float speed = 0.07f;

        System.Timers.Timer timer = new();


        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            progress += direction * speed;
            if (progress >= 1.0f)
                direction = -1;
            else if (progress <= 0.0f)
                direction = 1;
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            timer.Start();

            GL.ClearColor(0.5f, 0.5f, 0.5f, 1);

            var matrix = Matrix4.LookAt(
                0f, 0f, 1f,
                0f, 0f, 0f,
                0f, 1f, 0f);

            GL.LoadMatrix(ref matrix);

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, @"
                layout(location = 0) in vec3 position;

                void main()
                {
                    gl_Position = gl_ModelViewProjectionMatrix * vec4(position, 1.0);
                    //gl_Position = gl_ModelViewProjectionMatrix * vec4(0.0, 0.0, 0.0, 1.0);
                }"
            );

            GL.CompileShader(vertexShader);
            GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int status);
            Console.WriteLine(status.ToString());
            Console.WriteLine(GL.GetShaderInfoLog(vertexShader));

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, @"
                #version 330 core

                uniform vec2 resolution;
                uniform float zoom;
                uniform vec2 offset;

                out vec4 FragColor;

                void main()
                {
                    vec2 C = (gl_FragCoord.xy / resolution.xy ) * zoom + offset;
                    vec2 Z = vec2(0.0);
                    int iterations = 0;
                    const int maxIterations = 400;

                    while (iterations < maxIterations)
                    {
                         float x = Z.x * Z.x - Z.y * Z.y + C.x;
                         float y = 2.0 * Z.x * Z.y + C.y;

                        //if (x * x + y * y > 1000.0)
                           // break;

                        Z.x = x;
                        Z.y = y;

                        ++iterations;
                    }

                    float t = float(iterations) / float(maxIterations);

                    //vec3 color = vec3(0.5 + 0.5 * cos(3.0 + t * 10.0), 0.5 + 0.5 * sin(5.0 + t * 7.0), 0.5 + 0.5 * cos(7.0 + t * 5.0));
                    vec3 color = vec3(t, t * 0.5, 1.0 - t);

                    //if(t >= 0.2)
                    //{
                    //    color = vec3(1,0,0);
                    //}
                    //else
                    //{
                    //    color = vec3(0,1,0);
                    //}
                    FragColor = vec4(color, 1.0);
                }"
            );
            GL.CompileShader(fragmentShader);
            GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out status);
            Console.WriteLine(status.ToString());
            Console.WriteLine(GL.GetShaderInfoLog(fragmentShader));

            // Создание программы шейдеров
            shaderProgram = GL.CreateProgram();
            GL.AttachShader(shaderProgram, vertexShader);
            GL.AttachShader(shaderProgram, fragmentShader);
            GL.LinkProgram(shaderProgram);

            // Удаление ненужных шейдеров
            GL.DetachShader(shaderProgram, vertexShader);
            GL.DetachShader(shaderProgram, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);

            GL.GetProgram(shaderProgram, GetProgramParameterName.LinkStatus, out status);
            Console.WriteLine(status);

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.UseProgram(shaderProgram);
            GL.Color3(Color.Red);

            GL.Uniform1(GL.GetUniformLocation(shaderProgram, "zoom"), 0.001f);
            GL.Uniform2(GL.GetUniformLocation(shaderProgram, "resolution"), Size.X, Size.Y);
            GL.Uniform2(GL.GetUniformLocation(shaderProgram, "offset"), 0f, 0f);


            GL.Begin(PrimitiveType.Quads);

            GL.Normal3(0f, 0f, 1f);
            GL.Vertex3(-1f, -1f, 0.0f);
            GL.Vertex3(1f, -1f, 0.0f);
            GL.Vertex3(1f, 1f, 0.0f);
            GL.Vertex3(-1f, 1f, 0.0f);

            GL.End();


            SwapBuffers();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Left)
            {
                leftButtonPressed = true;

                mouseX = MousePosition.X;
                mouseY = MousePosition.Y;
            }


            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            if (!leftButtonPressed) return;
            // Вычисляем смещение курсора мыши
            float dx = e.X - mouseX;
            float dy = e.Y - mouseY;

            // Вычисляем угол поворота вокруг осей Y и X как линейно зависящие
            // от смещения мыши по осям X и Y
            float rotateX = dy * 180 / 500;
            float rotateY = dx * 180 / 500;
            RotateCamera(rotateX, rotateY);

            // Сохраняем текущие координаты мыши
            mouseX = e.X;
            mouseY = e.Y;

            base.OnMouseMove(e);

            OnRenderFrame(new FrameEventArgs());
        }

        private void RotateCamera(float x, float y)
        {
            GL.MatrixMode(MatrixMode.Modelview);

            GL.GetFloat(GetPName.ModelviewMatrix, out Matrix4 modelView);

            Vector3 xAxis = new(modelView[0, 0], modelView[1, 0], modelView[2, 0]);
            Vector3 yAxis = new(modelView[0, 1], modelView[1, 1], modelView[2, 1]);

            GL.Rotate(x, xAxis);
            GL.Rotate(y, yAxis);
            NormalizeModelViewMatrix();
        }

        private void NormalizeModelViewMatrix()
        {
            GL.GetFloat(GetPName.ModelviewMatrix, out Matrix4 modelView);

            Vector3 xAxis = new(modelView[0, 0], modelView[1, 0], modelView[2, 0]);
            xAxis.Normalize();
            Vector3 yAxis = new(modelView[0, 1], modelView[1, 1], modelView[2, 1]);
            yAxis.Normalize();

            Vector3 zAxis = Vector3.Cross(xAxis, yAxis);
            zAxis.Normalize();
            xAxis = Vector3.Cross(yAxis, zAxis);
            xAxis.Normalize();
            yAxis = Vector3.Cross(zAxis, xAxis);
            yAxis.Normalize();

            modelView[0, 0] = xAxis.X; modelView[1, 0] = xAxis.Y; modelView[2, 0] = xAxis.Z;
            modelView[0, 1] = yAxis.X; modelView[1, 1] = yAxis.Y; modelView[2, 1] = yAxis.Z;
            modelView[0, 2] = zAxis.X; modelView[1, 2] = zAxis.Y; modelView[2, 2] = zAxis.Z;

            GL.LoadMatrix(ref modelView);
        }


        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            leftButtonPressed = false;
            base.OnMouseUp(e);
            OnRenderFrame(new FrameEventArgs());
        }

        protected override void OnMouseLeave()
        {
            leftButtonPressed = false;
            base.OnMouseLeave();
            OnRenderFrame(new FrameEventArgs());
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            // Освобождение ресурсов
            GL.DeleteProgram(shaderProgram);
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
            double frustumSize = 2;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            double aspectRatio = ((double)width) / ((double)height);
            double frustumHeight = frustumSize;
            double frustumWidth = frustumHeight * aspectRatio;
            if (frustumWidth < frustumSize && (aspectRatio != 0))
            {
                frustumWidth = frustumSize;
                frustumHeight = frustumWidth / aspectRatio;
            }
            GL.Frustum(
                -frustumWidth * 0.5, frustumWidth * 0.5, // left, right
                -frustumHeight * 0.5, frustumHeight * 0.5, // top, bottom
                frustumSize * 0.5, frustumSize * 200 // znear, zfar
                );
        }
    }
}
