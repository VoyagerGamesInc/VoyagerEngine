using System.Numerics;
using VoyagerEngine.Framework;
using VoyagerEngine.Services;

namespace VoyagerEngine.Components
{
    public class Viewport2DComponent : IComponent
    {
        public UpdateFlags UpdateFlag { get; set; }
        private Vector2 size;
        public Vector2 Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
                UpdateFlag |= UpdateFlags.Viewport;
            }
        }
    }
}
