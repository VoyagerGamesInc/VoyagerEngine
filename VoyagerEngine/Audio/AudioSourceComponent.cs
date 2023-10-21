using VoyagerEngine.Attributes;
using VoyagerEngine.Framework;

namespace VoyagerEngine.Audio
{
    [IncludeComponent(typeof(InitializeAudioSourceComponent))]
    public class AudioSourceComponent : IComponent
    {
        public uint Source;
        public uint Buffer;
        public int Gain = 1;
        public int Pitch = 1;
    }
}
