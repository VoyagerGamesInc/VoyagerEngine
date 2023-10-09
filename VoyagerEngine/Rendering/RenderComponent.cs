using System.Drawing;
using System.Numerics;
using VoyagerEngine.Core;
using VoyagerEngine.Utilities;
using VoyagerEngine.Attributes;
using static VoyagerEngine.Rendering.RenderService;

namespace VoyagerEngine.Rendering
{
    [IncludeFlags(typeof(InitializeRendererFlag))]
    public class RenderComponent : IComponent
    {
        internal uint Program;
        internal uint VAO { get; private set; }
        internal uint VBO { get; private set; }

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
                UpdateFlags |= UpdateFlags.Position;
            }
        }
        internal Vector4 NormalizedColor { get; private set; }
        public Color Color
        {
            set
            {
                NormalizedColor = value.NormalizeToVector4();
                UpdateFlags |= UpdateFlags.Color;
            }
        }
        public Vector2 Size
        {
            get
            {
                return new Vector2(Verts[4], Verts[5]);
            }
            set
            {
                Verts[2] = value.X;
                Verts[4] = value.X;
                Verts[5] = value.Y;
                Verts[7] = value.Y;
                UpdateFlags |= UpdateFlags.Verts;
            }
        }

        public Vector2 Pivot
        {
            set
            {
                Verts[2] = value.X;
                Verts[4] = value.X;
                Verts[5] = value.Y;
                Verts[7] = value.Y;
                UpdateFlags |= UpdateFlags.Verts;
            }
        }

        internal float[] Verts { get; private set; } =
        {
            0f, 0f, // bottom left
            1f, 0f, // bottom right
            1f, 1f, // top right
            0f, 1f, // top left
        };

        internal UpdateFlags UpdateFlags = UpdateFlags.None;
    }
}
