using System;
using System.IO;
using System.Xml.Serialization;

namespace QuantumForge.Engine.Editor.Serialization
{
    public static class ProjectSerializer
    {
        public static void SaveProjectToXml(Project project, string filePath)
        {
            try
            {
                // Create an XmlSerializer for the Project type
                XmlSerializer serializer = new XmlSerializer(typeof(Project));

                // Create a FileStream to write the XML file
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    // Serialize the Project and write it to the file
                    serializer.Serialize(fileStream, project);

                    Logger.LogInfo($"Saved project to path : {Path.GetFullPath(filePath)}");
                }

            }
            /*
            catch (InvalidOperationException ex)
            {
                // Check inner exceptions
#nullable enable
                Exception? innerException = ex.InnerException;
                while (innerException != null)
                {
                    // Log or inspect inner exception details
                    innerException = innerException.InnerException;
#nullable disable
                    if (innerException != null)
                    {
                        throw innerException;
                    }
                }
                // Handle or rethrow the exception as needed
            */
            catch (Exception ex)
            {
                // Handle exceptions as needed
                //Console.WriteLine($"Error saving project to XML: {ex.Message}");
                Logger.LogError($"Error saving project to XML: message : {ex.Message} & innerException : {ex.InnerException}");
            }
        }
    }
}
