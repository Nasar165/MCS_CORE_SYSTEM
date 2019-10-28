using System;
using System.IO;
using System.Text;
using mcs.components.Interface;

namespace mcs.components
{
    public class FileWriter : IFileWriter
    {

        private void AddTextToFile(FileStream fs, string text)
        {
            byte[] byteArray = new UTF8Encoding(true).GetBytes(text + Environment.NewLine);
            fs.Write(byteArray, 0, byteArray.Length);
        }

        public void SaveDataAsFile(string text, string filePath)
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