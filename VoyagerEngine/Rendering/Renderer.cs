using System.Drawing;
using System.Numerics;
using Silk.NET.Maths;
using Silk.NET.OpenGLES;
using Silk.NET.Windowing;
using VoyagerEngine.Utilities;

namespace VoyagerEngine.Rendering
{
    internal class Renderer
    {
        internal const string SpriteShader = "Sprite";
        internal struct RenderParams
        {
            internal Vector4 cameraParams; //x,y - position, z,w
            internal Vector4 viewportParams; //x,y - size
        }
        internal static Renderer Instance;

        private readonly IWindow _window;
        private GL _gl;
        private List<RenderTransform> _renderTransforms = new ();
        private RenderParams _renderParams = new RenderParams();

        private Dictionary<string, uint> _shaderMap = new();

        internal Renderer(IWindow window)
        {
            _window = window;
            _renderParams.viewportParams.X = _window.Size.X;
            _renderParams.viewportParams.Y = _window.Size.Y;
            _window.Resize += OnWindowResized;
        }

        private void OnWindowResized(Vector2D<int> size)
        {
            _renderParams.viewportParams.X = size.X;
            _renderParams.viewportParams.Y = size.Y;
        }

        internal unsafe void OnLoad(GL gl)
        {
            Instance = this;
            _gl = gl;
            _gl.ClearColor(Color.CornflowerBlue);

            uint spriteShader = CreateShader(Resources.Read("VoyagerEngine.Rendering.Shaders.vert.glsl"), Resources.Read("VoyagerEngine.Rendering.Shaders.frag.glsl"));
            _shaderMap.Add(SpriteShader, spriteShader);
        }
        internal void AddRenderTransform(RenderTransform renderTransform)
        {
            renderTransform.Init(_gl);
            _renderTransforms.Add(renderTransform);
        }
        internal uint CreateShader(string vert, string frag)
        {
            uint vertexShader = _gl.CreateShader(ShaderType.VertexShader);
            _gl.ShaderSource(vertexShader, vert);
            _gl.CompileShader(vertexShader);
            _gl.GetShader(vertexShader, ShaderParameterName.CompileStatus, out int vertRes);
            if (vertRes != (int)GLEnum.True)
                throw new Exception("Vertex shader failed to compile: " + _gl.GetShaderInfoLog(vertexShader));

            uint fragmentShader = _gl.CreateShader(ShaderType.FragmentShader);
            _gl.ShaderSource(fragmentShader, frag);
            _gl.CompileShader(fragmentShader);
            _gl.GetShader(fragmentShader, ShaderParameterName.CompileStatus, out int fragRes);
            if (fragRes != (int)GLEnum.True)
                throw new Exception("Fragment shader failed to compile: " + _gl.GetShaderInfoLog(fragmentShader));

            uint program = _gl.CreateProgram();
            _gl.AttachShader(program, vertexShader);
            _gl.AttachShader(program, fragmentShader);
            _gl.LinkProgram(program);
            _gl.GetProgram(program, ProgramPropertyARB.LinkStatus, out int progRes);
            if (progRes != (int)GLEnum.True)
                throw new Exception("Program failed to link: " + _gl.GetProgramInfoLog(program));

            _gl.DetachShader(program, vertexShader);
            _gl.DetachShader(program, fragmentShader);
            _gl.DeleteShader(vertexShader);
            _gl.DeleteShader(fragmentShader);

            return program;
        }
        
        internal unsafe void OnRender()
        {
            _gl.Clear(ClearBufferMask.ColorBufferBit);

            foreach (RenderTransform renderTransform in _renderTransforms)
            {
                if (renderTransform != null)
                {
                    SetUniforms(renderTransform.Program);
                    renderTransform.SetVariables(_gl);
                    _gl.BindVertexArray(renderTransform.VAO);
                    _gl.DrawArrays(PrimitiveType.TriangleFan, 0, 4);
                }
            }
        }
        private void SetUniforms(uint program)
        {
            _gl.UseProgram(program);

            int windowLoc = _gl.GetUniformLocation(program, "window");
            if(windowLoc != -1)
                _gl.Uniform4(windowLoc, _renderParams.viewportParams);

            int cameraLoc = _gl.GetUniformLocation(program, "camera");
            if(cameraLoc != -1) 
                _gl.Uniform4(cameraLoc, _renderParams.cameraParams);
        }
    }
}