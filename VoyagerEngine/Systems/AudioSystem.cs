using Silk.NET.OpenAL;
using System.Numerics;
using VoyagerEngine.Components;
using VoyagerEngine.Framework;
using VoyagerEngine.Rendering;
using VoyagerEngine.Services;

namespace VoyagerEngine.Systems
{
    public class AudioSystem : ITickingSystem
    {
        private AudioService audioService;
        public AudioSystem()
        {
            audioService = Engine.GetService<AudioService>();

            //Entity musicPlayerEntity = EntityRegistry.CreateEntity();
            //AudioSourceComponent cmp = musicPlayerEntity.AddComponent<AudioSourceComponent>();
            //cmp.Buffer = audioService.GenerateBuffer();
        }

        public void Tick(in EntityRegistry registry)
        {
            registry.View<AudioSourceComponent, InitializeAudioSourceComponent>(InitializeAudioSource);
        }
        private void InitializeAudioSource(Entity entity, AudioSourceComponent cmp, InitializeAudioSourceComponent initializeFlag)
        {
            cmp.Source = audioService.GenerateSource();
            audioService.SetSourceProperty(cmp.Source, SourceFloat.Pitch, cmp.Pitch);
            audioService.SetSourceProperty(cmp.Source, SourceFloat.Gain, cmp.Gain);
            audioService.SeSetSourceProperty(cmp.Source, SourceVector3.Position, new Vector3());
            audioService.Play(cmp.Source, cmp.Buffer);
            entity.RemoveComponent<InitializeAudioSourceComponent>();
        }
    }
}
