using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;

namespace Components.Test
{
    [TestCategory("GithubAction")]
    [TestClass]
    public class FileWriterTest
    {
        // Each file or folder that is created can be
        // found in the bin/debug/netcoreapp3.0
        private FileWriter writer = new FileWriter();
        private string WorkingDirectory = Directory.GetCurrentDirectory();

        [TestMethod]
        public void DirectoryExists()
        {
            var direcrotyExists = Validation.DirecortyPathExists(WorkingDirectory);
            Assert.IsTrue(direcrotyExists);
        }

        [TestMethod]
        public void CreateDirectory()
        {
            var direcrotyPath = $"{WorkingDirectory}/test_folder";
            writer.CreateDirectoryPath(direcrotyPath);
            Assert.IsTrue(Validation.DirecortyPathExists(direcrotyPath));
        }

        [TestMethod]
        public void WriteFile()
        {
            var filePath = $"{WorkingDirectory}/test_folder/test_file.txt";
            writer.CreateFile(filePath);
            Assert.IsTrue(Validation.FilePathExists(filePath));
        }

        [TestMethod]
        public void WriteTextToFile()
        {
            var filePath = $"{WorkingDirectory}/test_folder/test_file.txt";
            var text = "This is a test " + DateTime.Now;
            writer.AppendTextToFile(text, filePath, FileMode.Append);
            var textfile = File.ReadAllText(filePath);
            Assert.IsTrue(textfile.Contains(text));
        }
    }
}