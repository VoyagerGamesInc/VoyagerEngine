using System.Drawing;
using System.Numerics;
using VoyagerEngine.Rendering;
using VoyagerEngine.Services;
using VoyagerEngine.Utilities;

namespace VoyagerEngine.Data
{
    public class Render2DData : IRenderData
    {
        uint IRenderData.Program { get; set; }
        uint IRenderData.VAO { get; set; }
        uint IRenderData.VBO { get; set; }
        uint IRenderData.EBO { get; set; }
        float[] IRenderData.Buffer => Buffer;

        public Color Color
        {
            set
            {
                for(int i = 6; i < Buffer.Length; i += 6)
                {
                    Buffer[i] = value.R;
                    Buffer[i+1] = value.G;
                    Buffer[i+2] = value.B;
                    Buffer[i+3] = value.A;
                }
            }
        }
        public Vector2 Size
        {
            set
            {

            }
        }
        public Vector2 Position
        {
            set
            {

            }
        }

        internal float[] Buffer { get; private set; } =
        {
            // pos
            0.0f, 0.0f,
            // size
            1.0f, 1.0f,
            // bottom left
            0.0f, 0.0f, //pos
            1.0f, 0.5f, 0.5f, 1.0f, // color
            // bottom right
            1.0f, 0.0f, //pos
            1.0f, 0.5f, 0.5f, 1.0f, // color
            // top right
            1.0f, 1.0f, //pos
            1.0f, 0.5f, 0.5f, 1.0f, // color
            // top left
            0.0f, 1.0f, //pos
            1.0f, 0.5f, 0.5f, 1.0f // color
        };
        
    }
}
