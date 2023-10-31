using System.ComponentModel;
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
        private RenderService renderService;
        private Dictionary<uint, HashSet<Render2DComponent>> sortedComponents = new ();
        private HashSet<IUniformArgs> uniformArgs = new ();
        public Render2DSystem()
        {
            renderService = Engine.GetService<RenderService>();
            renderService.SetClearColor(Color.CornflowerBlue);

            renderService.RegisterShader(Shader.DefaultSpriteShader());

            Entity viewportEntity = EntityRegistry.CreateEntity();
            Viewport2DComponent viewportComponent = viewportEntity.AddComponent<Viewport2DComponent>();
            viewportComponent.Size = FromVector2D(Engine.GetWindow().Size);

            Entity cameraEntity = EntityRegistry.CreateEntity();
            Camera2DComponent cameraComponent = cameraEntity.AddComponent<Camera2DComponent>();
        }

        public void Render(in EntityRegistry registry)
        {
            renderService.Clear();
            registry.View<Viewport2DComponent>(CollectViewportUpdates);
            registry.View<Camera2DComponent>(CollectCameraUpdates);
            registry.View<Render2DComponent, InitializeRender2DComponent>(InitializeRenderer);
            registry.View<Render2DComponent, Position2DComponent>(UpdatePosition);
            Render();
        }
        public void Render()
        {
            foreach (KeyValuePair<uint, HashSet<Render2DComponent>> kv in sortedComponents)
            {
                renderService.UseProgram(kv.Key);
                foreach(IUniformArgs args in uniformArgs)
                {
                    switch (args)
                    {
                        case UniformArgs<Vector2> vector2:
                            renderService.SetUniform(vector2.Name,kv.Key,vector2.Value);
                            break;
                    }
                }
                foreach(Render2DComponent renderComponent in kv.Value)
                {
                    UpdateComponentVariables(renderComponent, renderService);
                    renderService.DrawQuad(renderComponent.VAO);
                }
            }
            uniformArgs.Clear();
        }

        private void InitializeRenderer(Entity entity, Render2DComponent renderComponent, InitializeRender2DComponent initializeFlag)
        {
            renderService.GenerateVertexBuffer(renderComponent.Verts, out renderComponent.VAO, out renderComponent.VBO);
            renderService.GetProgram(renderComponent.ShaderName, out renderComponent.Program);
            if(!sortedComponents.TryGetValue(renderComponent.Program, out var hashSet))
            {
                hashSet = new HashSet<Render2DComponent>();
                sortedComponents.Add(renderComponent.Program, hashSet);
            }
            hashSet.Add(renderComponent);
            entity.RemoveComponent<InitializeRender2DComponent>();
        }
        public void UpdatePosition(Entity entity, Render2DComponent renderComponent, Position2DComponent position2DComponent)
        {
            if (position2DComponent.UpdateFlag.HasFlag(UpdateFlags.Position))
            {
                renderService.SeVertexAttrib("position", renderComponent.Program, position2DComponent.Position);
            }
        }
        private void CollectViewportUpdates(Entity entity, Viewport2DComponent viewportComponent)
        {
            if (viewportComponent.UpdateFlag.HasFlag(UpdateFlags.Viewport))
            {
                uniformArgs.Add(new UniformArgs<Vector2>("viewport",viewportComponent.Size));
            }
            viewportComponent.UpdateFlag = UpdateFlags.None;
        }
        private void CollectCameraUpdates(Entity entity, Camera2DComponent cameraComponent)
        {
            if (cameraComponent.UpdateFlag.HasFlag(UpdateFlags.Camera))
            {
                uniformArgs.Add(new UniformArgs<Vector2>("camera", cameraComponent.Position));
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


