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
using System.Diagnostics;
using System.Numerics;

namespace _5._2
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

            GL.ClearColor(0.0f, 0.0f, 0.0f, 1);

            var matrix = Matrix4.LookAt(
                0f, 0f, 1f,
                0f, 0f, 0f,
                0f, 1f, 0f);

            GL.LoadMatrix(ref matrix);

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, @"
                #version 330 core
                layout(location = 0) in vec3 position;

                void main()
                {
                    gl_Position = vec4(position, 1.0);
                }"
            );

            GL.CompileShader(vertexShader);
            GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int status);
            Console.WriteLine(status.ToString());
            Console.WriteLine(GL.GetShaderInfoLog(vertexShader));

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, @"
                #version 330 core
                out vec4 FragColor;
                void main()
                {
                    FragColor = vec4(1.0, 1.0, 1.0, 1.0); 
                }"
            );

            GL.CompileShader(fragmentShader);
            GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out status);
            Console.WriteLine(status.ToString());
            Console.WriteLine(GL.GetShaderInfoLog(fragmentShader));

            int geometrytShader = GL.CreateShader(ShaderType.GeometryShader);
            GL.ShaderSource(geometrytShader, @"
                #version 330 core

                layout (points) in;
                layout (line_strip, max_vertices = 121) out;
                uniform mat4 modelMatrix;
                uniform mat4 projectionMatrix;
                uniform mat4 ModelViewProjectionMatrix;
                const float PI = 3.14159265359;
                const int NUM_SEGMENTS = 120;

                void main()
                {
                    for (int i = 0; i <= NUM_SEGMENTS + 1; i++) 
                    {
                        float angle = 2.0 * PI * float(i) / float(NUM_SEGMENTS);
                        float x = gl_in[0].gl_Position.x + cos(angle) * 0.1; 
                        float y = gl_in[0].gl_Position.y + sin(angle) * 0.1;
                        //gl_Position = projectionMatrix * modelMatrix * vec4(x, y, 0.0, 1.0);
                        gl_Position = ModelViewProjectionMatrix * vec4(x, y, 0.0, 1.0);
                        EmitVertex();
                    }
                    EndPrimitive();
                }"
            );
            GL.CompileShader(geometrytShader);
            GL.GetShader(geometrytShader, ShaderParameter.CompileStatus, out status);
            Console.WriteLine(status.ToString());
            Console.WriteLine(GL.GetShaderInfoLog(geometrytShader));

            // Создание программы шейдеров
            shaderProgram = GL.CreateProgram();
            GL.AttachShader(shaderProgram, vertexShader);
            GL.AttachShader(shaderProgram, fragmentShader);
            GL.AttachShader(shaderProgram, geometrytShader);
            GL.LinkProgram(shaderProgram);

            // Удаление ненужных шейдеров
            GL.DetachShader(shaderProgram, vertexShader);
            GL.DetachShader(shaderProgram, fragmentShader);
            GL.DetachShader(shaderProgram, geometrytShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(geometrytShader);

            GL.GetProgram(shaderProgram, GetProgramParameterName.LinkStatus, out status);
            Console.WriteLine(status);
            Console.WriteLine(GL.GetProgramInfoLog(shaderProgram));

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.UseProgram(shaderProgram);
            GL.GetFloat(GetPName.ModelviewMatrix, out Matrix4 modelMatrix);
            GL.GetFloat(GetPName.ProjectionMatrix, out Matrix4 projectionMatrix);

            var modelViewProjectionMatrix = modelMatrix* projectionMatrix;

            GL.UniformMatrix4(GL.GetUniformLocation(shaderProgram, "modelMatrix"), false, ref modelMatrix);
            GL.UniformMatrix4(GL.GetUniformLocation(shaderProgram, "projectionMatrix"), false, ref projectionMatrix);
            GL.UniformMatrix4(GL.GetUniformLocation(shaderProgram, "ModelViewProjectionMatrix"), false, ref modelViewProjectionMatrix);

            GL.Uniform1(GL.GetUniformLocation(shaderProgram, "radius"), 0.2);

            GL.Color3(Color.White);

            GL.PointSize(10f);

            GL.Begin(PrimitiveType.Points);
            GL.Vertex3(0f, 0.5f, 0f);
            GL.Vertex3(0f, -0.5f, 0f);
            GL.Vertex3(0.5f, 0.0f, 0f);
            GL.Vertex3(-0.5f, 0.0f, 0f);

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
                frustumSize * 0.5, frustumSize * 200 // znear, zfar
                );
        }
    }
}
