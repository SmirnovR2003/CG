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

namespace _2._4
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

            GL.Enable(EnableCap.Texture2D);

            GL.ClearColor(Color.Blue);

            //GL.Enable(EnableCap.DepthTest);

            var matrix = Matrix4.LookAt(
                0f, 0f, 2f,
                0f, 0f, 0f,
                0f, 1f, 0f);

            GL.LoadMatrix(ref matrix);

            // Создание и компиляция шейдеров
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            //передавать перемноженную матрицу
            GL.ShaderSource(vertexShader, @"
            layout (location = 0) in vec3 position;
            void main()
            {
                gl_Position = gl_ModelViewProjectionMatrix * vec4(position, 1.0);
                gl_TexCoord[0] = gl_MultiTexCoord0;
            }");

            GL.CompileShader(vertexShader);
            GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int status);
            Console.WriteLine(status.ToString());

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, @"
            out vec4 FragColor;
            uniform float R;
            uniform float r;
            uniform sampler2D texture;
            void main()
            {
                vec2 uv = gl_TexCoord[0].xy;

                vec2 center = vec2(0.5, 0.5);
                float dist = distance(center, uv);

                if (dist >= r && dist <= R) 
                {
                    FragColor = vec4(0.0, 0.0, 0.0, 1.0);
                } 
                else 
                {
                   FragColor = vec4(0.0, 1.0, 1.0, 1.0);
                } 
            }");
            GL.CompileShader(fragmentShader);
            GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out status);
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

            float R = 0.5f;
            float r = 0.2f;

            GL.Uniform1(GL.GetUniformLocation(shaderProgram, "R"), R);
            GL.Uniform1(GL.GetUniformLocation(shaderProgram, "r"), r);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.UseProgram(shaderProgram);

            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0, 0);
            GL.Vertex3(-1f, -1f, 0);

            GL.TexCoord2(1, 0);
            GL.Vertex3(1f, -1f, 0);

            GL.TexCoord2(1, 1);
            GL.Vertex3(1f, 1f, 0);

            GL.TexCoord2(0, 1);
            GL.Vertex3(-1f, 1f, 0);

            GL.End();


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
