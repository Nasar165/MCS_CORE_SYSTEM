using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Components.Interface;

namespace Components.Security
{
    public class FileIntegrity : IFileIntegrity
    {
        public IList<FileHash> FileHashList { get; }

        private string GetSHA256FileSum(string filePath)
        {
            using(var SHA256 = SHA256Managed.Create())
                using(var fileStream = File.OpenRead(filePath))
                    return Convert.ToBase64String(SHA256.ComputeHash(fileStream));
        }

        private FileHash GetFileHashFromList(string fileName)
            => FileHashList.FirstOrDefault(x => x.FileName == fileName);

        public bool FileIntegrityIsIntact(string fileName)
        {
            var fileHash = GetFileHashFromList(fileName);
            var hash = GetSHA256FileSum(fileHash.FilePath);
            return Validation.StringsAreEqual(hash, fileHash.Hash);
        }

        private FileHash CreateFileHash(string filenName, string filePath)        
            => new FileHash(){
                FileName = filenName,
                FilePath = filePath,
                Hash =  GetSHA256FileSum(filePath)
            };
        

        public void AddFileHashToIntegrityStore(string fileName, string filePath)
        {
            var dir = Directory.GetCurrentDirectory();
            FileHashList.Add(CreateFileHash(fileName,filePath));
        }

        public void DeleteFileIntegrityFromStore(string fileName)
        {
            FileHashList.Remove(GetFileHashFromList(fileName));
        }

        public void UpdateFileInterity(string fileName)
        {
            throw new System.NotImplementedException();
        }
    }
}