using Silk.NET.OpenGLES;
using System;
using System.Drawing;
using System.Numerics;
using VoyagerEngine.Utilities;

namespace VoyagerEngine.Rendering
{
    public class RenderTransform
    {
        internal uint Program { get; private set; }
        internal uint VAO { get; private set; }
        internal uint VBO { get; private set; }
        public Vector2 Position;
        public Color Color
        {
            set
            {
                normalizedColor.FromColor(value);
            }
        }
        public Vector2 Size
        {
            set
            {
                verts[2] = value.X;
                verts[4] = value.X;
                verts[5] = value.Y;
                verts[7] = value.Y;
            }
        }

        private float[] verts =
        {
            0f, 0f, // bottom left
            1f, 0f, // bottom right
            1f, 1f, // top right
            0f, 1f, // top left
        };

        private Vector4 normalizedColor;
        public RenderTransform(uint program)
        {
            Program = program;
            Engine.Instance.Renderer.AddRenderTransform(this);
        }
        internal unsafe void Init(GL gl)
        {
            VAO = gl.GenVertexArray();
            gl.BindVertexArray(VAO);

            VBO = gl.GenBuffer();
            gl.BindBuffer(BufferTargetARB.ArrayBuffer, VBO);
            gl.BufferData(BufferTargetARB.ArrayBuffer, new ReadOnlySpan<float>(verts), BufferUsageARB.StaticDraw);
            gl.EnableVertexAttribArray(0);
            gl.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), (void*)0);
            SetVariables(gl);
        }
        internal void SetVariables(GL gl)
        {
            int positionUniformLoc = gl.GetUniformLocation(Program, "position");
            gl.Uniform2(positionUniformLoc, Position);

            int colorUniformLoc = gl.GetUniformLocation(Program, "color");
            gl.Uniform4(colorUniformLoc, normalizedColor);
        }
    }
}
