namespace Components.Interface
{
    public interface IFileWriter
    {
        void CreateDirectoryPath(string directoryPath);
        void CreateFile(string filePath);
        void AppendTextToFile(string text, string filePath);
        string ReadTextFromFile(string filePath);
    }
}