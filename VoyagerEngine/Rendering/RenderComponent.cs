using System.Drawing;
using System.Numerics;
using VoyagerEngine.Framework;
using VoyagerEngine.Utilities;
using VoyagerEngine.Attributes;

namespace VoyagerEngine.Rendering
{
    [IncludeComponent(typeof(InitializeRendererComponent))]
    public class RenderComponent : IComponent
    {
        public string ShaderName = Shader.DefaultSpriteShaderName;
        internal uint Program;
        internal uint VAO;
        internal uint VBO;

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
        internal Vector4 NormalizedColor { get; private set; }
        public Color Color
        {
            set
            {
                NormalizedColor = value.NormalizeToVector4();
                UpdateFlag |= UpdateFlags.Texture;
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
                //Verts[2] = value.X;
                //Verts[4] = value.X;
                //Verts[5] = value.Y;
                //Verts[7] = value.Y;
                UpdateFlag |= UpdateFlags.Verts;
            }
        }

        internal float[] Verts { get; private set; } =
        {
            0.0f, 0.0f, // bottom left
            1.0f, 0.0f, // bottom right
            1.0f, 1.0f, // top right
            0.0f, 1.0f, // top left
            1.0f, 0.5f, 0.5f, 1.0f, // bottom left color
            1.0f, 0.5f, 0.5f, 1.0f, // bottom right color
            1.0f, 0.5f, 0.5f, 1.0f, // top right color
            1.0f, 0.5f, 0.5f, 1.0f // top left color
        };

        internal UpdateFlags UpdateFlag = UpdateFlags.None;
    }
}
