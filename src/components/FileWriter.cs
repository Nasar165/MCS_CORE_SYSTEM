using System;
using System.IO;
using System.Text;
using Components.Interface;

namespace Components
{
    public class FileWriter : IFileWriter
    {
        public void DeleteFile(string filePath)
        {
            if (Validation.FilePathExists(filePath))
                File.Delete(filePath);
        }

        public void CreateDirectoryPath(string directoryPath)
        {
            if (!Validation.DirecortyPathExists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }

        public void CreateFile(string filePath)
        {
            if (!Validation.FilePathExists(filePath))
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
            catch (Exception)
            {
                throw;
            }
        }

        public string ReadTextFromFile(string filePath)
        {
              try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using(StreamReader reader = new StreamReader(fs))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}