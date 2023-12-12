using Silk.NET.OpenGLES;
using System.Drawing;
using System.Numerics;
using VoyagerEngine.Components;
using VoyagerEngine.Data;
using VoyagerEngine.Framework;
using VoyagerEngine.Rendering;
using VoyagerEngine.Services;

namespace VoyagerEngine.Systems
{
    public class Render2DSystem : IRenderSystem
    {
        private RenderService renderService;
        private Dictionary<uint, HashSet<Render2DComponent>> sortedComponents = new ();
        private HashSet<IUniformArgs> uniformArgs = new ();
        public Render2DSystem()
        {
            renderService = Engine.GetService<RenderService>();
            renderService.SetClearColor(Color.CornflowerBlue);

            renderService.RegisterShader(ShaderData.DefaultSpriteShader());

            Entity viewportEntity = EntityRegistry.CreateEntity();
            Viewport2DComponent viewportComponent = viewportEntity.AddComponent<Viewport2DComponent>();
            viewportComponent.Size = FromVector2D(Engine.GetWindow().Size);

            Entity cameraEntity = EntityRegistry.CreateEntity();
            cameraEntity.AddComponent<Camera2DComponent>();
        }

        public void Render(in EntityRegistry registry)
        {
            renderService.Clear();
            registry.View<Viewport2DComponent>(CollectViewportUpdates);
            registry.View<Camera2DComponent>(CollectCameraUpdates);
            registry.View<InitializeRender2DComponent>(InitializeRenderer);
            registry.View<Render2DComponent, Position2DComponent>(UpdatePosition);
            //registry.View<Render2DComponent>(RenderComponent);
            Render();
        }
        public void Render()
        {
            foreach (KeyValuePair<uint, HashSet<Render2DComponent>> kv in sortedComponents)
            {
                renderService.UseProgram(kv.Key);
                foreach (IUniformArgs args in uniformArgs)
                {
                    switch (args)
                    {
                        case UniformArgs<Vector2> vector2:
                            renderService.SetUniform(vector2.Name, kv.Key, vector2.Value);
                            break;
                    }
                }
                foreach (Render2DComponent renderComponent in kv.Value)
                {
                    UpdateComponentVariables(renderComponent, renderService);
                    renderService.DrawQuad(renderComponent.Data.VAO);
                }
            }
            uniformArgs.Clear();
        }
        private void RenderComponent(Entity entity, Render2DComponent renderComponent)
        {
            renderService.UseProgram(renderComponent.Data.Program);
            renderService.DrawQuad(renderComponent.Data.VAO);
        }

        private void InitializeRenderer(Entity entity, InitializeRender2DComponent initializeRenderComponent)
        {
            IRenderData renderData = new Render2DData();

            renderService.GenerateVertexBuffer(renderData.Buffer, BufferUsageARB.DynamicDraw, out uint vao, out uint vbo);
            renderData.VAO = vao;
            renderData.VBO = vbo;
            renderService.GetProgram(initializeRenderComponent.ShaderName, out uint program);
            renderData.Program = program;

            if (!sortedComponents.TryGetValue(program, out var hashSet))
            {
                sortedComponents.Add(program, hashSet = new HashSet<Render2DComponent>());
            }
            Render2DComponent renderComponent = entity.AddComponent<Render2DComponent>();
            renderComponent.Data = renderData;
            hashSet.Add(renderComponent);
            entity.RemoveComponent<InitializeRender2DComponent>();
        }
        public void UpdatePosition(Entity entity, Render2DComponent renderComponent, Position2DComponent position2DComponent)
        {
            if (position2DComponent.UpdateFlag.HasFlag(UpdateFlags.Position))
            {
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
            if (renderComponent.UpdateFlag.HasFlag(UpdateFlags.Buffer))
            {
                renderService.UpdateVertexBuffer(renderComponent.Data.Buffer, renderComponent.Data.VBO);
            }
            renderComponent.UpdateFlag = UpdateFlags.None;
        }

        private static Vector2 FromVector2D(Silk.NET.Maths.Vector2D<int> from)
        {
            return new Vector2(from.X, from.Y);
        }
    }
}


