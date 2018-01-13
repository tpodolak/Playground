using System.IO;
using DotRezCore.Api.Tests.Integration.ModelGenerator.FileSystem.Interfaces;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.FileSystem
{
    public class PhysicalFileSystem : IFileSystem
    {
        /// <summary>
        /// Create the folder if not existing for a full file name
        /// </summary>
        /// <param name="filename">full path of the file</param>
        public void EnsureDirectoryStructure(string filename)
        {
            var folder = Path.GetDirectoryName(filename);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }

        public void WriteAllText(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}