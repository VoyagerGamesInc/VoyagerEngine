using System.Drawing;
using System.Numerics;
using VoyagerEngine.Attributes;
using VoyagerEngine.Framework;

namespace VoyagerEngine.Rendering
{
    [RequiresService(typeof(RenderService))]
    public class RenderingSystem : IRenderSystem
    {
        private RenderService renderService;
        public RenderingSystem()
        {
            renderService = Engine.GetService<RenderService>();
            renderService.SetClearColor(Color.CornflowerBlue);

            renderService.RegisterShader(Shader.DefaultSpriteShader());

            Entity viewportEntity = EntityRegistry.CreateEntity();
            ViewportComponent viewportComponent = viewportEntity.AddComponent<ViewportComponent>();
            viewportComponent.UpdateFlag |= UpdateFlags.Viewport;
            //UpdateViewport(viewportEntity, viewportComponent);

            Entity cameraEntity = EntityRegistry.CreateEntity();
            CameraComponent cameraComponent = cameraEntity.AddComponent<CameraComponent>();
        }

        public void Render(in EntityRegistry registry)
        {
            renderService.Clear();
            //registry.View<ViewportComponent>(UpdateViewport);
            registry.View<CameraComponent>(UpdateCamera);
            registry.View<RenderComponent,InitializeRendererComponent>(InitializeRenderer);
            registry.View<RenderComponent>(Render);
        }
        private void InitializeRenderer(Entity entity, RenderComponent renderComponent, InitializeRendererComponent initializeFlag)
        {
            renderService.GenerateVertexBuffer(renderComponent.Verts, out renderComponent.VAO, out renderComponent.VBO);
            renderService.GetProgram(renderComponent.ShaderName, out renderComponent.Program);
            //UpdateComponentVariables(renderComponent, renderService);
            entity.RemoveComponent<InitializeRendererComponent>();
        }
        public void Render(Entity entity, RenderComponent renderComponent)
        {
            //UpdateComponentVariables(renderComponent, renderService);
            renderService.DrawQuad(renderComponent.Program, renderComponent.VAO);
        }

        private void UpdateViewport(Entity entity, ViewportComponent viewportComponent)
        {
            if (viewportComponent.UpdateFlag.HasFlag(UpdateFlags.Viewport))
            {
                viewportComponent.Size = FromVector2D(Engine.GetWindow().Size);
                renderService.SetUniforms("viewport", viewportComponent.Size);
            }
            viewportComponent.UpdateFlag = UpdateFlags.None;
        }
        private void UpdateCamera(Entity entity, CameraComponent cameraComponent)
        {
            if (cameraComponent.UpdateFlag.HasFlag(UpdateFlags.Camera))
            {
            }
            cameraComponent.UpdateFlag = UpdateFlags.None;
        }
        private static void UpdateComponentVariables(RenderComponent renderComponent, RenderService renderService)
        {
            if (renderComponent.UpdateFlag.HasFlag(UpdateFlags.Verts))
            {
                renderService.UpdateVertexBuffer(renderComponent.Verts, renderComponent.VBO);
            }
            if (renderComponent.UpdateFlag.HasFlag(UpdateFlags.Position))
            {
                renderService.SeVertexAttrib("position", renderComponent.Program, renderComponent.Position);
            }
            if (renderComponent.UpdateFlag.HasFlag(UpdateFlags.Texture))
            {
                renderService.SetVertexAttrib("color", renderComponent.Program, renderComponent.NormalizedColor);
            }
            renderComponent.UpdateFlag = UpdateFlags.None;
        }
        private static Vector2 FromVector2D(Silk.NET.Maths.Vector2D<int> from)
        {
            return new Vector2(from.X, from.Y);
        }
    }
}


