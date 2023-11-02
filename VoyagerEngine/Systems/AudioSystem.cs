using Silk.NET.OpenAL;
using System.Numerics;
using VoyagerEngine.Components;
using VoyagerEngine.Data;
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
        }

        public void Tick(in EntityRegistry registry)
        {
            registry.View<InitializeAudioSourceComponent>(InitializeAudioSource);
        }
        private void InitializeAudioSource(Entity entity, InitializeAudioSourceComponent initializeAudioComponent)
        {
            IAudioData audioData = new AudioSourceData();
            audioData.Source = audioService.GenerateSource();
            audioData.Buffer = audioService.GenerateBuffer(initializeAudioComponent.ResourceName);

            audioService.SetSourceProperty(audioData.Source, SourceFloat.Pitch, 1);
            audioService.SetSourceProperty(audioData.Source, SourceFloat.Gain, 1);
            audioService.SeSetSourceProperty(audioData.Source, SourceVector3.Position, new Vector3());
            audioService.Play(audioData.Source, audioData.Buffer);

            AudioSourceComponent audioSourceComponent = entity.AddComponent<AudioSourceComponent>();
            audioSourceComponent.Data = audioData;
            entity.RemoveComponent<InitializeAudioSourceComponent>();
        }
    }
}
