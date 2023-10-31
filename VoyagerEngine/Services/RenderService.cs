using Silk.NET.OpenGL;
using System.Drawing;
using System.Numerics;
using VoyagerEngine.Framework;
using Shader = VoyagerEngine.Rendering.Shader;

namespace VoyagerEngine.Services
{
    [Flags]
    public enum UpdateFlags
    {
        None = 0,
        Texture = 1,
        Position = 2,
        Verts = 4,
        Camera = 8,
        Viewport = 16
    }
    public sealed class RenderService : IService
    {
        private GL gl;
        private Dictionary<string, Shader> shaderMap = new();
        private HashSet<uint> programs = new();

        public RenderService()
        {
            gl = Engine.GetOpenGL();
        }
        internal void SetClearColor(Color color)
        {
            gl.ClearColor(color);
        }
        internal void Clear()
        {
            gl.Clear(ClearBufferMask.ColorBufferBit);
        }
        internal void RegisterShader(Shader shader)
        {
            uint vertexShader = gl.CreateShader(ShaderType.VertexShader);
            gl.ShaderSource(vertexShader, shader.GetVert());
            gl.CompileShader(vertexShader);
            gl.GetShader(vertexShader, ShaderParameterName.CompileStatus, out int vertRes);
            if (vertRes != (int)GLEnum.True)
                throw new Exception("Vertex shader failed to compile: " + gl.GetShaderInfoLog(vertexShader));

            uint fragmentShader = gl.CreateShader(ShaderType.FragmentShader);
            gl.ShaderSource(fragmentShader, shader.GetFrag());
            gl.CompileShader(fragmentShader);
            gl.GetShader(fragmentShader, ShaderParameterName.CompileStatus, out int fragRes);
            if (fragRes != (int)GLEnum.True)
                throw new Exception("Fragment shader failed to compile: " + gl.GetShaderInfoLog(fragmentShader));

            uint program = gl.CreateProgram();

            programs.Add(program);

            gl.AttachShader(program, vertexShader);
            gl.AttachShader(program, fragmentShader);
            gl.LinkProgram(program);
            gl.GetProgram(program, ProgramPropertyARB.LinkStatus, out int progRes);
            if (progRes != (int)GLEnum.True)
                throw new Exception("Program failed to link: " + gl.GetProgramInfoLog(program));

            shader.SetProgram(program);

            shaderMap.Add(shader.Name, shader);

            gl.DetachShader(program, vertexShader);
            gl.DetachShader(program, fragmentShader);
            gl.DeleteShader(vertexShader);
            gl.DeleteShader(fragmentShader);
        }
        public void GetProgram(string shaderName, out uint program)
        {
            program = 0;
            if (shaderMap.TryGetValue(shaderName, out var shader))
            {
                program = shader.Program;
            }
        }
        public void UseProgram(uint program)
        {
            gl.UseProgram(program);
        }
        public void DrawQuad(uint vao)
        {
            gl.BindVertexArray(vao);
            gl.DrawArrays(GLEnum.TriangleFan, 0, 4);
        }
        public unsafe void GenerateVertexBuffer(float[] data, out uint vao, out uint vbo)
        {
            vao = gl.GenVertexArray();
            gl.BindVertexArray(vao);

            vbo = gl.GenBuffer();
            gl.BindBuffer(BufferTargetARB.ArrayBuffer, vbo);
            gl.BufferData(BufferTargetARB.ArrayBuffer, new ReadOnlySpan<float>(data), BufferUsageARB.StaticDraw);

            const uint vertLoc = 0;
            gl.EnableVertexAttribArray(vertLoc);
            gl.VertexAttribPointer(vertLoc, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), (void*)0);

            const uint colorLoc = 1;
            gl.EnableVertexAttribArray(colorLoc);
            gl.VertexAttribPointer(colorLoc, 4, VertexAttribPointerType.Float, false, 4 * sizeof(float), (void*)(8 * sizeof(float)));

            gl.BindVertexArray(0);
            gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
        }
        public void UpdateVertexBuffer(float[] vertex, uint vbo)
        {
            gl.BindBuffer(BufferTargetARB.ArrayBuffer, vbo);
            gl.BufferSubData(BufferTargetARB.ArrayBuffer, 0, new ReadOnlySpan<float>(vertex));
        }
        public void SetUniform(string name, uint program, Vector2 data)
        {
            int loc = gl.GetUniformLocation(program, name);
            if (loc != -1)
                gl.Uniform2(loc, ref data);
        }
        public void SeVertexAttrib(string name, uint program, Vector2 data)
        {
            int loc = gl.GetAttribLocation(program, name);
            if (loc != -1)
                gl.VertexAttrib2((uint)loc, ref data);
        }
        public void SetVertexAttrib(string name, uint program, Vector4 data)
        {
            int loc = gl.GetAttribLocation(program, name);
            if (loc != -1)
                gl.VertexAttrib4((uint)loc, ref data);
        }
        public void GenerateTexture(string resourcePath)
        {
            Image image = Image.FromStream(Engine.LoadResource(resourcePath));
            //gl.BindTexture(GLEnum.Texture2D, textureId);
            //gl.TexImage2D<byte>(
            //    GLEnum.Texture2D, 0, (int)GLEnum.Rgba, (uint)imageResult.Width, (uint)imageResult.Height, 0,
            //    GLEnum.Rgba, GLEnum.UnsignedByte, new ReadOnlySpan<byte>(imageResult.Data));
        }
    }
}
