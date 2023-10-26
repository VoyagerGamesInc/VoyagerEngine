using System.Drawing;
using System.Numerics;
using VoyagerEngine.Framework;
using VoyagerEngine.Utilities;
using VoyagerEngine.Attributes;
using VoyagerEngine.Rendering;
using VoyagerEngine.Services;

namespace VoyagerEngine.Components
{
    [IncludeComponent(typeof(InitializeRendererComponent))]
    public class Render2DComponent : IComponent
    {
        public string ShaderName = Shader.DefaultSpriteShaderName;
        internal uint Program;
        internal uint VAO;
        internal uint VBO;
        internal Vector4 NormalizedColor { get; private set; }

        internal UpdateFlags UpdateFlag = UpdateFlags.None;
        public Color Color
        {
            set
            {
                NormalizedColor = value.NormalizeToVector4();
                Verts[8] = Verts[12] = Verts[16] = Verts[20] = NormalizedColor.X;
                Verts[9] = Verts[13] = Verts[17] = Verts[21] = NormalizedColor.Y;
                Verts[10] = Verts[14] = Verts[18] = Verts[22] = NormalizedColor.Z;
                Verts[11] = Verts[15] = Verts[19] = Verts[23] = NormalizedColor.W;
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
                Verts[2] = value.X;
                Verts[4] = value.X;
                Verts[5] = value.Y;
                Verts[7] = value.Y;
                UpdateFlag |= UpdateFlags.Verts;
            }
        }

        internal float[] Verts { get; private set; } =
        {
            0.0f, 0.0f, // bottom left - 0,1
            1.0f, 0.0f, // bottom right - 2,3
            1.0f, 1.0f, // top right - 4,5
            0.0f, 1.0f, // top left - 6,7
            1.0f, 0.5f, 0.5f, 1.0f, // bottom left color 8
            1.0f, 0.5f, 0.5f, 1.0f, // bottom right color 12
            1.0f, 0.5f, 0.5f, 1.0f, // top right color 16
            1.0f, 0.5f, 0.5f, 1.0f // top left color 20
        };
    }
}
