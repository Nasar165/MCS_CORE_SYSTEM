namespace mcs.Components.Interface
{
    interface IFileWriter
    {

        bool DirecortyPathExists(string direcortyPath);
        bool FilePathExists(string filePath);
        void CreateDirectoryPath(string directoryPath);
        void CreateFile(string filePath);
        void AppendTextToFile(string text, string filePath);
    }
}