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
            }
        }
        public Vector2 Size
        {
            set
            {
            }
        }

        internal float[] Buffer { get; private set; } =
        {
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
