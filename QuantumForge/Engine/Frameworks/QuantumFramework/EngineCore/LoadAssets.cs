using QuantumForge.Engine.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace QuantumForge.Engine.Frameworks.QuantumFramework.EngineCore
{
    public static class LoadAssets
    {
        public static Dictionary<string, string> scriptPaths = new Dictionary<string, string>();
        public static Dictionary<string, string> texturePaths = new Dictionary<string, string>();

        public static void LoadGame()
        {
            string directoryPath = "../../../Content";
            //Debug.WriteLine(Path.GetFullPath(directoryPath));

            // Get all files in the directory
            string[] files = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);

            // Sort files by extension
            var sortedFiles = files.OrderBy(file => Path.GetExtension(file)).ToArray();

                // Store the paths in dictionaries without the "../../../Content/" prefix
                foreach (var file in sortedFiles)
                {
                    string fileName = Path.GetFileName(file);
                    string relativePath = GetRelativePath(directoryPath, file);
                    string extension = Path.GetExtension(file);

                    if (extension == ".png" || extension == ".jpg")
                    {
                        if(texturePaths.ContainsKey(fileName))
                            texturePaths.Remove(fileName);
                        texturePaths.Add(fileName, relativePath); // Add texture path to the dictionary
                    }
                    else if (extension == ".cs")
                    {
                        if(scriptPaths.ContainsKey(fileName))
                            scriptPaths.Remove(fileName);
                        scriptPaths.Add(fileName, relativePath); // Add script path to the dictionary
                        
                    }
                }

            // You can now access the paths in the scriptPaths and texturePaths dictionaries
            /*
            foreach (var scriptPath in scriptPaths.Values)
            {
                Debug.WriteLine($"Script Path: {scriptPath}");
            }

            foreach (var texturePath in texturePaths.Values)
            {
                Debug.WriteLine($"Texture Path: {texturePath}");
            }
            */
            
        }

        // Helper method to get the relative path
        private static string GetRelativePath(string basePath, string fullPath)
        {
            return Path.GetRelativePath(basePath, fullPath);
        }
    }
}
