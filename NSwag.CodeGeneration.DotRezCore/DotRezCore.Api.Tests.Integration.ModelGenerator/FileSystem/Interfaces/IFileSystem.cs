namespace DotRezCore.Api.Tests.Integration.ModelGenerator.FileSystem.Interfaces
{
    public interface IFileSystem
    {
        void WriteAllText(string path, string content);

        /// <summary>
        /// Create the folder if not existing for a full file name
        /// </summary>
        /// <param name="filename">full path of the file</param>
        void EnsureDirectoryStructure(string filename);
    }
}