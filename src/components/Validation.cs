using System;
using System.IO;

namespace Components
{
    public static class Validation
    {
        public static bool DirecortyPathExists(string direcortyPath)
            => Directory.Exists(direcortyPath);

        public static bool FilePathExists(string filePath)
            => File.Exists(filePath);

        public static bool StringsAreEqual(string one, string two)
           => one.ToLower() == two.ToLower() ? true : false;

        public static bool ObjectIsNull(object obj)
            => obj == null ? true : false;

        public static bool StringIsNullOrEmpty(string text)
            => string.IsNullOrEmpty(text);

        public static bool ValueIsGreateherThan(int value, int greater)
            => value > greater ? true : false;

        public static bool FilePathIsValid(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return false;
            else
                return Directory.Exists(filePath) ? true : false;
        }

        public static bool FileExists(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return false;
            else
                return File.Exists(filePath) ? true : false;
        }
        public static bool AppSettingIsValid(string key, string value)
             => !StringIsNullOrEmpty(key) && !StringIsNullOrEmpty(value) ? true : false;

        public static bool StringContainsValue(string text, string value)
             => text.Contains(value) ? true : false;

        public static void ValidateLogFile(string directoryPath, string filename)
        {
            var fileWriter = new FileWriter();
            if (!DirecortyPathExists($"{directoryPath}"))
                fileWriter.CreateDirectoryPath($"{directoryPath}");

            if (!FilePathExists($"{directoryPath}{filename}"))
                fileWriter.CreateFile($"{directoryPath}{filename}");
        }
    }
}