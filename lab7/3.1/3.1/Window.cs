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

namespace _3._1
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
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 50;
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

            //GL.ClearColor(0.5f, 0.5f, 0.5f, 1);
            GL.ClearColor(0.1f, 0.1f, 0.1f, 1);

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Lighting);

            GL.Light(LightName.Light2, LightParameter.Position, new Vector4(1f, 1f, 1f, 0f));
            GL.Light(LightName.Light2, LightParameter.Diffuse, new Vector4(1f, 1f, 1f, 1f));
            GL.Light(LightName.Light2, LightParameter.Ambient, new Vector4(0.2f, 0.2f, 0.2f, 1f));
            GL.Light(LightName.Light2, LightParameter.Specular, new Vector4(1f, 1f, 1f, 1f));
            GL.Enable(EnableCap.Light2); 

            GL.Enable(EnableCap.ColorMaterial);

            GL.Material(MaterialFace.Front, MaterialParameter.Diffuse, new Vector4(0.8f, 0.8f, 0f, 1f));
            GL.Material(MaterialFace.Front, MaterialParameter.Ambient, new Vector4(0.2f, 0.2f, 0.2f, 1));
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, new Vector4(0.7f, 0.7f, 0.7f, 1));
            GL.Material(MaterialFace.Front, MaterialParameter.Shininess, 100);

            var matrix = Matrix4.LookAt(
                0f, 50f, 200f,
                0f, 0f, 0f,
                0f, 1f, 0f);

            GL.LoadMatrix(ref matrix);

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, @"
                layout(location = 0) in vec3 position;
                out vec3 FragPos;
                out vec3 Normal;
                uniform float progress;
                uniform mat3 normalMatrix;
                uniform mat4 modelMatrix;
                uniform float step;

                void main()
                {
                    vec2 pos = position.xy;
                    vec3 start_position = vec3(pos.x, pos.y, (pos.x * pos.x + pos.y * pos.y)/100);
                    vec3 end_position = vec3(pos.x, pos.y, (pos.x * pos.x - pos.y * pos.y)/100);
                    vec3 morphed_position = mix(start_position, end_position, progress);

                    vec2 pos_dx = vec2(position.x + step, position.y);
                    vec3 start_position_dx = vec3(pos_dx.x, pos_dx.y, (pos_dx.x * pos_dx.x + pos_dx.y * pos_dx.y)/100);
                    vec3 end_position_dx = vec3(pos_dx.x, pos_dx.y, (pos_dx.x * pos_dx.x - pos_dx.y * pos_dx.y)/100);
                    vec3 morphed_position_dx = mix(start_position_dx, end_position_dx, progress);

                    vec2 pos_dy = vec2(position.x, position.y + step);
                    vec3 start_position_dy = vec3(pos_dy.x, pos_dy.y, (pos_dy.x * pos_dy.x + pos_dy.y * pos_dy.y)/100);
                    vec3 end_position_dy = vec3(pos_dy.x, pos_dy.y, (pos_dy.x * pos_dy.x - pos_dy.y * pos_dy.y)/100);
                    vec3 morphed_position_dy = mix(start_position_dy, end_position_dy, progress);

                    vec3 dFdx = morphed_position_dx - morphed_position;
                    vec3 dFdy = morphed_position_dy - morphed_position;
                    vec3 intermediateNormal = normalize(cross(dFdx, dFdy));

                    FragPos = vec3(modelMatrix * vec4(morphed_position, 1.0));
                    gl_Position = gl_ModelViewProjectionMatrix  * vec4(morphed_position, 1.0);
                    Normal = vec3(normalMatrix  * intermediateNormal);
                }"
            );

            GL.CompileShader(vertexShader);
            GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int status);
            Console.WriteLine(status.ToString());
            Console.WriteLine(GL.GetShaderInfoLog(vertexShader));

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, @"
            out vec4 FragColor;
            in vec3 FragPos;
            in vec3 Normal;
            uniform mat4 modelMatrix;
            void main()
            {
                vec3 lightPos = vec3(modelMatrix * vec4(000.0, 2000.0, 000.0, 0.0)); 
                //vec3 lightPos = vec3(000.0, 1000.0, 000.0); 
                vec3 lightDirection = normalize(lightPos - FragPos); 
                vec3 normal = (gl_FrontFacing ? 1.0 : -1.0) * normalize(Normal); 

                float diffuse = max(0.0, dot(normal, lightDirection)); 
                float diffuseAbs = abs(diffuse); 
                vec3 lightColor = vec3(1.0, 1.0, 1.0); 
                vec3 objectColor = vec3(1.0, 0.0, 0.0); 

                vec3 ambient = 0.5 * objectColor;

                vec3 viewDir = normalize(-FragPos);
                vec3 reflectDir = reflect(-lightDirection, normal);
                float spec = pow(max(0.0, dot(viewDir, reflectDir)), 32);
                vec3 specular = lightColor * spec;

                vec3 finalColor = (ambient + diffuseAbs * objectColor) * lightColor + specular;

                FragColor = vec4(finalColor, 1.0); 
            }");
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
            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.UseProgram(shaderProgram);

            GL.GetFloat(GetPName.ModelviewMatrix, out Matrix4 modelMatrix);
            Matrix3 normalMatrix = new Matrix3(modelMatrix).Inverted();
            normalMatrix.Transpose();

            float step = 1;

            GL.UniformMatrix3(GL.GetUniformLocation(shaderProgram, "normalMatrix"), false, ref normalMatrix);
            GL.UniformMatrix4(GL.GetUniformLocation(shaderProgram, "modelMatrix"), false, ref modelMatrix);
            GL.Uniform1(GL.GetUniformLocation(shaderProgram, "progress"), progress);
            GL.Uniform1(GL.GetUniformLocation(shaderProgram, "step"), step);


            GL.Begin(PrimitiveType.Quads);
            for(float i = -50; i < 50; i += step)
            {
                for (float k = -50; k < 50; k += step)
                {
                    GL.Normal3(0f, 0f, 1f);
                    GL.Vertex3(i, k, 0.0f);
                    GL.Vertex3(i + step, k, 0.0f);
                    GL.Vertex3(i + step, k + step, 0.0f);
                    GL.Vertex3(i, k + step, 0.0f);
                }
            }

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
                frustumSize * 0.5, frustumSize * 500 // znear, zfar
                );
        }
    }
}
