using Silk.NET.OpenGLES;
using Silk.NET.Windowing;
using System.Drawing;
using VoyagerEngine.Core;

namespace VoyagerEngine.Rendering
{
    internal class RenderService : IService
    {
        [Flags]
        public enum UpdateFlags
        {
            None = 0,
            Color = 1,
            Position = 2,
            Verts = 4,
            Viewport = 8
        }
        internal IWindow Window { get; private set; }
        internal WindowOptions WindowOptions { get; private set; }
        public GL GL { get; private set; }

        private Dictionary<string, Shader> _shaderMap = new();
        public RenderService(out IWindow window, WindowOptions windowOptions)
        {
            window = Window = Silk.NET.Windowing.Window.Create(windowOptions);
            WindowOptions = windowOptions;
        }
        public void Init()
        {
            GL = Window.CreateOpenGLES();
        }

        internal void SetClearColor(Color color)
        {
            GL.ClearColor(color);
        }
        internal void Clear()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }
        internal void RegisterShader(ref Shader shader)
        {
            uint vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, shader.GetVert());
            GL.CompileShader(vertexShader);
            GL.GetShader(vertexShader, ShaderParameterName.CompileStatus, out int vertRes);
            if (vertRes != (int)GLEnum.True)
                throw new Exception("Vertex shader failed to compile: " + GL.GetShaderInfoLog(vertexShader));

            uint fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, shader.GetFrag());
            GL.CompileShader(fragmentShader);
            GL.GetShader(fragmentShader, ShaderParameterName.CompileStatus, out int fragRes);
            if (fragRes != (int)GLEnum.True)
                throw new Exception("Fragment shader failed to compile: " + GL.GetShaderInfoLog(fragmentShader));

            uint program = GL.CreateProgram();
            GL.AttachShader(program, vertexShader);
            GL.AttachShader(program, fragmentShader);
            GL.LinkProgram(program);
            GL.GetProgram(program, ProgramPropertyARB.LinkStatus, out int progRes);
            if (progRes != (int)GLEnum.True)
                throw new Exception("Program failed to link: " + GL.GetProgramInfoLog(program));

            shader.SetProgram(program);

            _shaderMap.Add(shader.Name, shader);

            GL.DetachShader(program, vertexShader);
            GL.DetachShader(program, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }
    }
}
