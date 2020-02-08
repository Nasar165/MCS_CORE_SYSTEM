using System.IO;

namespace Components.Interface
{
    public interface IFileWriter
    {
        void DeleteFile(string filePath);
        void DeleteDirectory(string directoryPath);
        void CreateDirectoryPath(string directoryPath);
        void CreateFile(string filePath);
        void AppendTextToFile(string text, string filePath, FileMode fileMode);
        string ReadTextFromFile(string filePath);
    }
}