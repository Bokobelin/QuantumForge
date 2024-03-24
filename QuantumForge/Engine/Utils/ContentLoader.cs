using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace QuantumForge.Engine.Utils
{
    public static class ContentLoader
    {
        public static Texture2D LoadTexture(string path)
        {
            //Debug.WriteLine($"Entering LoadTextureContent method for path: {path}");
            return LoadTextureContent(path);
        }

        public static string LoadScript(string path)
        {
            return LoadTextContent(path);
        }


        private static string LoadTextContent(string path)
        {
            try
            {
                using (var stream = TitleContainer.OpenStream(path))
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., file not found)
                Debug.WriteLine($"Failed to load text content from '{path}': {ex.Message}");
            }

            return null;
        }

        private static Texture2D LoadTextureContent(string path)
        {
            string resolvedPath = Path.Combine("Content", path);
            try
            {
                using (var stream = TitleContainer.OpenStream(resolvedPath))
                {
                    return Texture2D.FromStream(Constants.graphicsDevice, stream);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., file not found, incorrect type)
                Debug.WriteLine($"Failed to load Texture2D content from '{path}': {ex.Message}");
            }

            return null;
        }
    }
}
