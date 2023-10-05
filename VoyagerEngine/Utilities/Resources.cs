using System.Reflection;

namespace VoyagerEngine.Utilities
{
    internal class Resources
    {
        internal static string Read(string resourcePath)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            {
                if (stream != null)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string content = reader.ReadToEnd();
                        return content;
                    }
                }
                else
                {
                    Log.Write($"Resource not found: \"{resourcePath}\".");
                }
            }
            return "";
        }
    }
}
