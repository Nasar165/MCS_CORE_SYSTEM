using System;
using System.IO;
using System.Text;
using Components.Interface;

namespace Components
{
    public class FileWriter : IFileWriter
    {
        public bool DirecortyPathExists(string direcortyPath)
            => Directory.Exists(direcortyPath);

        public bool FilePathExists(string filePath)
            => File.Exists(filePath);

        public void DeleteFile(string filePath)
        {
            if (FilePathExists(filePath))
                File.Delete(filePath);
        }

        public void CreateDirectoryPath(string directoryPath)
        {
            if (!DirecortyPathExists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }

        public void CreateFile(string filePath)
        {
            if (!FilePathExists(filePath))
            {
                var file = File.Create(filePath);
                file.Close();
            }
        }

        private void AddTextToFile(FileStream fs, string text)
        {
            byte[] byteArray = new UTF8Encoding(true).GetBytes(text + Environment.NewLine);
            fs.Write(byteArray, 0, byteArray.Length);
        }

        public void AppendTextToFile(string text, string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                {
                    AddTextToFile(fs, text);
                }
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}