using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Components.Interface;
using Microsoft.AspNetCore.Http;

namespace Components
{
    public class FileHandler : IFileHandler
    {
        public delegate bool FileType(IFormFile file);

        private string RemoveIllegalChars(string fileName)
        {
            var illegalChars = Path.GetInvalidPathChars();
            illegalChars = Path.GetInvalidFileNameChars();
            fileName = fileName.Trim(illegalChars);
            fileName = fileName.Replace('/', 'w');
            return fileName;
        }

        private string CreateFileName()
        {
            var random = new Byte[32];
            using (var rand = RandomNumberGenerator.Create())
            {
                rand.GetBytes(random);
                return RemoveIllegalChars(Convert.ToBase64String(random));
            }
        }

        private List<IFormFile> GetFilesFromFileCollection(IFormFileCollection files, FileType validation)
        {
            var list = new List<IFormFile>();
            foreach (var file in files)
            {
                if (validation(file))
                    list.Add(file);
            }
            return list;
        }

        private string FileNameNotTaken(IFormFile file, string directoryPath)
        {

            var filePath = "";
            do
            {
                var fileExtension = Path.GetExtension(file.FileName);
                var fileName = $"{CreateFileName()}{fileExtension}";
                filePath = $"{directoryPath}/{fileName}";
            }
            while (File.Exists(filePath));
            return filePath;
        }

        private Stack SaveFiles(IReadOnlyCollection<IFormFile> Files, string directoryPath)
        {
            var stack = new Stack();
            foreach (var image in Files)
            {
                if (image.Length > 0)
                {
                    var filePath = FileNameNotTaken(image, directoryPath);
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(fs);
                        fs.Close();
                        fs.Dispose();
                    }
                    stack.Push(new FileInfo(filePath).Name);
                }
            }
            return stack;
        }

        public Stack SaveFile(IFormFileCollection files, string directoryPath, FileType validation)
        {
            try
            {
                if (validation is null)
                    throw new Exception("FileType Validation is null");

                var images = GetFilesFromFileCollection(files, validation);
                return SaveFiles(images, directoryPath);
            }
            catch
            {
                throw;
            }
        }

        public byte[] GetFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new Exception($"{filePath} does not exist");

                return File.ReadAllBytes(filePath); ;
            }
            catch
            {
                throw;
            }
        }

        private Stack GetFileNames(string[] files)
        {
            var array = new Stack();
            foreach (var file in files)
            {
                if (!string.IsNullOrWhiteSpace(file))
                    array.Push(new FileInfo(file).Name);
            }
            return array;
        }

        public Stack GetAllFiles(string directory)
        {
            var dirFiles = Directory.GetFiles($"{Directory.GetCurrentDirectory()}/{directory}");
            if (dirFiles.Length == 0)
                throw new Exception("");
            return GetFileNames(dirFiles);
        }
    }
}