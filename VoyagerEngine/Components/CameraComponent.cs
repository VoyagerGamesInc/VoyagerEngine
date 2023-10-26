using System.Numerics;
using VoyagerEngine.Framework;
using VoyagerEngine.Services;

namespace VoyagerEngine.Components
{
    public class CameraComponent : IComponent
    {
        public UpdateFlags UpdateFlag { get; set; }
        private Vector2 position;
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                UpdateFlag |= UpdateFlags.Camera;
            }
        }
        private float scale;
        public float Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
                UpdateFlag |= UpdateFlags.Camera;
            }
        }
    }
}
