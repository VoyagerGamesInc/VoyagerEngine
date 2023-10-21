using System.Numerics;
using VoyagerEngine.Framework;

namespace VoyagerEngine.Rendering
{
    public class ViewportComponent : IComponent
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
                UpdateFlag |= UpdateFlags.Camera;
            }
        }
    }
}
