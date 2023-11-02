using NAudio.Wave;
using Silk.NET.OpenAL;
using System.Numerics;
using VoyagerEngine.Framework;

namespace VoyagerEngine.Services
{
    public sealed class AudioService : IService
    {
        private unsafe class AudioPointers
        {
            internal Device* audioDevice;
            internal Context* deviceContext;
            internal AudioPointers(ALContext alContext)
            {
                audioDevice = alContext.OpenDevice("");
                if (audioDevice == null)
                {
                    Console.WriteLine("Could not create device");
                    return;
                }

                deviceContext = alContext.CreateContext(audioDevice, null);

                alContext.MakeContextCurrent(deviceContext);
            }
            internal void Dispose(ALContext alContext)
            {
                alContext.DestroyContext(deviceContext);
                alContext.CloseDevice(audioDevice);
            }
        }
        AL al;
        ALContext alContext;
        AudioPointers audioPointers;
        HashSet<uint> sources = new HashSet<uint>();
        HashSet<uint> buffers = new HashSet<uint>();
        public AudioService()
        {
            al = Engine.GetOpenAL();
            alContext = Engine.GetALContext();
            audioPointers = new AudioPointers(alContext);

            al.GetError();

            al.SetListenerProperty(ListenerVector3.Position, new Vector3());
            al.SetListenerProperty(ListenerVector3.Velocity, new Vector3());
            al.SetListenerProperty(ListenerFloat.Gain, 0.75f);
        }
        public uint GenerateSource()
        {
            return al.GenSource();
        }
        public uint GenerateBuffer(string resourceName)
        {
            using (var mp3Reader = new Mp3FileReader(Engine.LoadResource(resourceName)))
            {
                uint buffer = al.GenBuffer();
                buffers.Add(buffer);
                byte[] mp3Data = new byte[mp3Reader.Length];
                int length = mp3Reader.Read(mp3Data, 0, mp3Data.Length);
                al.BufferData(buffer, BufferFormat.Stereo16, mp3Data, mp3Reader.Mp3WaveFormat.SampleRate);
                al.GetError();
                return buffer;
            }
        }
        public void SetSourceProperty(uint source, SourceFloat property, float value)
        {
            al.SetSourceProperty(source, property, value);
        }
        public void SeSetSourceProperty(uint source, SourceVector3 property, Vector3 value)
        {
            al.SetSourceProperty(source, property, value);
        }
        public void Play(uint source, uint buffer)
        {
            al.SetSourceProperty(source, SourceInteger.Buffer, buffer);
            al.SourcePlay(source);
        }
        public void Dispose()
        {
            foreach (uint buffer in buffers)
            {
                al.DeleteBuffer(buffer);
            }
            foreach (uint source in sources)
            {
                al.DeleteSource(source);
            }
            audioPointers.Dispose(alContext);
        }
    }
}
