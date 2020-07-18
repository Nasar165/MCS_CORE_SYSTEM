using System.Collections;
using Microsoft.AspNetCore.Http;

namespace Components.Interface
{
    public interface IFileHandler
    {
        Stack SaveFile(IFormFileCollection files, string directoryPath, FileHandler.FileType validation);
        byte[] GetFile(string filePath);
        Stack GetAllFiles(string directoryPath);
    }
}