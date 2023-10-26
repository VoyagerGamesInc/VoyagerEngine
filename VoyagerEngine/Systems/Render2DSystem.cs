using System.Drawing;
using System.Numerics;
using VoyagerEngine.Attributes;
using VoyagerEngine.Components;
using VoyagerEngine.Framework;
using VoyagerEngine.Rendering;
using VoyagerEngine.Services;

namespace VoyagerEngine.Systems
{
    [RequiresService(typeof(RenderService))]
    public class Render2DSystem : IRenderSystem
    {
        private delegate void OnUniformUpdate(IUniformUpdateArgs args);
        private RenderService renderService;
        public Render2DSystem()
        {
            renderService = Engine.GetService<RenderService>();
            renderService.SetClearColor(Color.CornflowerBlue);

            renderService.RegisterShader(Shader.DefaultSpriteShader());

            Entity viewportEntity = EntityRegistry.CreateEntity();
            Viewport2DComponent viewportComponent = viewportEntity.AddComponent<Viewport2DComponent>();
            viewportComponent.Size = FromVector2D(Engine.GetWindow().Size);
            UpdateViewport(viewportEntity, viewportComponent);

            Entity cameraEntity = EntityRegistry.CreateEntity();
            CameraComponent cameraComponent = cameraEntity.AddComponent<CameraComponent>();
        }

        public void Render(in EntityRegistry registry)
        {
            renderService.Clear();
            registry.View<Viewport2DComponent>(UpdateViewport);
            registry.View<CameraComponent>(UpdateCamera);
            registry.View<Render2DComponent, InitializeRendererComponent>(InitializeRenderer);
            registry.View<Render2DComponent, Position2DComponent>(UpdatePosition);
            registry.View<Render2DComponent>(Render);
        }
        private void InitializeRenderer(Entity entity, Render2DComponent renderComponent, InitializeRendererComponent initializeFlag)
        {
            renderService.GenerateVertexBuffer(renderComponent.Verts, out renderComponent.VAO, out renderComponent.VBO);
            renderService.GetProgram(renderComponent.ShaderName, out renderComponent.Program);
            //UpdateComponentVariables(renderComponent, renderService);
            entity.RemoveComponent<InitializeRendererComponent>();
        }
        public void UpdatePosition(Entity entity, Render2DComponent renderComponent, Position2DComponent position2DComponent)
        {
            if (position2DComponent.UpdateFlag.HasFlag(UpdateFlags.Position))
            {
                renderService.SeVertexAttrib("position", renderComponent.Program, position2DComponent.Position);
            }
        }
        public void Render(Entity entity, Render2DComponent renderComponent)
        {
            UpdateComponentVariables(renderComponent, renderService);
            renderService.DrawQuad(renderComponent.Program, renderComponent.VAO);
        }

        private void UpdateViewport(Entity entity, Viewport2DComponent viewportComponent)
        {
            if (viewportComponent.UpdateFlag.HasFlag(UpdateFlags.Viewport))
            {
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
        private static void UpdateComponentVariables(Render2DComponent renderComponent, RenderService renderService)
        {
            if (renderComponent.UpdateFlag.HasFlag(UpdateFlags.Verts))
            {
                renderService.UpdateVertexBuffer(renderComponent.Verts, renderComponent.VBO);
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


