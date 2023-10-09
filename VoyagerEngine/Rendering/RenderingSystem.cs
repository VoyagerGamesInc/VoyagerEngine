using System.Drawing;
using System.Numerics;
using Silk.NET.Maths;
using Silk.NET.OpenGLES;
using VoyagerEngine.Attributes;
using VoyagerEngine.Core;
using static VoyagerEngine.Rendering.RenderService;

namespace VoyagerEngine.Rendering
{
    [ServiceDependency(typeof(RenderService))]
    internal class RenderingSystem : IRenderSystem
    {
        internal struct RenderParams
        {
            internal Vector4 cameraParams; //x,y - position, z,w
            internal Vector4 viewportParams; //x,y - size
        }

        private RenderParams _renderParams = new RenderParams();

        private RenderService renderService;

        public void Init()
        {
            renderService = Engine.GetService<RenderService>();
            renderService.SetClearColor(Color.CornflowerBlue);

            Shader shader = Shader.DefaultSpriteShader();
            renderService.RegisterShader(ref shader);

            renderService.Window.Resize += OnWindowResized;
            _renderParams.viewportParams.X = renderService.Window.Size.X;
            _renderParams.viewportParams.Y = renderService.Window.Size.Y;
        }
        private void OnWindowResized(Vector2D<int> size)
        {
            _renderParams.viewportParams.X = size.X;
            _renderParams.viewportParams.Y = size.Y;
        }
        static void InitializeRenderComponent(Entity entity, RenderComponent component)
        {
            Debug.Log("Hello 1");
        }
        static void RenderComponent(Entity entity, RenderComponent component)
        {
            Debug.Log("Hello 2");
            //Shader shader = sprite.Shader;
            //_gl.UseProgram(shader.Program);
            //UpdateUniforms(shader.Program);
            //sprite.SetVariables(_gl);
            //_gl.BindVertexArray(sprite.VAO);
            //_gl.DrawArrays(PrimitiveType.TriangleFan, 0, 4);
        }
        public void Render(in EntityRegistry registry)
        {
            registry.View<RenderComponent>(InitializeRenderComponent,typeof(InitializeRendererFlag));
            registry.View<RenderComponent>(RenderComponent);
        }
        private void UpdateUniforms(RenderComponent renderComponent, GL gl)
        {
            int windowLoc = gl.GetUniformLocation(renderComponent.Program, "window");
            if (windowLoc != -1)
                gl.Uniform4(windowLoc, _renderParams.viewportParams);

            int cameraLoc = gl.GetUniformLocation(renderComponent.Program, "camera");
            if (cameraLoc != -1)
                gl.Uniform4(cameraLoc, _renderParams.cameraParams);
        }
        internal void UpdateVariables(RenderComponent renderComponent, GL gl)
        {
            if (renderComponent.UpdateFlags.HasFlag(UpdateFlags.Verts))
            {
                gl.BindBuffer(BufferTargetARB.ArrayBuffer, renderComponent.VBO);
                gl.BufferSubData(BufferTargetARB.ArrayBuffer, 0, new ReadOnlySpan<float>(renderComponent.Verts));
            }
            if (renderComponent.UpdateFlags.HasFlag(UpdateFlags.Position))
            {
                int positionUniformLoc = gl.GetUniformLocation(renderComponent.Program, "position");
                if (positionUniformLoc != -1)
                {
                    gl.Uniform2(positionUniformLoc, renderComponent.Position);
                }
            }

            if (renderComponent.UpdateFlags.HasFlag(UpdateFlags.Color))
            {
                int colorUniformLoc = gl.GetUniformLocation(renderComponent.Program, "color");
                if (colorUniformLoc != -1)
                {
                    gl.Uniform4(colorUniformLoc, renderComponent.NormalizedColor);
                }
            }

            renderComponent.UpdateFlags = UpdateFlags.None;
        }
    }
}