using System.Numerics;
using VoyagerEngine.Framework;
using VoyagerEngine.Services;

namespace VoyagerEngine.Components
{
    public class Position2DComponent : IComponent
    {

        internal UpdateFlags UpdateFlag = UpdateFlags.None;

        private Vector2 _position;
        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                UpdateFlag |= UpdateFlags.Position;
            }
        }
    }
}
