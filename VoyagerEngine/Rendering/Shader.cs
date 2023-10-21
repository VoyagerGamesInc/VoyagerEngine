using VoyagerEngine.Utilities;

namespace VoyagerEngine.Rendering
{
    public struct Shader
    {
        public const string DefaultSpriteShaderName = "Sprite";
        public string Name { get; private set; }
        public string VertPath { get; private set; }
        public string FragPath { get; private set; }
        public uint Program {  get; private set; }
        public Shader(string name, string vertPath, string fragPath)
        {
            Name = name;
            VertPath = vertPath;
            FragPath = fragPath;
        }
        internal void SetProgram(uint program)
        {
            Program = program;
        }
        internal string GetVert()
        {
            return Resources.Read(VertPath);
        }
        internal string GetFrag()
        {
            return Resources.Read(FragPath);
        }

        internal static Shader DefaultSpriteShader()
        {
            return new Shader
            {
                Name = DefaultSpriteShaderName,
                VertPath = "VoyagerEngine.Rendering.GLSL.vert.glsl",
                FragPath = "VoyagerEngine.Rendering.GLSL.frag.glsl"
            };
        }
    }
}
