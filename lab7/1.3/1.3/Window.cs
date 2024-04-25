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

namespace _1._3
{
    internal class Window : GameWindow
    {

        private int shaderProgram;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            //GL.Enable(EnableCap.DepthTest);

            var matrix = Matrix4.LookAt(
                0f, 0f, 2f,
                0f, 1f, 0f,
                0f, 1f, 0f);

            GL.LoadMatrix(ref matrix);

            // Создание и компиляция шейдеров
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, @"
            #version 330 core
            layout (location = 0) in vec3 position;
            attribute  float x;
            uniform mat4 modelViewMatrix; 
            uniform mat4 projectionMatrix; 
            void main()
            {
                float R = (1 + sin(x)) * (1 + 0.9 * cos(8 * x)) * (1 + 0.1 * cos(24 * x)) * (0.5 + 0.05 * cos(140 * x));
                float x_prime = R * cos(x);
                float y_prime = R * sin(x);
                float z_prime = position.z;
                gl_Position = projectionMatrix * modelViewMatrix * vec4(x_prime, y_prime, z_prime, 1.0);
            }");

            GL.CompileShader(vertexShader);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, @"
            #version 330 core
            out vec4 FragColor;
            void main()
            {
                FragColor = vec4(1.0, 0.0, 0.0, 1.0);
            }");
            GL.CompileShader(fragmentShader);
            GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int status);
            Console.WriteLine(status.ToString());
            // Создание программы шейдеров
            shaderProgram = GL.CreateProgram();
            GL.AttachShader(shaderProgram, vertexShader);
            GL.AttachShader(shaderProgram, fragmentShader);
            GL.LinkProgram(shaderProgram);

            // Удаление ненужных шейдеров
            GL.DetachShader(shaderProgram, vertexShader);
            GL.DetachShader(shaderProgram, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            // Использование программы шейдеров
            GL.UseProgram(shaderProgram);

            GL.GetProgram(shaderProgram, GetProgramParameterName.LinkStatus, out status);
            Console.WriteLine(status);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.UseProgram(shaderProgram);


            int modelViewMatrixLocation = GL.GetUniformLocation(shaderProgram, "modelViewMatrix");
            int projectionMatrixLocation = GL.GetUniformLocation(shaderProgram, "projectionMatrix");

            Matrix4 modelViewMatrix;
            GL.GetFloat(GetPName.ModelviewMatrix, out modelViewMatrix);
            Matrix4 projectionMatrix;
            GL.GetFloat(GetPName.ProjectionMatrix, out projectionMatrix);

            // передача значений матриц в шейдеры
            GL.UniformMatrix4(modelViewMatrixLocation, false, ref modelViewMatrix);
            GL.UniformMatrix4(projectionMatrixLocation, false, ref projectionMatrix);

            GL.GetError();
            // Рисование канаболы
            int xLocation = GL.GetAttribLocation(shaderProgram, "x");
            GL.GetError();
            GL.Begin(PrimitiveType.LineLoop);
            for (float i = 0; i < 2 * MathF.PI; i += MathF.PI / 1000) 
            {
                GL.VertexAttrib1(xLocation, i);
                GL.GetError();

                GL.Vertex3(0f, 0f, 0f);
            }
            GL.GetError();
            GL.End();

            GL.GetError();

            SwapBuffers();
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
                frustumSize * 0.5, frustumSize * 50 // znear, zfar
                );
        }
    }
}
